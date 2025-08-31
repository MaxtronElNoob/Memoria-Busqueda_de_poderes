using UnityEngine;

public class AudioFunctions : MonoBehaviour
{
    private enum FunctionSelector
    {
        stop,
        start
    }
    [SerializeField] FunctionSelector selector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void MusicStart()
    {
        if (AudioManager.instance != null)
        {
            // AudioManager.instance.PlayTrack("Audio/Eternal_Love_by_Twisterium_Romantic_Music", true, 0, 9, 0.7f);
        }
        else
        {
            Debug.LogError("AudioManager no está inicializado en la escena.");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void MusicStop()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.StopTrack(0);
        }
        else
        {
            Debug.LogError("AudioManager no está inicializado en la escena.");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch (selector)
        {
            case FunctionSelector.stop:
                MusicStop();
                break;
            case FunctionSelector.start:
                MusicStart();
                break;
        }
    }
}
