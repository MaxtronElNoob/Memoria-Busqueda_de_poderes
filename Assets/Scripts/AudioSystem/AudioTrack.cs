using UnityEngine;
using UnityEngine.Audio;

public class AudioTrack
{
    private const string TRACK_CONTAINER_NAME_FORMAT = "Track - [{0}]";
    public string name { get; private set; } = null;
    private AudioChannel channel;
    private AudioSource source;
    public GameObject root => source.gameObject;
    public bool loop =>source.loop;
    public float volumeCap { get; private set; }
    public bool isPlaying => source.isPlaying;
    public float volume { get {return source.volume;} set {source.volume = value;}}
    public AudioTrack(AudioChannel channel, AudioClip clip, bool loop, float startingVolume, float volumeCap,AudioMixerGroup mixer)
    {
        name= clip.name;
        this.channel = channel;
        this.volumeCap = volumeCap;

        source = CreateSource();
        source.clip = clip;
        source.loop = loop;
        source.volume = startingVolume;

        source.outputAudioMixerGroup = mixer;
    }
    private AudioSource CreateSource()
    {
        GameObject audioObject = new GameObject(string.Format(TRACK_CONTAINER_NAME_FORMAT, name));
        audioObject.transform.SetParent(channel.trackContainer);
        AudioSource source = audioObject.AddComponent<AudioSource>();
        return source;
    }
    public void Play()
    {
        source.Play();
    }
    public void Stop()
    {
        source.Stop();
    }
}
