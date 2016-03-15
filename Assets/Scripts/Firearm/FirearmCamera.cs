using UnityEngine;

public class FirearmCamera : MonoBehaviour
{
	// Hack
	public static Vector3 recoilRotation;

	[SerializeField] private Transform target;

	private Firearm firearm;

	protected void Update()
	{
		if(firearm == null)
		{
			firearm = GetComponentInChildren<Firearm>();

			if(firearm == null)
			{
				return;
			}
		}

		Vector3 rotation = Vector3.zero;
		rotation.x = -(firearm.RecoilOffset * 10);
		
		Vector3 shake = Vector3.zero;
		if(firearm.Firing)
		{
			shake = Random.insideUnitCircle * 0.15f;
		}

		recoilRotation = rotation + shake;
		firearm.transform.rotation = Quaternion.Euler(target.transform.eulerAngles + recoilRotation);
	}
}