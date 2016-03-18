using UnityEngine;
using System.Collections;
using ParticlePlayground;

public class BloodImpact : MonoBehaviour 
{
	[SerializeField]private PlaygroundParticlesC blood;

	public void init()
	{
		blood.Emit(true);
	}
}
