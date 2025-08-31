using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class AudioChannel
{
    public int channelIndex { get; private set; }
    public Transform trackContainer { get; private set; } = null;
    private const string TRACK_CONTAINER_NAME_FORMAT = "Chanel - [{0}]";
    private List<AudioTrack> tracks = new List<AudioTrack>();
    public AudioTrack activeTrack { get; private set; } = null;
    private AudioSource source; // Add this line to define the 'source' variable
    public bool isPlaying => source != null && source.isPlaying; // Ensure 'source' is not null before accessing
    Coroutine co_volumeLeveling = null;
    bool islevelingVolume => co_volumeLeveling != null;


    public AudioChannel(int channel)
    {
        channelIndex = channel;
        trackContainer = new GameObject(string.Format(TRACK_CONTAINER_NAME_FORMAT, channel)).transform;
        trackContainer.SetParent(AudioManager.instance.transform);

        // Initialize the AudioSource
        source = trackContainer.gameObject.AddComponent<AudioSource>();
    }

    public AudioTrack PlayTrack(AudioClip clip, bool loop = true, float startingVolume = 0f, float volumeCap = 1f, string filepath = "")
    {
        if (TryGetTrack(clip.name, out AudioTrack existingTrack))
        {
            if (!existingTrack.isPlaying) // Check if the track is not already playing
            {
                existingTrack.Play(); // Play the existing track if it's not playing
            }
            SetAsActiveTrack(existingTrack);
            return existingTrack;
        }
        AudioTrack track = new AudioTrack(this, clip, loop, startingVolume, volumeCap,AudioManager.instance.BGMixer);
        track.Play();
        SetAsActiveTrack(track);
        return track;
    }

    public bool TryGetTrack(string TrackName, out AudioTrack value)
    {
        TrackName = TrackName.ToLower();
        foreach (AudioTrack t in tracks)
        {
            if (t.name.ToLower() == TrackName)
            {
                value = t;
                return true;
            }
        }
        value = null;
        return false;
    }
    private void SetAsActiveTrack(AudioTrack track)
    {
        if (!tracks.Contains(track))
        {
            tracks.Add(track);
        }
        activeTrack = track;
        TryStartVolumeLeveling();
    }
    private void TryStartVolumeLeveling()
    {
        if(!islevelingVolume)
        {
            co_volumeLeveling = AudioManager.instance.StartCoroutine(volumeLeveling());
        }
    }
    private IEnumerator volumeLeveling()
    {
        while((activeTrack!=null && (tracks.Count>1||activeTrack.volume!=activeTrack.volumeCap)) || (activeTrack==null && tracks.Count>0))
        {
            for(int i = tracks.Count - 1; i >= 0; i--)
            {
                AudioTrack track = tracks[i];
                float targetVol=activeTrack==track?track.volumeCap:0;

                if (track.volume == targetVol)
                {
                    continue;
                }

                track.volume= Mathf.MoveTowards(track.volume, targetVol, Time.deltaTime * AudioManager.TracktransitionTime);

                if (track != activeTrack && track.volume == 0)
                {
                    DestroyTrack(track);
                }
            }
            yield return null;
        }
        co_volumeLeveling = null;
    }
    private void DestroyTrack(AudioTrack track)
    {
        if (tracks.Contains(track))
        {
            tracks.Remove(track);
        }
        Object.Destroy(track.root);
    }
    public void StopTrack()
    {
        if (activeTrack==null)
        {
            return;
        }
        activeTrack=null;
        TryStartVolumeLeveling();
    }
}
