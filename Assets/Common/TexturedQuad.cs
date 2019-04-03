using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexturedQuad : MonoBehaviour
{
	public Material mat;

	void Start()
	{
		MeshFilter filter = gameObject.AddComponent<MeshFilter>();
		MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
		renderer.material = mat;

		Mesh mesh = new Mesh();
		filter.mesh = mesh;

		List<Vector3> vertices = new List<Vector3>();
		vertices.Add(new Vector3(-1, -1, 0));
		vertices.Add(new Vector3(-1, 1, 0));
		vertices.Add(new Vector3(1, 1, 0));
		vertices.Add(new Vector3(1, -1, 0));

		List<int> triangles = new List<int>();
		triangles.Add(0);
		triangles.Add(1);
		triangles.Add(2);

		triangles.Add(2);
		triangles.Add(3);
		triangles.Add(0);

		List<Vector2> uvs = new List<Vector2>();
		uvs.Add(new Vector2(0, 0));
		uvs.Add(new Vector2(0, 1));
		uvs.Add(new Vector2(1, 1));
		uvs.Add(new Vector2(1, 0));

		mesh.SetVertices(vertices);
		mesh.SetUVs(0, uvs);
		mesh.SetTriangles(triangles, 0);
	}
}
