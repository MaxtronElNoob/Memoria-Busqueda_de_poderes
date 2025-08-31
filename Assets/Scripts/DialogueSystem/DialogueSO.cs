using System.Collections.Generic;
using UnityEngine;

public enum EmotionType
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
public struct DialogueSOData
{
    public EmotionType emotionType;
    [TextArea(3, 10)]
    public string line;
    // public Music motive;
}

[System.Serializable]
public struct Option
{
    [TextArea(1, 10)]
    public string optionText;
    public DialogueSO nextDialogueWithOptions;
}

[CreateAssetMenu(fileName = "DialogueSys", menuName = "Scriptable Objects/DialogueSys")]
public class DialogueSO: ScriptableObject
{
    [Header("Character Info")]
    public string characterName;
    // public bool characterImage; //[Depricated]
    
    [SerializeField] public DialogueSOData[] dialogueSecuence;
    public List<Option> nextDialogueSecuenceOptions = new List<Option>();
    public DialogueSO nextDialogueWithoutOptions;
}
