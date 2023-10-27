using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public List<AudioClip> Tracks;

        private AudioSource AudioSource;
        private int CurrentTrackIndex;

        private void Start()
        {
            CurrentTrackIndex = 0;
            AudioSource = GetComponent<AudioSource>();
            FirstSettingTracks();
            PlayNextTrack();
            InvokeRepeating(nameof(Update), 1f, 1f);
        }

        private void Update()
        {
            if (AudioSource.isPlaying) return;

            PlayNextTrack();
        }

        private void FirstSettingTracks()
        {
            AudioSource.Stop();
        }

        private void PlayNextTrack()
        {
            CurrentTrackIndex = Random.Range(0, Tracks.Count);
            AudioSource.PlayOneShot(Tracks[CurrentTrackIndex]);
        }
    }
}