using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	[SerializeField, HideInInspector] private Entity entity;

	private EntityMovement entityMovement;

	protected void Awake()
	{
		entityMovement = entity.Movement();
	}

	protected void Reset()
	{
		entity = GetComponent<Entity>();
	}

	protected void Update()
	{
		float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");

		if(vertical != 0 || horizontal != 0)
		{
			Vector3 input = new Vector3(horizontal, 0, vertical);
			entityMovement.Move(input);
		}
	}
}