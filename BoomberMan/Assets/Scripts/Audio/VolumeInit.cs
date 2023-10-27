using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class VolumeInit : MonoBehaviour
    {
        public string volumeParameter = "MasterVolume";
        public AudioMixer mixer;
    
        private void Start()
        {
            var volumeValue = PlayerPrefs.GetFloat(volumeParameter, volumeParameter == "MusicVolume" ? 0f : -40f);
            mixer.SetFloat(volumeParameter, volumeValue);
        }
    }
}
