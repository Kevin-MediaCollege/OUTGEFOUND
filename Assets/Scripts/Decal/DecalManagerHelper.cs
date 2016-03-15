using UnityEngine;

/// <summary>
/// Helper behaviour for managing bullet impact decals
/// </summary>
public class DecalManagerHelper : MonoBehaviour
{
	private MeshFilter meshFilter;
	private MeshRenderer meshRenderer;
	private Mesh mesh;
	private Vector3[] verts;
	private int[] tris;
	private Vector2[] uvs;

	private int poolLenght;
	private int nextPool;

	private bool meshUpdated;

	protected void Awake()
	{
		meshFilter = gameObject.AddComponent<MeshFilter>();
		meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		meshRenderer.material = Resources.Load("DecalMaterial") as Material;

		createMesh();
	}

	private void createMesh()
	{
		mesh = meshFilter.mesh;

		poolLenght = 1024;
		nextPool = 0;
		meshUpdated = true;

		verts = new Vector3[poolLenght * 4];
		tris = new int[poolLenght * 6];
		uvs = new Vector2[poolLenght * 4];

		int i4, i6;
		Vector3 invisible = new Vector3(1000f, 1000f, 1000f);
		for(int i = 0; i < poolLenght; i++)
		{
			i4 = i * 4;
			i6 = i * 6;

			verts[i4]     = invisible;
			verts[i4 + 1] = invisible;
			verts[i4 + 2] = invisible;
			verts[i4 + 3] = invisible;

			tris[i6]     = 0 + i4;
			tris[i6 + 1] = 1 + i4;
			tris[i6 + 2] = 2 + i4;
			tris[i6 + 3] = 2 + i4;
			tris[i6 + 4] = 1 + i4;
			tris[i6 + 5] = 3 + i4;

			uvs[i4]     = new Vector2(0f, 0f);
			uvs[i4 + 1] = new Vector2(1f, 0f);
			uvs[i4 + 2] = new Vector2(0f, 1f);
			uvs[i4 + 3] = new Vector2(1f, 1f);
		}

		mesh.vertices = verts;
		mesh.triangles = tris;
		mesh.uv = uvs;
		mesh.bounds = new Bounds(new Vector3(0f, 0f, 0f), new Vector3(1000f, 1000f, 1000f));
	}

	protected void Update()
	{
		if(meshUpdated)
		{
			mesh.vertices = verts;
			mesh.UploadMeshData(false);
			meshUpdated = false;
		}
	}

	public void AddDecal(Vector3 point, Vector3 normal, string tag)
	{
		if(normal == Vector3.zero || tag != "Wall") { return; }

		float size = 0.07f + Random.Range(0f, 0.02f);
		Quaternion q = Quaternion.LookRotation (-normal);
		q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, q.eulerAngles.z + UnityEngine.Random.Range(0f, 360f));
		verts[nextPool * 4]     = point + (q * (Vector3.left * size)) + (q * (Vector3.up * size)) + q * (Vector3.back * 0.01f);
		verts[nextPool * 4 + 1] = point + (q * (Vector3.right * size)) + (q * (Vector3.up * size)) + q * (Vector3.back * 0.01f);
		verts[nextPool * 4 + 2] = point + (q * (Vector3.left * size)) + (q * (Vector3.down * size)) + q * (Vector3.back * 0.01f);
		verts[nextPool * 4 + 3] = point + (q * (Vector3.right * size)) + (q * (Vector3.down * size)) + q * (Vector3.back * 0.01f);
		nextPool = nextPool >= poolLenght - 1 ? 0 : nextPool + 1;
		meshUpdated = true;
	}
}