using UnityEngine;

public class TerrainBeatSequencer : MonoBehaviour
{
    public AudioSource audioSource;
    public Terrain terrain;
    public float scale = 10.0f;

    private TerrainData terrainData;
    private float[,] originalHeights;
    private float[] samples = new float[512];

    private void Start()
    {
        terrainData = terrain.terrainData;
        originalHeights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
    }

    private void Update()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);

        float[,] newHeights = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];
        for (int x = 0; x < terrainData.heightmapResolution; x++)
        {
            for (int y = 0; y < terrainData.heightmapResolution; y++)
            {
                float height = originalHeights[y, x] + samples[x % 512] * scale * Mathf.Sin(x + Time.time);
                newHeights[y, x] = height;
            }
        }
        terrainData.SetHeights(0, 0, newHeights);
    }
}
