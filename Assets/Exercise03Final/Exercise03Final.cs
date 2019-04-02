using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exercise03Final : MonoBehaviour
{
	[Header("Global")]
	public float size = 20;
	public float featureScaling = 0.1f;
	public float featureMagnitude = 1f;
	public float fineFeatureScaling = 0.1f;
	public float fineFeatureMagnitude = 0.1f;
	
	[Header("Mesh specific")]
	public int subdivisions = 10;
	public float heightModifier = 4;
	[Header("Texture")]
	public Gradient heightToColor;
	public int textureSize = 512;

	[Header("Speed")]
	public float speed = 3f;

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

	// Use this for initialization
	void Update ()
	{
		UpdateTexture();
		
		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();
		List<Vector2> uvs = new List<Vector2>();

		float quadSize = size / subdivisions;
		float reciprocalSubdivisions = 1.0f / subdivisions;
		float offset = Time.time * speed;
		
		for (int y = 0; y < subdivisions; y++)
		{
			for (int x = 0; x < subdivisions; x++)
			{
				float normalizedX = x * reciprocalSubdivisions;
				float normalizedY = y  * reciprocalSubdivisions;

				//normalizedX *= quadSize;
				//normalizedY *= quadSize;

				//		normalizedX += offset;
				//	normalizedY += offset;

				float height = GetNormalizedHeight(normalizedX, normalizedY) * heightModifier;// Mathf.PerlinNoise(normalizedX * featureScaling, normalizedY * featureScaling) * heightModifier;

		//		Vector3 vertex = new Vector3(x, normalizedX + normalizedY, y);

				Vector3 vertex = new Vector3(x * quadSize, height, y * quadSize);
								
				vertices.Add(vertex);
				uvs.Add(new Vector2(normalizedX, normalizedY));
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
		float offset = Time.time * speed;

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

		height += featureHeight * featureHeight * Mathf.Lerp(-fineFeatureMagnitude, fineFeatureMagnitude, Mathf.PerlinNoise(nx * fineFeatureScaling, ny * fineFeatureScaling));

		return height;
	}
}
