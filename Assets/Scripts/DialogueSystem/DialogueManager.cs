using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class DialogueManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject namePanel; // Panel for character name
    public TextMeshProUGUI nameText; // Text for character name
    [Space]
    public GameObject dialogueObject;
    public TextMeshProUGUI dialogueText; // Text for dialogue
    [Space]
    public GameObject OptionsPanel; // Panel for options
    public GameObject OptionPrefab; // Prefab for options
    [Space]
    public GameObject Potrait; // Panel for character image
    // public GameObject TEST;
    [Space]
    [Header("Writing Configuration")]
    public float typingSpeed = 0.03f;
    private Queue<DialogueSOData> LineQueue; // Queue for dialogue Lines
    private DialogueSOData currentLine;
    private DialogueSO currentDialogue;

    void Start()
    {
        dialogueText.text = ""; // Limpia el texto antes de comenzar
        LineQueue = new Queue<DialogueSOData>(); // Inicializa la cola de oraciones
        //panelButton.onClick.AddListener(OnClick);
        // currentDialogue = FirstDialogue; // Asigna el primer diálogo
        if (currentDialogue == null)
        {
            Debug.LogError("No hay diálogos disponibles.");
            return;
        }
        StartDialogue(); // Inicia el diálogo con el diálogo actual
    }
    private void StartDialogue()
    {
        switch (currentDialogue.characterName)
        {
            case "":
                namePanel.SetActive(false); // Desactiva el panel de nombre si está vacío
                break;
            case "<name>":
                nameText.text = PlayerPrefs.GetString("PlayerName", "");
                namePanel.SetActive(true); // Activa el panel de nombre si no está vacío
                break;
            default:
                nameText.text = currentDialogue.characterName;
                namePanel.SetActive(true); // Activa el panel de nombre si no está vacío
                break;
        }
        // if (currentDialogue.characterName == "")
        // {
        // }
        // else
        // {
        //     nameText.text = currentDialogue.characterName;
        //     namePanel.SetActive(true); // Activa el panel de nombre si no está vacío
        // }

        // if (currentDialogue.characterImage != null)
        // {
        // ImagePanel.SetActive(currentDialogue.characterImage); // Activa el panel de imagen si no está vacío [Depricated]
        //     ImagePanel.GetComponent<Image>().sprite = currentDialogue.characterImage; // Asigna la imagen del personaje al panel
        //     ImagePanel.GetComponent<Image>().SetNativeSize(); // Ajusta el tamaño de la imagen al tamaño nativo
        // }
        // else
        // {
        //     ImagePanel.SetActive(false); // Desactiva el panel de imagen si está vacío
        // }
        LoadDialogue(currentDialogue); // Carga el diálogo
        currentLine = LineQueue.Dequeue(); // Obtiene la línea actual de la cola
        ShowLine(currentLine); // Muestra la primera línea del diálogo
    }
    private void LoadDialogue(DialogueSO dialogue)
    {
        foreach (DialogueSOData Line in dialogue.dialogueSecuence)
        {
            LineQueue.Enqueue(Line); // Agrega cada oración a la cola
        }
        Debug.Log("Diálogo cargado");
    }

    private void ShowLine(DialogueSOData dialogue)
    {
        // switch (dialogue.motive)
        // {
        //     case Music.Eterial:
        //         AudioManager.instance.PlayTrack("Audio/710545__ncone__slow-piano-loop",true,0,9,0.7f);
        //         break;
        //     case Music.Stop:
        //         AudioManager.instance.StopTrack(0);
        //         break;
        //     case Music.Relax:
        //         AudioManager.instance.PlayTrack("Audio/710546__ncone__soft-piano-loop",true,0,9,0.7f);
        //         break;
        //     case Music.Funny:
        //         AudioManager.instance.PlayTrack("Audio/512381__mrthenoronha__bass-line-theme-loop-2",true,0,9,0.7f);
        //         break;
        //     case Music.Mantener:
        //         break;
        // }
        Potrait.GetComponent<Animator>().SetInteger("Emotion", (int)dialogue.emotionType); // Inicia la animación del panel de diálogo
        if (currentDialogue.characterName != "<name>" && currentDialogue.characterName != ""){
            AudioManager.instance.PlaySoundEffect("SFX/747914__newlocknew__birdsong_pigeonscooing");
        }
        dialogueText.GetComponent<DialogueWriter>().WriteDialogue(dialogue.line); // Inicia el efecto de escritura
    }

    public void Interaction()
    {
        if (!dialogueText.GetComponent<DialogueWriter>().IsWriting)
        {
            if (LineQueue.Count > 0)
            {
                // Si hay más líneas en la cola, muestra la siguiente
                currentLine = LineQueue.Dequeue(); // Obtiene la siguiente línea de la cola
                ShowLine(currentLine); // Muestra la línea actual
            }
            else
            {
                if (currentDialogue.nextDialogueSecuenceOptions.Count > 0)
                {
                    // Si hay opciones, muestra el panel de opciones
                    Debug.Log("Hay opciones disponibles en el diálogo actual.");
                    if (!OptionsPanel.activeSelf)
                    {
                        DisplayOptions(); // Muestra las opciones del diálogo
                    }
                }
                else
                {
                    if (currentDialogue.nextDialogueWithoutOptions != null)
                    {
                        // Si no hay opciones, carga el siguiente diálogo
                        currentDialogue = currentDialogue.nextDialogueWithoutOptions; // Asigna el nuevo diálogo
                        StartDialogue(); // Inicia el nuevo diálogo
                    }
                    // else
                    // {
                    //     if (!finalDialogue)
                    //     {
                    //         finalDialogue = true; // Marca el diálogo como final
                    //         if (GoodEnding == null && BadEnding == null)
                    //         {
                    //             Debug.LogError("No hay diálogos de final disponibles.");
                    //             SceneManager.LoadScene(NextSceneWithoutPoints);
                    //             return;
                    //         }
                    //         else
                    //         {
                    //             if (score>=2)
                    //             {
                    //                 currentDialogue = GoodEnding; // Asigna el diálogo de final bueno
                    //             }
                    //             else
                    //             {
                    //                 currentDialogue = BadEnding; // Asigna el diálogo de final bueno
                    //             }
                    //             StartDialogue(); // Inicia el nuevo diálogo
                    //         }
                    //     }
                    //     else
                    //     {
                    //         if (score>=2)
                    //         {
                    //             Debug.Log("No hay más Dialogo en esta ruta. Procede al final bueno.");
                    //             SceneManager.LoadScene(GoodEndingScene);
                    //             //Se termina el juego
                    //         }
                    //         else
                    //         {
                    //             Debug.Log("No hay más Dialogo en esta ruta. Procede al final malo.");
                    //             SceneManager.LoadScene(BadEndingScene);
                    //             //Se termina el juego
                    //         }
                    //     }
                    // }
                }
            }
        }
    }
    private void DisplayOptions()
    {// Muestra las opciones del diálogo actual
        OptionsPanel.SetActive(true); // Activa el panel de opciones
        foreach (Option option in currentDialogue.nextDialogueSecuenceOptions)
        {
            GameObject optionButton = Instantiate(OptionPrefab, OptionsPanel.transform); // Variable para el botón de opción
            optionButton.GetComponentInChildren<TextMeshProUGUI>().text = option.optionText; // Asigna el texto de la opción
            Option capturedOption = option; // Captura la opción actual para usarla en el delegado
            optionButton.GetComponent<Button>().onClick.AddListener(delegate 
            {
                OnOptionSelected(capturedOption.nextDialogueWithOptions); 
            });
            // optionButton.GetComponent<Button>().onClick.AddListener(delegate { OnOptionSelected(option.nextDialogueWithOptions); }); // Asigna la función al botón
        }
    }
    private void OnOptionSelected(DialogueSO nextDialogue)
    { // Función para manejar la selección de opciones
        ClearOptions(); // Limpia las opciones anteriores
        currentDialogue = nextDialogue; // Asigna el nuevo diálogo
        StartDialogue(); // Inicia el nuevo diálogo
    }
    private void ClearOptions()
    { // Limpia las opciones del panel
        foreach (Transform child in OptionsPanel.transform)
        {
            Destroy(child.gameObject); // Destruye cada opción en el panel
        }
        OptionsPanel.SetActive(false); // Desactiva el panel de opciones
    }
    void Update()
    {
        var writer = dialogueText.GetComponent<DialogueWriter>();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (writer.IsWriting)
            {
                writer.CompleteText(); // Autocompleta si está escribiendo
            }
            else
            {
                Interaction(); // Si ya está completo, pasa al siguiente
            }
        }
    }
}
