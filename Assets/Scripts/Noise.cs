using UnityEngine;

public static class Noise
{
	// Credits to Sabation Legue on youtube
	// https://www.youtube.com/watch?v=WP-Bm65Q-1Y&list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3&index=3
	public static float[,] CreateNoiseMap(int width, int height, float scale, int seed, int octaves, float lacunarity, float persistence, Vector2 offset)
	{
		float halfWidth = width / 2f;
		float halfHeight = height / 2f;

		float[,] noiseMap = new float[width, height];

		scale = Mathf.Clamp01(scale);

		System.Random randint = new System.Random(seed);
		Vector2[] octaveOffsets = new Vector2[octaves];
		for (int i = 0; i < octaves; i++)
		{
			octaveOffsets[i] = new Vector2(randint.Next(-100_000, 100_000) + offset.x, randint.Next(-100_000, 100_000) + offset.y);
		}

		float maxNoiseHeight = float.MinValue;
		float minNoiseHeight = float.MaxValue;

		for (int y = 0; y < width; y++)
		{
			for (int x = 0; x < height; x++)
			{
				float amplitude = 1;
				float frequency = 1;
				float noiseHeight = 0;

				for (int i = 0; i < octaves; i++)
				{
					noiseHeight += (Mathf.PerlinNoise((x - halfWidth) * scale * frequency + octaveOffsets[i].x, (y - halfHeight) * scale * frequency + octaveOffsets[i].x) * amplitude * 2) - 1;
					amplitude *= persistence;
					frequency *= lacunarity;

				}

				if (noiseHeight < minNoiseHeight)
				{
					minNoiseHeight = noiseHeight;
				}
				if (noiseHeight > maxNoiseHeight)
				{
					maxNoiseHeight = noiseHeight;
				}
				noiseMap[x, y] = noiseHeight;
			}
		}

		for (int y = 0; y < width; y++)
		{
			for (int x = 0; x < height; x++) 
			{
				noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
			} 
		}

		return noiseMap;
	}
}
