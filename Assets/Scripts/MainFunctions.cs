using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para manejar escenas

public class MainFunctions : MonoBehaviour
{
    // Funcion para cerrar el Juego
    public void cerrarJuego()
    {
        Debug.Log("Cerrando Juego"); // Log message for debugging purposes
        Application.Quit(); // Close the application
    }
    // Funcion para cambiar de escena
    public void ChangeScene(string sceneName)
    {
        Debug.Log("Cambiando de escena");
        SceneManager.LoadScene(sceneName);
    }
}
