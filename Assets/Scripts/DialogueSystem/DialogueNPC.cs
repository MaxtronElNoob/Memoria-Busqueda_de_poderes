using System.Collections.Generic;
using UnityEngine;

public enum Emotion
{
    Neutral,
    Love,
    Sad,
    Angry
}
// [System.Serializable]
// public enum Music{
//     Mantener,
//     Relax,
//     Funny,
//     Eterial,
//     Stop
// }

[System.Serializable]
public struct DialogueNPCData
{
    public EmotionType emotionType;
    [TextArea(3, 10)]
    public string line;
    // public Music motive;
}

[System.Serializable]
public struct Alternatives
{
    [TextArea(1, 10)]
    public string optionText;
    public DialogueSO nextDialogueWithOptions;
}

[CreateAssetMenu(fileName = "DialogueSys", menuName = "Scriptable Objects/DialogueSys")]
public class DialogueNPC : ScriptableObject
{
    [Header("Character Info")]
    public string characterName;
    public Sprite characterImage;
    public DialogueNPCData Greeting;
    [SerializeField] public DialogueNPCData[] dialogueSecuence;
    public List<Alternatives> nextDialogueSecuenceOptions = new List<Alternatives>();
    public string nextgameScene;
    // public DialogueSO nextDialogueWithoutOptions;
}
