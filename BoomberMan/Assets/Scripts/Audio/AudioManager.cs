using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private int currentTrackIndex;
    private AudioSource AudioSource;
    
    public List<AudioClip> Tracks;

    [Range(0, 1)]
    public float maxVolume;

    private void Start()
    {
        currentTrackIndex = 0;
        AudioSource = GetComponent<AudioSource>();
        FirstSettingTracks();
        PlayNextTrack();
        InvokeRepeating(nameof(Update), 1f, 1f);
    }

    private void FirstSettingTracks()
    {
        foreach (var track in Tracks)
        {
            AudioSource.Stop();
            AudioSource.volume = maxVolume;
        }
    }

    private void Update()
    {
        if (AudioSource.isPlaying) return;
        
        PlayNextTrack();
    }

    private void PlayNextTrack()
    {
        currentTrackIndex = Random.Range(0, Tracks.Count);
        AudioSource.PlayOneShot(Tracks[currentTrackIndex]);
    }
}
