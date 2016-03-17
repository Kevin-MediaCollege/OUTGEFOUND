using UnityEngine;
using System.Collections;
using ParticlePlayground;

public class BulletImpact : MonoBehaviour 
{
	[SerializeField]private PlaygroundParticlesC dust;
	[SerializeField]private PlaygroundParticlesC pieces;

	public void init()
	{
		dust.Emit(true);
		pieces.Emit(true);
	}
}
