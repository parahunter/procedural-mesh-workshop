using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exercise03Base : MonoBehaviour
{
	[Header("Global")]
	public float size = 20;
	public float featureScaling = 0.1f;
	public float featureMagnitude = 1f;
	
	[Header("Mesh specific")]
	public int subdivisions = 10;
	public float heightModifier = 4;
	[Header("Texture")]
	public Gradient heightToColor;
	public int textureSize = 512;
	
	public Material mat;

	Mesh mesh;
	Texture2D tex;

	private void Start()
	{
		MeshFilter filter = gameObject.AddComponent<MeshFilter>();
		MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
		renderer.material = mat;

		mesh = new Mesh();
		mesh.MarkDynamic();
		filter.mesh = mesh;

		tex = new Texture2D(textureSize, textureSize);
		tex.wrapMode = TextureWrapMode.Clamp;
		mat.mainTexture = tex;	
	}

	void Update ()
	{
		UpdateTexture();
		
		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();
		List<Vector2> uvs = new List<Vector2>();

		float quadSize = size / subdivisions;
		float reciprocalSubdivisions = 1.0f / subdivisions;

		for (int y = 0; y < subdivisions; y++)
		{
			for (int x = 0; x < subdivisions; x++)
			{
				float normalizedX = x * reciprocalSubdivisions;
				float normalizedY = y  * reciprocalSubdivisions;

				float height = GetNormalizedHeight(normalizedX, normalizedY) * heightModifier;

				Vector3 vertex = new Vector3(x * quadSize, height, y * quadSize);
								
				vertices.Add(vertex);
			}
		}

		for (int x = 0; x < subdivisions - 1; x++)
		{
			for (int y = 0; y < subdivisions - 1; y++)
			{
				int vertexIndex = x + y * subdivisions;

				triangles.Add(vertexIndex);
				triangles.Add(vertexIndex + subdivisions);
				triangles.Add(vertexIndex + subdivisions + 1);

				triangles.Add(vertexIndex + subdivisions + 1);
				triangles.Add(vertexIndex + 1);
				triangles.Add(vertexIndex);
			}
		}

		mesh.SetVertices(vertices);
		mesh.SetUVs(0, uvs);
		mesh.SetTriangles(triangles, 0);
		mesh.RecalculateNormals();
	}	

	Texture2D UpdateTexture()
	{		
		Color[] pixels = new Color[textureSize * textureSize];

		float texSize = 1.0f / textureSize;

		for(int y = 0; y < textureSize; y++)
		{
			for(int x = 0; x < textureSize; x++)
			{
				float normalizedX = x * texSize;
				float normalizedY = y * texSize;
				
				float height = GetNormalizedHeight(normalizedX, normalizedY);// Mathf.PerlinNoise(normalizedX * featureScaling, normalizedY * featureScaling);
				
				pixels[x + y * textureSize] = heightToColor.Evaluate(height);
			}
		}

		tex.SetPixels(pixels);
		tex.Apply();

		return tex;
	}

	float GetNormalizedHeight(float nx, float ny)
	{
		float featureHeight = Mathf.PerlinNoise(nx * featureScaling, ny * featureScaling);
		float height = featureHeight * featureMagnitude;

		return height;
	}
}
