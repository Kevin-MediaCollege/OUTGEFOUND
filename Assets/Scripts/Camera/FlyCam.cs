using UnityEngine;

public class FlyCam : MonoBehaviour
{
	[SerializeField] private float speed = 50.0f;
	[SerializeField] private float sensitivity = 0.25f;

	[SerializeField] private bool smooth = true;
	[SerializeField] private float acceleration = 0.1f;

	private Vector3 lastDir;

	private float actSpeed;
    private float rotationX;
    private float rotationY;
	
	protected void Update () {
        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY += Input.GetAxis("Mouse Y") * sensitivity;

		transform.rotation = Quaternion.AngleAxis(rotationX, Vector3.up);
        transform.rotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

		Vector3 dir = new Vector3();

		if(Input.GetKey(KeyCode.W))
		{
			dir.z += 1.0f;
		}
		else if(Input.GetKey(KeyCode.S))
		{
			dir.z -= 1.0f;
		}

		if(Input.GetKey(KeyCode.A))
		{
			dir.x -= 1.0f;
		}
		else if(Input.GetKey(KeyCode.D))
		{
			dir.x += 1.0f;
		}

		if(Input.GetKey(KeyCode.LeftControl))
		{
			dir.y -= 1.0f;
		}
		else if(Input.GetKey(KeyCode.LeftShift))
		{
			dir.y += 1.0f;
		}

        dir.Normalize();

		if(dir != Vector3.zero)
		{
			if(actSpeed < 1)
			{
				actSpeed += acceleration * Time.deltaTime * 40;
			}
			else
			{
				actSpeed = 1.0f;
			}

			lastDir = dir;
		}
		else
		{
			if(actSpeed > 0)
			{
				actSpeed -= acceleration * Time.deltaTime * 20;
			}
			else
			{
				actSpeed = 0.0f;
			}
		}

		if(smooth)
		{
			transform.Translate(lastDir * actSpeed * speed * Time.deltaTime);
		}
		else
		{
			transform.Translate(dir * speed * Time.deltaTime);
		}
	}
}
