using UnityEngine;

public class WaveMesh : MonoBehaviour
{
    [SerializeField] private float baseAmplitude = 0.5f; // Base wave height
    [SerializeField] private float noiseAmplitude = 0.2f; // Amplitude of Perlin noise for randomness
    [SerializeField] private float noiseFrequency = 2.0f; // Frequency of Perlin noise
    [SerializeField] private float wavelength = 2.0f; // Distance between wave peaks
    [SerializeField] private float waveSpeed = 1.0f; // Speed of wave movement
    [SerializeField] private float direction = 45f; // Angle of wave direction (in radians)
    private float _height;

    private Mesh mesh;
    private float timeOffset = 0.0f;

    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    private void Update()
    {
        Vector3[] vertices = mesh.vertices;

        // Update time offset for wave movement
        timeOffset += Time.deltaTime * waveSpeed;

        for (int i = 0; i < vertices.Length; i++)
        {
            float x = vertices[i].x * Mathf.Cos(direction) + vertices[i].z * Mathf.Sin(direction);
            float z = vertices[i].z * Mathf.Cos(direction) - vertices[i].x * Mathf.Sin(direction);

            // Calculate base wave height based on sine function
            float baseWaveHeight = Mathf.Sin((x * wavelength + timeOffset) * waveSpeed) * baseAmplitude;

            // Add Perlin noise for randomness
            float noise = Mathf.PerlinNoise(x * noiseFrequency, z * noiseFrequency) * noiseAmplitude;

            // Combine base wave and noise
            _height = baseWaveHeight + noise;

            vertices[i] = new Vector3(x, _height, z);
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
    
    public float GetWaveHeight(Vector3 position)
    {
        // Transform position to local space based on wave direction
        float x = position.x * Mathf.Cos(direction) + position.z * Mathf.Sin(direction);
        float z = position.z * Mathf.Cos(direction) - position.x * Mathf.Sin(direction);

        // Calculate base wave height based on sine function
        float baseWaveHeight = Mathf.Sin((x * wavelength + timeOffset) * waveSpeed) * baseAmplitude;

        // Add Perlin noise for randomness
        float noise = Mathf.PerlinNoise(x * noiseFrequency, z * noiseFrequency) * noiseAmplitude;

        // Combine base wave and noise
        var height = baseWaveHeight + noise;
        return height;
    }
}