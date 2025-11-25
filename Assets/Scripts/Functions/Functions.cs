using UnityEngine;

public static class Functions
{
	public static Vector3 RemovePlaneComponent(Vector3 vector, Vector3 planeNormal)
	{
		Vector3 n = planeNormal.normalized;
		float dot = Vector3.Dot(vector, n);
		return dot * n; // only the perpendicular part remains
	}

	public static Vector3 ProjectOntoPlane(Vector3 vector, Vector3 planeNormal)
	{
		Vector3 normalizedNormal = planeNormal.normalized;
		float dot = Vector3.Dot(vector, normalizedNormal);
		Vector3 projection = vector - dot * normalizedNormal;
		return projection;
	}


	#region Gausian Blur
	/// <summary>
	/// Apply Gaussian blur to a float[,] array.
	/// </summary>
	/// <param name="input">Source array</param>
	/// <param name="radius">Kernel radius (e.g. 3)</param>
	/// <param name="sigma">Standard deviation (e.g. 1.0f)</param>
	/// <returns>Blurred array</returns>
	public static float[,] ApplyGausianBlur(float[,] input, int radius, float sigma)
	{
		int width = input.GetLength(0);
		int height = input.GetLength(1);

		// Step 1: Build 1D Gaussian kernel
		float[] kernel = BuildGausianKernel(radius, sigma);

		// Step 2: Horizontal pass
		float[,] temp = new float[width, height];
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				float sum = 0f;
				float weightSum = 0f;

				for (int k = -radius; k <= radius; k++)
				{
					int sampleX = Mathf.Clamp(x + k, 0, width - 1);
					float weight = kernel[k + radius];
					sum += input[sampleX, y] * weight;
					weightSum += weight;
				}

				temp[x, y] = sum / weightSum;
			}
		}

		// Step 3: Vertical pass
		float[,] output = new float[width, height];
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				float sum = 0f;
				float weightSum = 0f;

				for (int k = -radius; k <= radius; k++)
				{
					int sampleY = Mathf.Clamp(y + k, 0, height - 1);
					float weight = kernel[k + radius];
					sum += temp[x, sampleY] * weight;
					weightSum += weight;
				}

				output[x, y] = sum / weightSum;
			}
		}

		return output;
	}

	/// <summary>
	/// Build 1D Gaussian kernel.
	/// </summary>
	private static float[] BuildGausianKernel(int radius, float sigma)
	{
		float[] kernel = new float[radius * 2 + 1];
		float sigma2 = 2 * sigma * sigma;
		float sum = 0f;

		for (int i = -radius; i <= radius; i++)
		{
			float value = Mathf.Exp(-(i * i) / sigma2);
			kernel[i + radius] = value;
			sum += value;
		}

		// Normalize kernel
		for (int i = 0; i < kernel.Length; i++)
			kernel[i] /= sum;

		return kernel;
	}
	#endregion
}