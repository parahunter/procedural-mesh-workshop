using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exercise02BoxFinal : MonoBehaviour
{
	public Material mat;

	List<Vector3> vertices;
	List<Vector3> normals;
	List<int> triangles;
	int triangleIndex = 0;

	void Start()
	{
		MeshFilter filter = gameObject.AddComponent<MeshFilter>();
		MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
		renderer.material = mat;

		Mesh mesh = new Mesh();
		filter.mesh = mesh;

		vertices = new List<Vector3>();
		normals = new List<Vector3>();
		triangles = new List<int>();

		Vector3 offset = -Vector3.one * 0.5f;
		Vector3 p0 = offset + Vector3.zero;
		Vector3 p1 = offset + Vector3.forward;
		Vector3 p2 = offset + Vector3.right + Vector3.forward;
		Vector3 p3 = offset + Vector3.right;

		Vector3 p4 = p0 + Vector3.up;
		Vector3 p5 = p1 + Vector3.up;
		Vector3 p6 = p2 + Vector3.up;
		Vector3 p7 = p3 + Vector3.up;

		//Debug.DrawLine(transform.position + p0, transform.position + p1);
		//Debug.DrawLine(transform.position + p1, transform.position + p2);
		//Debug.DrawLine(transform.position + p2, transform.position + p3);
		//Debug.DrawLine(transform.position + p3, transform.position + p0);

		//Debug.Break();

		AddSide(p3, p2, p1, p0); //bottom

		AddSide(p0, p4, p7, p3); //front
		AddSide(p1, p2, p6, p5); //back
		AddSide(p0, p1, p5, p4); //left
		AddSide(p2, p3, p7, p6); //right
		
		AddSide(p4, p5, p6, p7); //top

		mesh.SetVertices(vertices);
		mesh.SetNormals(normals);
		mesh.SetTriangles(triangles, 0);
	}

	void AddSide(Vector3 corner0, Vector3 corner1, Vector3 corner2, Vector3 corner3)
	{
		Vector3 normal = Vector3.Cross((corner1 - corner0), (corner3 - corner0)).normalized;

		vertices.Add(corner0);
		vertices.Add(corner1);
		vertices.Add(corner2);
		vertices.Add(corner3);

		normals.Add(normal);
		normals.Add(normal);
		normals.Add(normal);
		normals.Add(normal);
		
		triangles.Add(triangleIndex);
		triangles.Add(triangleIndex + 1);
		triangles.Add(triangleIndex + 2);

		triangles.Add(triangleIndex + 2);
		triangles.Add(triangleIndex + 3);
		triangles.Add(triangleIndex);

		triangleIndex += 4;
	}
}
