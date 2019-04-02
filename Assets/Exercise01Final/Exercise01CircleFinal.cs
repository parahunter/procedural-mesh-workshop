using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exercise01CircleFinal : MonoBehaviour
{
	public float radius = 1;
	public int subdivisions = 10;
	public Material mat;

	void Start ()
	{
		MeshFilter filter = gameObject.AddComponent<MeshFilter>();
		MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
		renderer.material = mat;

		Mesh mesh = new Mesh();
		filter.mesh = mesh;

		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();

		vertices.Add(Vector3.zero);

		Vector3 point = Vector3.zero;
		for(int i = 0; i < subdivisions; i++)
		{
			float radian = (2 * Mathf.PI * i) / subdivisions;
			point.x = Mathf.Cos(radian);
			point.y = Mathf.Sin(radian);
			point *= radius;

			vertices.Add(point);

			int nextVertex = (i + 1) % subdivisions;
			triangles.Add(0);
			triangles.Add(1 + nextVertex);
			triangles.Add(1 + i);
		}

		mesh.SetVertices(vertices);		
		mesh.SetTriangles(triangles, 0);				
	}
}
