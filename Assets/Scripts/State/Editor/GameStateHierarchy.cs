using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class GameStateHierarchy
{
	static GameStateHierarchy()
	{
		EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
	}

	private static void OnHierarchyGUI(int instanceID, Rect selectionRect)
	{
		GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

		if(obj != null)
		{
			if(obj.GetComponent<GameState>() != null)
			{
				if(Application.isPlaying)
				{
					bool enable = true;

					Transform last = obj.transform;
					while(last != obj.transform.root)
					{
						last = last.parent;

						if(!last.gameObject.activeSelf)
						{
							enable = false;
							break;
						}
					}

					if(enable)
					{
						Rect rect = new Rect(selectionRect);
						rect.x = selectionRect.width - 12;
						rect.y += 2;
						rect.width = 12;
						rect.height = 12;

						Color origColor = GUI.backgroundColor;

						GUI.backgroundColor = obj.activeSelf ? Color.green : Color.red;
						GUI.Box(rect, "");

						GUI.backgroundColor = origColor;
					}
				}
			}
		}
	}
}