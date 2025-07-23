using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesMenu : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("principal"); // Reemplaza por el nombre real de tu escena
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("El juego se cerraría aquí (solo funciona en build).");
    }
}
