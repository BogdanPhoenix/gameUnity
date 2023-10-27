using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioManagerEnemy : MonoBehaviour
    {
        private const string TagPlayer = "Player";
        private const float MinVolume = 0.0001f;
        
        private AudioSource AudioSource;
        private GameObject Player;
        private int CurrentTrackIndex;
        
        public List<AudioClip> tracks;
        public float maxDistance = 6f;
        
        private void Start()
        {
            AudioSource = GetComponent<AudioSource>();
            AudioSource.minDistance = 0;
            AudioSource.maxDistance = maxDistance;
            
            CurrentTrackIndex = 0;
            FirstSettingTracks();
            PlayNextTrack();
            InvokeRepeating(nameof(Update), 1f, 1f);
        }

        private void Update()
        {
            UpdateTrack();
            UpdateDistance();
        }

        private void UpdateTrack()
        {
            if (AudioSource.isPlaying) return;

            PlayNextTrack();
        }

        private void UpdateDistance()
        {
            Player = GameObject.FindGameObjectWithTag(TagPlayer);
            if (Player == null) return;

            var maxVolume = VolumeControl.CalculateMaxValue("SoundVolume");
            
            var distanceToPlayer = Vector2.Distance(transform.position, Player.transform.position);
            var distanceRatio = Mathf.Clamp01(distanceToPlayer / maxDistance);
            var targetVolume = Mathf.Lerp(MinVolume, maxVolume, 1f - distanceRatio);

            AudioSource.volume = targetVolume;
        }
        
        private void FirstSettingTracks()
        {
            AudioSource.Stop();
            AudioSource.volume = MinVolume;
        }

        private void PlayNextTrack()
        {
            CurrentTrackIndex = Random.Range(0, tracks.Count);
            AudioSource.PlayOneShot(tracks[CurrentTrackIndex]);
        }
    }
}