using UnityEngine;

public static class NoiseGenerator {
    
    public enum NormalizeMode {Local, Global}

    public static float[] GenerateNoise(int resolution, int seed, float scale, int octaves, float persistance, float lacunarity, float offset) {
        
        float[] noiseMap = new float[resolution];

        System.Random prng = new System.Random (seed);
        float[] octaveOffsets = new float[octaves];

        float maxPossibleHeight = 0;
        float amplitude = 1f;
        float frequency = 1;

        for (int i = 0; i < octaves; i++) {
            float offsetX = prng.Next (-100000, 100000) + offset;
            octaveOffsets [i] = offsetX;

            maxPossibleHeight += amplitude;
            amplitude *= persistance;
        }

        if (scale <= 0) {
            scale = 0.0001f;
        }

        float maxLocalNoiseHeight = float.MinValue;
        float minLocalNoiseHeight = float.MaxValue;

        float halfWidth = resolution / 2f;

        for (int x = 0; x < resolution; x++) {

            amplitude = 1;
            frequency = 1;
            float noiseHeight = 0;

            for (int i = 0; i < octaves; i++) {
                float sampleX = (x-halfWidth + octaveOffsets[i]) / scale * frequency;
                float sampleY = 0f;

                float perlinValue = Mathf.PerlinNoise (sampleX, sampleY);
                noiseHeight += perlinValue * amplitude;

                amplitude *= persistance;
                frequency *= lacunarity;
            }

            if (noiseHeight > maxLocalNoiseHeight) {
                maxLocalNoiseHeight = noiseHeight;
            } else if (noiseHeight < minLocalNoiseHeight) {
                minLocalNoiseHeight = noiseHeight;
            }
            noiseMap [x] = noiseHeight;
        }


        for (int x = 0; x < resolution; x++) {
                float normalizedHeight = (noiseMap [x] + 1) / (maxPossibleHeight/0.9f);
                noiseMap [x] = Mathf.Clamp(normalizedHeight,0, int.MaxValue);
        }

        return noiseMap;
        
    }


}
