using UnityEngine;

public class AmmoStockPile : MonoBehaviour
{
	public int Capacity
	{
		get
		{
			return capacity;
		}
	}

	public int Current
	{
		set
		{
			current = Mathf.Clamp(value, 0, Capacity);
		}
		get
		{
			return current;
		}
	}

	[SerializeField] private int start;
	[SerializeField] private int capacity;

	private Entity entity;
	private int current;

	protected void Awake()
	{
		entity = GetComponent<Entity>() ?? GetComponentInParent<Entity>();
		current = start;
	}

	protected void OnEnable()
	{
		entity.Events.AddListener<RefillAmmoEvent>(OnRefillAmmoEvent);
	}

	protected void OnDisable()
	{
		entity.Events.RemoveListener<RefillAmmoEvent>(OnRefillAmmoEvent);
	}

	private void OnRefillAmmoEvent(RefillAmmoEvent evt)
	{
		current = capacity;
	}
}