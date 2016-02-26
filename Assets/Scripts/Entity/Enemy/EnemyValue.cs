using UnityEngine;

public class EnemyValue : MonoBehaviour
{
	public int Value
	{
		get
		{
			return value;
		}
	}

	[SerializeField] private int value;
}