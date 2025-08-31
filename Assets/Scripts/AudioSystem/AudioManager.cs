using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    private const string SFX_PARENT_NAME = "SFX";
    private const string SFX_NAME_FORMAT = "SFX - [{0}]";
    public static AudioManager instance { get; private set; }
    [SerializeField] public const float TracktransitionTime = 1f;
    public AudioMixerGroup BGMixer;
    public AudioMixerGroup sfxMixer;
    private Transform sfxRoot;
    public Dictionary<int, AudioChannel> channels = new Dictionary<int, AudioChannel>();
    private void Awake()
    {
        if (instance == null)
        {
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
            return;
        }

        sfxRoot = new GameObject(SFX_PARENT_NAME).transform;
        sfxRoot.SetParent(transform);
    }
    public AudioSource PlaySoundEffect(string filepath)
    {
        AudioClip clip = Resources.Load<AudioClip>(filepath);
        if (clip == null) {
            Debug.LogError($"Could not load audio");
        }
        return PlaySoundEffect(clip);
    }
    public AudioSource PlaySoundEffect(AudioClip clip)
    {
        AudioSource effectSource = new GameObject(string.Format(SFX_NAME_FORMAT, clip.name)).AddComponent<AudioSource>();
        effectSource.transform.SetParent(sfxRoot);
        effectSource.transform.position = sfxRoot.position;

        effectSource.clip = clip;

        effectSource.Play();
        return effectSource;
    }
    public AudioTrack PlayTrack(string filepath, bool loop = true, int Channel = 0, float startingVolume = 0f, float volumeCap = 1f)
    {
        AudioClip clip = Resources.Load<AudioClip>(filepath);
        if (clip == null)
        {
            Debug.LogError("Audio clip not found at path: " + filepath);
            return null;
        }
        return PlayTrack(clip, loop, Channel, startingVolume, volumeCap, filepath);
    }
    public AudioTrack PlayTrack(AudioClip clip,bool loop = true,int Channel=0, float startingVolume = 0f, float volumeCap = 1f, string filepath="")
    {
        AudioChannel audioChannel = TryGetChannel(Channel,createIfDoesNotExist: true);
        AudioTrack track = audioChannel.PlayTrack(clip, loop, startingVolume, volumeCap, filepath);
        return track;
    }
    public AudioChannel TryGetChannel(int channelNumber,bool createIfDoesNotExist = false)
    {
        AudioChannel channel = null;
        if (channels.TryGetValue(channelNumber, out channel))
        {
            return channel;
        }
        else if (createIfDoesNotExist)
        {
            channel = new AudioChannel(channelNumber);
            channels.Add(channelNumber, channel);
            return channel;
        }
        return null;
    }
    public void StopTrack(int channel)
    {
        AudioChannel c= TryGetChannel(channel,createIfDoesNotExist:false);
        if(c==null)
        {
            return;
        }
        c.StopTrack();
    }
}
