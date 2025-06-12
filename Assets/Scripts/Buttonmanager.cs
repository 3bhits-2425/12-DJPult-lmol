using UnityEngine;
using UnityEngine.UI;

public class DJTrackController : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject turntable;

    public Button playPauseButton;
    public Button stopButton;
    public Slider speedSlider;
    public Slider volumeSlider;
    public Slider crossfaderSlider;

    [Range(0f, 1f)]
    public float sideBias = 0f; // 0 = ganz links, 1 = ganz rechts

    private bool isPlaying = false;

    void Start()
    {
        if (playPauseButton != null) playPauseButton.onClick.AddListener(TogglePlayPause);
        if (stopButton != null) stopButton.onClick.AddListener(StopTrack);
    }

    void Update()
    {
        // Tempo anpassen
        audioSource.pitch = speedSlider.value;

        // Lautstärke berechnen: weniger, je weiter weg vom eigenen Bias
        float fade = Mathf.Clamp01(1f - Mathf.Abs(sideBias - crossfaderSlider.value));
        audioSource.volume = volumeSlider.value * fade;

        // Plattenteller drehen
        if (isPlaying)
        {
            turntable.transform.Rotate(Vector3.up * 360 * Time.deltaTime * speedSlider.value);
        }
    }

    void TogglePlayPause()
    {
        if (!isPlaying)
        {
            audioSource.Play();
            isPlaying = true;
        }
        else
        {
            audioSource.Pause();
            isPlaying = false;
        }
    }

    void StopTrack()
    {
        audioSource.Stop();
        isPlaying = false;
    }
}
