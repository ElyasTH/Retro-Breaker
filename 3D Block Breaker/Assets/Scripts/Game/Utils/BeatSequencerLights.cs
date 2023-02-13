using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSequencerLights : MonoBehaviour
{
    public Light lightSource;
    public float threshold = 0.9f;
    public float transitionSpeed = 0.5f;

    [SerializeField] private int startingColorIndex = 0;

    [SerializeField] private Color[] _colors;
    private int _colorIndex;
    private Color _previousColor;
    private Color _targetColor;
    private float[] _samples = new float[512];
    private float[] _autocorrelation = new float[512];
    private float _sumOfSquaredDifferences;
    private int _peakIndex;

    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        _colorIndex = startingColorIndex;
        _previousColor = lightSource.color;
        _targetColor = _colors[_colorIndex];
    }

    private void Update()
    {
        GetSpectrumAudioSource();
        ComputeAutocorrelation();
        FindPeak();

        if (_autocorrelation[_peakIndex] > threshold)
        {
            _targetColor = _colors[_colorIndex];
            _colorIndex = (_colorIndex + 1) % _colors.Length;
        }

        lightSource.color = Color.Lerp(lightSource.color, _targetColor, transitionSpeed * Time.deltaTime);
        if (lightSource.color == _targetColor)
        {
            _previousColor = _targetColor;
        }
    }

    private void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    private void ComputeAutocorrelation()
    {
        for (int lag = 0; lag < _samples.Length / 2; lag++)
        {
            _sumOfSquaredDifferences = 0f;

            for (int i = 0; i < _samples.Length / 2; i++)
            {
                float sample1 = _samples[i];
                float sample2 = _samples[(i + lag) % (_samples.Length / 2)];
                _sumOfSquaredDifferences += (sample1 - sample2) * (sample1 - sample2);
            }

            _autocorrelation[lag] = _sumOfSquaredDifferences;
        }
    }

    private void FindPeak()
    {
        _peakIndex = 0;
        float maxAutocorrelation = 0f;

        for (int i = 0; i < _autocorrelation.Length / 2; i++)
        {
            if (_autocorrelation[i] > maxAutocorrelation)
            {
                maxAutocorrelation = _autocorrelation[i];
                _peakIndex = i;
            }
        }
    }
}
