using UnityEngine;

public class SineGenerator : MonoBehaviour
{
    private AudioSource AudioSource;

    [SerializeField, Range(0, 1)]
    private float Volume = 1;

    [SerializeField, Range(100, 1000)]
    private int Frequency = 440;

    private const float PI_2 = 2 * Mathf.PI;
    private int SampleRate;
    private float Phase;

    private void Awake()
    {
        AudioSource = gameObject.AddComponent<AudioSource>();
        SampleRate = AudioSettings.outputSampleRate;
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        var samples = data.Length / channels;

        for (var i = 0; i < samples; ++i)
        {
            var value = GetNextSample();

            for (var ch = 0; ch < channels; ++ch)
            {
                data[i * channels + ch] = value;
            }
        }
    }

    private float GetNextSample()
    {
        Phase += Frequency * PI_2 / SampleRate;
        Phase %= PI_2;
        return Mathf.Sin(Phase) * Volume;
    }
}
