using UnityEngine;
using System.Collections;

public class Reload : MonoBehaviour
{
	[SerializeField] private Magazine magazine;
	[SerializeField] private AudioAsset reloadAudio;

	protected void Update()
	{
		if(Input.GetKeyDown(KeyCode.R) && magazine.Current < magazine.Capacity)
		{
			magazine.Put(magazine.Capacity - magazine.Current);
			AudioManager.PlayAt(reloadAudio, transform.position);
		}
	}
}