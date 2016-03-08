using System;
using System.Collections.Generic;
using UnityEngine;

public class DecalManagerHelper : MonoBehaviour
{
	private MeshFilter meshFilter;
	private MeshRenderer meshRenderer;
	private int decalLenght;
	private int nextDecal;

	protected void Awake()
	{
		meshFilter = gameObject.AddComponent<MeshFilter>();
		meshRenderer = gameObject.AddComponent<MeshRenderer>();
		createMesh();
	}

	private void createMesh()
	{
		Mesh mesh = new Mesh();

		//Quaternion * Vector3.forward

		meshFilter.mesh = mesh;
	}

	protected void OnEnable()
	{
		GlobalEvents.AddListener<SpawnDecalEvent>(OnSpawnDecalEvent);
	}

	protected void OnDisable()
	{
		GlobalEvents.RemoveListener<SpawnDecalEvent>(OnSpawnDecalEvent);
	}

	protected void Update()
	{

	}

	private void OnSpawnDecalEvent(SpawnDecalEvent evt)
	{
		/*
		decal.position = evt.Position;
		decal.normal = evt.Normal;
		decal.tag = evt.Tag;
		*/

		Quaternion q = Quaternion.LookRotation (-evt.Normal);

		//(q * Vector3.left) + (q * Vector3.top)
		//(q * Vector3.right) + (q * Vector3.top)
		//(q * Vector3.left) + (q * Vector3.down)
		//(q * Vector3.right) + (q * Vector3.down)
	}
}