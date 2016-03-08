using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDependency : IDependency
{
	private SceneLoader sceneLoader;
	private CoroutineRunner coroutineRunner;

	private Currency currency;
	private DecalManager decalManager;
	private AIManager aiManager;
	private CoverManager coverManager;

	public GameDependency()
	{
		sceneLoader = Dependency.Get<SceneLoader>();
		coroutineRunner = Dependency.Get<CoroutineRunner>();

		currency = Dependency.Get<Currency>();
		decalManager = Dependency.Get<DecalManager>();
		aiManager = Dependency.Get<AIManager>();
		coverManager = Dependency.Get<CoverManager>();
	}

	public void Start()
	{
		GlobalEvents.AddListener<EntityDiedEvent>(OnEntityDiedEvent);

		// Hardcode Level_1 because we probably won't have more levels
		sceneLoader.Load("Level_1");

		coroutineRunner.StartCoroutine(WaitForGameLoad());
	}

	public void Stop()
	{
		GlobalEvents.RemoveListener<EntityDiedEvent>(OnEntityDiedEvent);
		GlobalEvents.Invoke(new GameStoppedEvent());

		currency.Destroy();
		decalManager.Destroy();
		aiManager.Destroy();
		coverManager.Destroy();

		sceneLoader.Load("Menus");
	}

	private IEnumerator WaitForGameLoad()
	{
		while(sceneLoader.Loading)
		{
			yield return null;
		}

		currency.Create();
		decalManager.Create();
		aiManager.Create();
		coverManager.Destroy();

		GlobalEvents.Invoke(new GameStartedEvent());
	}

	private void OnEntityDiedEvent(EntityDiedEvent evt)
	{
		if(evt.Entity.HasTag("Player"))
		{
			// The player died, go back to the menu
			Stop();
		}
	}
}