using UnityEngine;

/// <summary>
/// The first-person camera behaviour
/// </summary>
public class GameCamera : MonoBehaviour
{
	public static Vector3 offset;

	public Vector2 Sensitivity { set; get; }

	[SerializeField] private Vector2 yConstraint;

	private Transform weapon;
	private Transform player;
	private Transform eyes;

	private float rotationX;
	private float rotationY;

	protected void OnEnable()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	protected void OnDisable()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	protected void Update()
	{
		if(player == null || weapon == null || eyes == null)
		{
			Entity p = EntityUtils.GetEntityWithTag("Player");

			if(p == null)
			{
				Debug.LogError("No player found");
				return;
			}

			player = p.transform;
			weapon = player.Find("Weapon");
			eyes = player.Find("Eyes");
		}

		if(Cursor.visible || Cursor.lockState != CursorLockMode.Locked)
		{
			if(Input.GetMouseButtonDown(0))
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}

		UpdateCamera();

		// Move the camera to the player's eyes
		Camera.main.transform.position = eyes.position;
		Camera.main.transform.rotation = eyes.rotation;
	}

	private void UpdateCamera()
	{
		rotationX += Input.GetAxis("Mouse X") * Sensitivity.x;
		rotationY += Input.GetAxis("Mouse Y") * Sensitivity.y;

		rotationX += offset.x;
		rotationY += offset.y;
		rotationY = Mathf.Clamp(rotationY, yConstraint.x, yConstraint.y);

		player.rotation = Quaternion.AngleAxis(rotationX, Vector3.up);

		eyes.rotation = Quaternion.AngleAxis(rotationX, Vector3.up);
		eyes.rotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

		offset = Vector3.zero;
	}
}