using UnityEngine;
using System.Collections;

public class PlayerReloadAnimation : MonoBehaviour
{
	[SerializeField] private Animator animator;

	private Entity entity;

	protected void Awake()
	{
		entity = GetComponentInParent<Entity>();
	}

	protected void OnEnable()
	{
		entity.Events.AddListener<ReloadEvent>(OnReloadEvent);
		entity.Events.AddListener<ReloadDoneEvent>(OnReloadDoneEvent);
	}

	protected void OnDisable()
	{
		entity.Events.AddListener<ReloadEvent>(OnReloadEvent);
		entity.Events.AddListener<ReloadDoneEvent>(OnReloadDoneEvent);
	}

	private void OnReloadEvent(ReloadEvent evt)
	{
		animator.SetBool("Reloading", true);
	}

	private void OnReloadDoneEvent(ReloadDoneEvent evt)
	{
		animator.SetBool("Reloading", false);
	}
}