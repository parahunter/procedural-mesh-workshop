using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exercise01LFinal : MonoBehaviour
{

	public float height = 4;
	public float width = 10;
	public float thickness = 1;

	public Material mat;

	void Start ()
	{
		MeshFilter filter = gameObject.AddComponent<MeshFilter>();
		MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
		renderer.material = mat;

		Mesh mesh = new Mesh();
		filter.mesh = mesh;

		Vector3 offset = new Vector3(width, height, 0) * -0.5f;

		List<Vector3> vertices = new List<Vector3>();

		//the vertical bit of the L

		vertices.Add(offset);
		vertices.Add(offset + new Vector3(0, height, 0));
		vertices.Add(offset + new Vector3(thickness, height, 0));
		vertices.Add(offset + new Vector3(thickness, 0, 0));
		
		List<int> triangles = new List<int>();
		
		//first quad
		triangles.Add(0);
		triangles.Add(1);
		triangles.Add(2);
		triangles.Add(2);
		triangles.Add(3);
		triangles.Add(0);

		offset.x += thickness;

		//the horizontal bit of the L
		vertices.Add(offset);
		vertices.Add(offset + new Vector3(0, thickness, 0));
		vertices.Add(offset + new Vector3(width, thickness, 0));
		vertices.Add(offset + new Vector3(width, 0, 0));

		//next quad
		triangles.Add(4 + 0);
		triangles.Add(4 + 1);
		triangles.Add(4 + 2);
		triangles.Add(4 + 2);
		triangles.Add(4 + 3);
		triangles.Add(4 + 0);

		mesh.SetVertices(vertices);
		mesh.SetTriangles(triangles, 0);				
	}
}
