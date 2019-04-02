using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exercise02CylinderFinal : MonoBehaviour
{
	public float height = 1;
	public float radius = 0.5f;
	public int subdivisions = 40;

	public Material mat;

	void Start()
	{
		MeshFilter filter = gameObject.AddComponent<MeshFilter>();
		MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
		renderer.material = mat;

		Mesh mesh = new Mesh();
		filter.mesh = mesh;

		List<Vector3> vertices = new List<Vector3>();
		List<Vector3> normals = new List<Vector3>();
		List<int> triangles = new List<int>();

		Vector3 bottomCenterPoint = Vector3.down * height * 0.5f;
		Vector3 topCenterPoint = Vector3.up * height * 0.5f;

		int triangleOffset = 0;

		for(int i = 0; i < subdivisions; i++)
		{
			float radian = (Mathf.PI * 2 * i) / subdivisions;

			Vector3 planarOffset = new Vector3(Mathf.Cos(radian), 0, Mathf.Sin(radian));

			vertices.Add(planarOffset + bottomCenterPoint);
			vertices.Add(planarOffset + topCenterPoint);
			normals.Add(planarOffset.normalized);
			normals.Add(planarOffset.normalized);

			int nextOffset = triangleOffset + 2;
			if (nextOffset >= subdivisions * 2) //to handle wraparound
				nextOffset = 0;
			
			triangles.Add(triangleOffset);
			triangles.Add(triangleOffset + 1);
			triangles.Add(nextOffset);

			triangles.Add(nextOffset);
			triangles.Add(triangleOffset + 1);
			triangles.Add(nextOffset + 1);
			
			triangleOffset += 2;
		}
		
		Vector3 normal = Vector3.down;
		normals.Add(normal);
		vertices.Add(bottomCenterPoint);		
		triangleOffset += 1;
		int centerIndex = triangleOffset;

		int bottomIndexOffset = 0;
		for (int i = 0; i < subdivisions; i++)
		{
			float radian = (Mathf.PI * 2 * i) / subdivisions;

			Vector3 planarOffset = new Vector3(Mathf.Cos(radian), 0, Mathf.Sin(radian));

			vertices.Add(planarOffset + bottomCenterPoint);
			normals.Add(normal);
			
			triangles.Add(centerIndex);
			triangles.Add(centerIndex + bottomIndexOffset);
			
			bottomIndexOffset += 1;
			if (bottomIndexOffset >= subdivisions) //to handle wraparound
				bottomIndexOffset = 0;

			triangles.Add(centerIndex + bottomIndexOffset);			
		}

		triangleOffset += subdivisions;

		normal = Vector3.up;
		normals.Add(normal);
		vertices.Add(topCenterPoint);
		triangleOffset += 1;
		centerIndex = triangleOffset;

		bottomIndexOffset = 0;
		for (int i = 0; i < subdivisions; i++)
		{
			float radian = (Mathf.PI * 2 * i) / subdivisions;

			Vector3 planarOffset = new Vector3(Mathf.Cos(radian), 0, Mathf.Sin(radian));

			vertices.Add(planarOffset + topCenterPoint);
			normals.Add(normal);

			triangles.Add(centerIndex + bottomIndexOffset);
			triangles.Add(centerIndex);

			bottomIndexOffset += 1;
			if (bottomIndexOffset >= subdivisions) //to handle wraparound
				bottomIndexOffset = 0;

			triangles.Add(centerIndex + bottomIndexOffset);
		}


		mesh.SetVertices(vertices);
		mesh.SetNormals(normals);
		mesh.SetTriangles(triangles, 0);
	}
}
