﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exercise01TriangleBase : MonoBehaviour
{
	public Material mat;

	void Start ()
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

		List<int> triangles = new List<int>();
		triangles.Add(0);
		triangles.Add(1);
		triangles.Add(2);
		
		mesh.SetVertices(vertices);
		mesh.SetTriangles(triangles, 0);				
	}
}
