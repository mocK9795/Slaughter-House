using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public static class TextureGenerator { 
	public static Color[] CreateColorMap(int mapWidth, int mapHeight, float[,] noiseMap, TerrainType[] regions)
	{
		Color[] colorMap = new Color[noiseMap.GetLength(0) * noiseMap.GetLength(1)];
		for (int y = 0; y < mapHeight; y++)
		{
			for (int x = 0; x < mapWidth; x++)
			{
				for (int i = 0; i < regions.Length; i++)
				{
					if (noiseMap[x, y] <= regions[i].height)
					{
						colorMap[y * mapWidth + x] = regions[i].color;
						break;
					}
				}
			}
		}
		return colorMap;
	}
}
