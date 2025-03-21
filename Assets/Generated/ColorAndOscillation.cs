using UnityEngine;

public class ColorAndOscillation : MonoBehaviour
{
    [Tooltip("Speed at which the color changes.")]
    [SerializeField] private float colorChangeSpeed = 1.0f;

    [Tooltip("Frequency of the vertical oscillation.")]
    [SerializeField] private float oscillationFrequency = 1.0f;

    [Tooltip("Amplitude of the vertical oscillation.")]
    [SerializeField] private float oscillationAmplitude = 1.0f;

    private MeshRenderer meshRenderer;
    private float initialY;
    private float hue;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        initialY = transform.position.y;
    }

    void Update()
    {
        // Change color hue
        hue += colorChangeSpeed * Time.deltaTime;
        if (hue > 1.0f) hue -= 1.0f;
        Color color = Color.HSVToRGB(hue, 1.0f, 1.0f);
        meshRenderer.material.color = color;

        // Oscillate vertically
        float scaleAdjustedAmplitude = oscillationAmplitude * transform.localScale.y;
        float newY = initialY + Mathf.Sin(Time.time * oscillationFrequency) * scaleAdjustedAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}