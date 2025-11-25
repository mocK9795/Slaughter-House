using UnityEngine;

public class MapDisplay : MonoBehaviour
{
	public Renderer textureRenderer;
	public MeshRenderer meshRenderer;
	public MeshFilter meshFilter;

	public int mapWidth;
	public int mapHeight;
	public float mapScale;
	public int octaves;
	public float lacunarity;
	public float persistence;
	public int seed;
	public Vector2 offset;
	public float heightScale = 1;

	public TerrainType[] regions;
	public float[,] noiseMap;
	public Texture2D texture;

	private Color[] colorMap;
	private MeshData meshData;

	private void Start()
	{
		CreateAndDisplay();
	}

	private void CreateAndDisplay()
	{
		texture = new Texture2D(mapWidth, mapHeight);
		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;

		noiseMap = Noise.CreateNoiseMap(mapWidth, mapHeight, mapScale, seed, octaves, lacunarity, persistence, offset);
		colorMap = TextureGenerator.CreateColorMap(mapWidth, mapHeight, noiseMap, regions);
		texture.SetPixels(colorMap);
		
		texture.Apply();

		meshData = MeshGenerator.GenerateTerrainMesh(noiseMap, heightScale);

		DrawMesh();
	}

	public void DrawTexture()
	{
		textureRenderer.sharedMaterial.mainTexture = texture;
		transform.localScale = new Vector3(texture.width / 100f, 1, texture.height / 100f);
	}

	public void DrawMesh()
	{
		meshFilter.sharedMesh = meshData.CreateMesh();
		meshRenderer.sharedMaterial.mainTexture = texture;
	}

	private void OnValidate()
	{
		mapWidth = Mathf.Max(mapWidth, 10);
		mapHeight = Mathf.Max(mapHeight, 10);
		lacunarity = Mathf.Max(lacunarity, 1);
		octaves = Mathf.Max(octaves, 1);
		CreateAndDisplay();
	}
}


[System.Serializable]
public struct TerrainType{
	public string name;
	public Color color;
	public float height;
}