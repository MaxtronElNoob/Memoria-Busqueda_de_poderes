using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueWriter : MonoBehaviour
{
    public bool IsWriting { get; private set; } = false;
    private TextMeshProUGUI textComponent; // Componente TextMeshProUGUI para mostrar el texto
    // private List<int> waveIndices = new List<int>(); // Índices de letras dentro de <wave>
    private string rawText = ""; // Texto sin etiquetas
    private Coroutine typingCoroutine;

    [Header("Variables")]
    [SerializeField] float typingSpeed = 0.05f;
    [SerializeField] float waitTime = 0.2f; // Frecuencia de la onda
    [Space]

    [Header("Efectos")]
    // Aun no funcionan
    [SerializeField] float waveVelocity = 2.5f;
    [SerializeField] float waveOffset = 0.3f;
    [SerializeField] float waveAmplitude = 0.04f;

    private void Awake()
    {
        textComponent = gameObject.GetComponent<TextMeshProUGUI>();
        if (textComponent == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on this GameObject.");
        }
    }

    // Función pública que comienza el efecto de escritura
    public void WriteDialogue(string dialogue)
    {
        Debug.Log($"Dialogue Given: {dialogue}"); // Mostrar el diálogo que se va a escribir
        rawText = ""; // Reiniciar el texto sin etiquetas
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        ;
        Debug.Log("Entering Preprocess"); // Mostrar que se entra a la función de preprocesamiento
        Preprocess(dialogue); // Preprocesar efectos
        typingCoroutine = StartCoroutine(TypewriterEffect(rawText)); // Llama al efecto de escritura con la velocidad especificada
        // return false; // Retorna true para indicar que el diálogo comenzó
    }

    // Coroutine que simula el efecto de escritura
    public IEnumerator TypewriterEffect(string dialogue)
    {
        IsWriting = true; // Marca que el diálogo está en proceso
        textComponent.text = ""; // Limpiar el texto antes de comenzar
        for (int i = 0; i < dialogue.Length; i++)
        {
            // Verificar si encontramos la etiqueta <wait>
            if (dialogue.Substring(i).StartsWith("<wait>"))
            {
                yield return new WaitForSeconds(waitTime);
                i += 6;
                continue;
            }
            textComponent.text += dialogue[i]; // Mostrar el texto actual
            yield return new WaitForSeconds(typingSpeed);
        }

        IsWriting = false; // Marca que el diálogo ha terminado
    }

    public void CompleteText()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        textComponent.text = rawText; // Muestra el texto completo procesado
        IsWriting = false;
    }

    // private void Update()
    // {
    //     // if (waveIndices.Count != 0) {
    //     //     ApplyWaveEffect(waveIndices);
    //     // };

    //     if (Input.GetKeyDown(KeyCode.Space) && IsWriting) // Si se presiona la barra espaciadora
    //     {
    //         StopAllCoroutines(); // Detener la escritura gradual
    //         textComponent.text = rawText; // Mostrar el texto completo
    //         IsWriting = false; // Marcar como terminado
    //     }
    // }

    // Función para aplicar el efecto de onda a los vértices específicos según los índices
    // private void ApplyWaveEffect(List<int> indices)
    // {
    //     textComponent.ForceMeshUpdate(); // Forzar la actualización de la malla para obtener los vértices actuales
    //     var textInfo=textComponent.textInfo; // Obtener la información del texto
    //     // Recorrer los índices de letras con el efecto de onda
    //     // for (int i = 0; i < indices.Count; i++)
    //     foreach (int indice in indices)
    //     {
    //         var charInfo = textInfo.characterInfo[indice]; // Obtener información del carácter
    //         int letterIndex = indice;
    //         // Recorremos los vértices de la letra (4 vértices por letra)
    //         for (int j = letterIndex * 4; j < (letterIndex * 4) + 4; j++)
    //         {
    //             // Movimiento ondulado, usando el seno para un movimiento centrado (arriba y abajo)
    //             float offset = Mathf.Sin(Time.time * waveVelocity + j * waveOffset ) * waveAmplitude; // Movimiento ondulado centrado
    //             vertices[j] = new Vector3(vertices[j].x, vertices[j].y + offset, vertices[j].z); // Modificar solo la Y
    //         }
    //     }

    //     // Después de aplicar el efecto, asignar los vértices modificados de vuelta a la malla
    //     textComponent.mesh.vertices = vertices;

    //     // Informamos a TextMeshPro que la malla ha cambiado para que se actualice visualmente
    //     textComponent.canvasRenderer.SetMesh(textComponent.mesh);
    // }

    private void Preprocess(string dialogue)
    {
        Debug.Log("Starting preprocessing"); // Mostrar el diálogo original
        // waveIndices.Clear();

        // bool isWaving = false;
        for (int i = 0; i < dialogue.Length; i++)
        {
            if (dialogue.Substring(i).StartsWith("<name>"))
            {
                rawText += PlayerPrefs.GetString("PlayerName", "");
                i += 5;
                continue;
            }
            // if (dialogue.Substring(i).StartsWith("<wave>"))
            // {
            //     isWaving = true;
            //     i += 5;
            //     continue;
            // }
            // else if (dialogue.Substring(i).StartsWith("</wave>"))
            // {
            //     isWaving = false;
            //     i += 6;
            //     continue;
            // }

            // if (isWaving)
            // {
            //     waveIndices.Add(rawText.Length - 1);
            // }

            rawText += dialogue[i];
        }
        Debug.Log($"rawText: {rawText}"); // Mostrar el texto preprocesado
        Debug.Log("Leaving Preprocess"); // Mostrar que se sale de la función de preprocesamiento
    }
}
