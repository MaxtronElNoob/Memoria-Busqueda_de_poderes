using UnityEngine;

public class BehavoirNPC : MonoBehaviour
{
    public enum StateNPC
    {
        canInteract,
        Talking,
        cantInteract
    }
    [SerializeField] private GameObject visualCue;
    [SerializeField] public StateNPC status;
    // public DialogueNPC Dialogue;
    // [Header("Dialogue")]
    [SerializeField] private TextAsset Dialogue;

    private void Awake()
    {
        visualCue.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            visualCue.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            visualCue.SetActive(false);
        }
    }
    public void DialogueStart()
    {
        
    }
}
