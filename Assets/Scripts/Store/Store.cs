using UnityEngine;
using System.Collections;

public class Store : MonoBehaviour
{
	public bool IsPlayerInRange
	{
		get
		{
			Entity player = EntityUtils.GetEntityWithTag("Player");

			if(player != null)
			{
				float distance2 = (player.transform.position - transform.position).sqrMagnitude;

				if(distance2 < interactionRange * interactionRange)
				{
					return true;
				}
			}

			return false;
		}
	}

	[SerializeField] private float interactionRange = 3;

	[SerializeField] private Transform playerPosition;
	[SerializeField] private Canvas storeUI;

	private CameraStateManager cameraStateManager;

	private Quaternion origPlayerRotation;
	private Vector3 origPlayerPosition;

	protected void Awake()
	{
		cameraStateManager = Dependency.Get<CameraStateManager>();

		storeUI.enabled = false;
	}

	protected void Update()
	{
		if(IsPlayerInRange)
		{
			if(Input.GetKeyDown(KeyCode.F))
			{
				Open();
			}
			else if(Input.GetKeyDown(KeyCode.Escape))
			{
				Close();
			}
		}
	}

	protected void OnDrawGizmos()
	{
		float angle = 360f / 60;
		Quaternion rotation = Quaternion.Euler(0f, angle, 0f);

		Vector3 direction = interactionRange * Vector3.forward;
		Vector3 position = transform.position + new Vector3(0, 0.25f, 0);

		for(int i = 0; i < 60; i++)
		{
			Vector3 nextDir = rotation * direction;
			Gizmos.DrawLine(position + direction, position + nextDir);
			direction = nextDir;
		}
	}
	
	public void Open()
	{
		storeUI.enabled = true;
		cameraStateManager.SetCurrentState(0);

		Entity player = EntityUtils.GetEntityWithTag("Player");
		origPlayerPosition = player.transform.position;
		origPlayerRotation = player.transform.rotation;

		player.transform.position = playerPosition.position;
		player.transform.rotation = playerPosition.rotation;
	}

	public void Close()
	{
		storeUI.enabled = false;
		cameraStateManager.SetToDefaultState();

		Entity player = EntityUtils.GetEntityWithTag("Player");

		player.transform.position = origPlayerPosition;
		player.transform.rotation = origPlayerRotation;
	}
}