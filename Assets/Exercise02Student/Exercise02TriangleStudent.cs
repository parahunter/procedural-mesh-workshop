using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exercise02TriangleStudent : MonoBehaviour
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
		vertices.Add(new Vector3(0, 0.8f, 0));
		vertices.Add(new Vector3(1, -1, 0));

		List<Vector3> normals = new List<Vector3>();
		normals.Add(Vector3.back);
		normals.Add(Vector3.back);
		normals.Add(Vector3.back);
		
		List<int> triangles = new List<int>();
		triangles.Add(0);
		triangles.Add(1);
		triangles.Add(2);
		
		mesh.SetVertices(vertices);
		mesh.SetNormals(normals);
		mesh.SetTriangles(triangles, 0);
	}
}
