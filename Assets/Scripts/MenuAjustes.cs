using UnityEngine;
using UnityEngine.UI;
using TMPro; // Si usas TextMeshPro

public class MenuAjustes : MonoBehaviour
{
    public TMP_Dropdown dropdownResolucion; // Dropdown para resoluciones
    public Toggle togglePantallaCompleta;   // Toggle para pantalla completa
    public Slider sliderVolumen;            // Slider de volumen
    public GameObject panelAjustes;         // El panel en sí

    Resolution[] resoluciones;

    void Start()
    {
        // --- Configurar Resoluciones ---
        resoluciones = Screen.resolutions;
        dropdownResolucion.ClearOptions();

        var opciones = new System.Collections.Generic.List<string>();
        int indiceResolucionActual = 0;

        for (int i = 0; i < resoluciones.Length; i++)
        {
            string opcion = resoluciones[i].width + " x " + resoluciones[i].height;
            opciones.Add(opcion);

            if (resoluciones[i].width == Screen.currentResolution.width &&
                resoluciones[i].height == Screen.currentResolution.height)
            {
                indiceResolucionActual = i;
            }
        }

        dropdownResolucion.AddOptions(opciones);
        dropdownResolucion.value = indiceResolucionActual;
        dropdownResolucion.RefreshShownValue();

        // Eventos
        dropdownResolucion.onValueChanged.AddListener(CambiarResolucion);
        togglePantallaCompleta.onValueChanged.AddListener(CambiarPantallaCompleta);
        sliderVolumen.onValueChanged.AddListener(CambiarVolumen);
    }

    void CambiarResolucion(int indice)
    {
        Resolution resolucion = resoluciones[indice];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }

    void CambiarPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    void CambiarVolumen(float volumen)
    {
        AudioListener.volume = volumen; // Controla volumen global
    }

    // Método para mostrar/ocultar panel
    public void MostrarPanel(bool mostrar)
    {
        panelAjustes.SetActive(mostrar);
    }
}
