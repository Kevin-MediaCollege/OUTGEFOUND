using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// The state machine, this is the base of the game, it maintains the states and the game dependencies.
/// </summary>
public class StateMachine : MonoBehaviour
{
	[SerializeField] private StateCanvas canvas;
		
	// Active state instances
	private Dictionary<Link, LinkCondition> linkConditions;

	private IEventDispatcher eventDispatcher;
	private Scene mainScene;

	#region Unity callbacks
	protected void Awake()
	{
		if(canvas == null)
		{
			Debug.LogError("[StateMachine] No canvas has not been assigned.", this);
			return;
		}

		linkConditions = new Dictionary<Link, LinkCondition>();
		eventDispatcher = new EventDispatcher();

		foreach(Node node in canvas.Nodes)
		{
			if(node.EntryPoint)
			{
				SetCurrentNode(node);
				break;
			}
		}
	}

	protected void Update()
	{
		foreach(KeyValuePair<Link, LinkCondition> condition in linkConditions)
		{
			if(condition.Value.IsValid)
			{
				// Switch to this condition after this update cycle
				StartCoroutine(SetCurrentNodeAtEndOfFrame(condition.Key.To));
				break;
			}
		}
	}
	#endregion

	private void DestroyExisting()
	{
		if(linkConditions.Count > 0)
		{
			foreach(KeyValuePair<Link, LinkCondition> condition in linkConditions)
			{
				condition.Value.Destroy();
			}
		}

		linkConditions.Clear();
	}

	/// <summary>
	/// Instantiate all link conditions of a node.
	/// This will remove all current conditions and instantiate the ones defined in the node
	/// </summary>
	/// <remarks>
	/// This should only be used when switching to a new node in the state machine.
	/// </remarks>
	/// <param name="node">The node to read the conditions from.</param>
	private void InstantiateNodeLinkConditions(Node node)
	{
		// Create instances of link conditions
		foreach(Link link in node.Links)
		{
			foreach(LinkCondition condtion in link.Conditions)
			{
				condtion.Create();
				linkConditions.Add(link, condtion);

				// Register the event dispatcher if required.
				ICommunicantUtils.RegisterEventDispatcher(eventDispatcher, condtion);
			}
		}
	}

	/// <summary>
	/// Instantiate the state in a node.
	/// </summary>
	/// <param name="node"></param>
	private IEnumerator InstantiateNode(Node node)
	{
		List<Scene> current = new List<Scene>();
		for(int i = 0; i < SceneManager.sceneCount; i++)
		{
			current.Add(SceneManager.GetSceneAt(i));
		}

		List<string> next = new List<string>();
		foreach(string scene in node.AdditionalScenes)
		{
			next.Add(scene);
		}

		List<string> loaded = new List<string>();
		foreach(Scene scene in current)
		{
			if(scene.name == "Start")
			{
				continue;
			}

			if(!next.Contains(scene.name))
			{
				SceneManager.UnloadScene(scene.name);
			}
			else
			{
				loaded.Add(scene.name);
			}
		}

		AsyncOperation main = SceneManager.LoadSceneAsync(node.SceneName, LoadSceneMode.Additive);
		main.allowSceneActivation = false;
		while(main.progress < 0.9f)
		{
			yield return null;
		}

		List<AsyncOperation> additional = new List<AsyncOperation>();
		foreach(string scene in node.AdditionalScenes)
		{
			if(!loaded.Contains(scene))
			{
				additional.Add(SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive));
			}
		}

		while(true)
		{
			bool done = true;

			foreach(AsyncOperation async in additional)
			{
				done = done && (async.progress >= 0.9f);
			}

			if(done)
			{
				break;
			}

			yield return null;
		}
			
		main.allowSceneActivation = true;

		while(!main.isDone)
		{
			yield return null;
		}

		mainScene = SceneManager.GetSceneByName(node.SceneName);
		SceneManager.SetActiveScene(mainScene);

		GameObject[] rootGameObjects = mainScene.GetRootGameObjects();
		foreach(GameObject obj in rootGameObjects)
		{
			ICommunicantUtils.RegisterEventDispatcher(eventDispatcher, obj.GetComponentsInChildren<Component>(true));
		}
	}

	/// <summary>
	/// Set the current node.
	/// </summary>
	/// <param name="node">The new node.</param>
	private void SetCurrentNode(Node node)
	{
		DestroyExisting();
						
		StartCoroutine(InstantiateNodeAtNextFrame(node));
	}

	/// <summary>
	/// Switch to a new node at the end of the current frame. This allows the state machine to finish it's current cycle before switching.
	/// </summary>
	/// <param name="node">The new node.</param>
	/// <returns></returns>
	private IEnumerator SetCurrentNodeAtEndOfFrame(Node node)
	{
		yield return new WaitForEndOfFrame();

		SetCurrentNode(node);
	}

	private IEnumerator InstantiateNodeAtNextFrame(Node node)
	{
		yield return null;

		InstantiateNodeLinkConditions(node);

		StartCoroutine(InstantiateNode(node));
	}
}