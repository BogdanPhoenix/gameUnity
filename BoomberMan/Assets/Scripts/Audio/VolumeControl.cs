using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Audio
{
    public class VolumeControl : MonoBehaviour
    {
        private const float Multiplier = 20f;
        private float _volumeValue;
    
        public string volumeParameter = "MasterVolume";
        public AudioMixer mixer;
        public Slider slider;

        private void Awake()
        {
            slider.onValueChanged.AddListener(HandleSliderValueChanged);
        }

        private void HandleSliderValueChanged(float value)
        {
            _volumeValue = CalculateValue(value);
            mixer.SetFloat(volumeParameter, _volumeValue);
        }

        private void Start()
        {
            _volumeValue = PlayerPrefs.GetFloat(volumeParameter, CalculateValue(slider.value));
            slider.value = Mathf.Pow(10f, _volumeValue / Multiplier);
        }

        private static float CalculateValue(float value)
        {
            return Mathf.Log10(value) * Multiplier;
        }
        
        private void OnDisable()
        {
            PlayerPrefs.SetFloat(volumeParameter, _volumeValue);
        }
        
        public static float CalculateMaxValue(string volumeParameter)
        {
            var mixerVolume = PlayerPrefs.GetFloat(volumeParameter);
            return Mathf.Pow(10f, mixerVolume / Multiplier);
        }
    }
}
