using UnityEngine;
using System.Collections;

public class EntityMovement : MonoBehaviour
{
	[SerializeField, HideInInspector] private Entity entity;
	[SerializeField] private float speed;

	private new Rigidbody rigidbody;

	protected void Awake()
	{
		rigidbody = entity.GetComponent<Rigidbody>();
	}

	protected void Reset()
	{
		entity = GetComponent<Entity>();
	}

	protected void FixedUpdate()
	{
		rigidbody.velocity = Vector3.zero;
	}

	public void Move(Vector3 direction)
	{
		rigidbody.AddForce((direction * speed) * Time.deltaTime);
	}
}