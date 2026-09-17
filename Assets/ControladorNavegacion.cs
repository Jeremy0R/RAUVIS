using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControladorNavegacion : MonoBehaviour
{
    [Header("Vistas Principales")]
    public GameObject contenedorVistas;
    public GameObject vistaInicio;
    public GameObject vistaLecciones;
    public GameObject vistaEscaner;
    public GameObject vistaAjustes;

    [Header("Barra de Navegación Inferior")]
    public GameObject barraNavegacion;

    [Header("UI Tarjeta Progreso")]
    public TextMeshProUGUI textoBotonAccion;
    public TextMeshProUGUI textoProgreso;
    public Slider barraProgreso;

    [Header("Botones de Lecciones")]
    public Button[] botonesLecciones; // Arreglo para meter los 6 botones

    [Header("Conexiones")]
    public ControladorMaestro maestro;

    private int nivelActualGuardado = 1;

    void Start()
    {
        if (contenedorVistas != null) contenedorVistas.SetActive(false);
        if (barraNavegacion != null) barraNavegacion.SetActive(false);

        ActualizarInterfazProgreso();
    }

    // --- FUNCIONES DE NAVEGACIÓN ---
    public void IrAInicio()
    {
        ApagarTodasLasVistas();
        vistaInicio.SetActive(true);
        ActualizarInterfazProgreso();
    }

    public void IrALecciones()
    {
        ApagarTodasLasVistas();
        vistaLecciones.SetActive(true);
        ActualizarInterfazProgreso(); // Asegura que se bloqueen/desbloqueen al abrir
    }

    public void IrAEscaner() { ApagarTodasLasVistas(); vistaEscaner.SetActive(true); }
    public void IrAAjustes() { ApagarTodasLasVistas(); vistaAjustes.SetActive(true); }

    private void ApagarTodasLasVistas()
    {
        vistaInicio.SetActive(false);
        vistaLecciones.SetActive(false);
        vistaEscaner.SetActive(false);
        vistaAjustes.SetActive(false);
    }

    // --- LÓGICA DE PROGRESO Y BLOQUEOS ---
    public void ActualizarInterfazProgreso()
    {
        nivelActualGuardado = PlayerPrefs.GetInt("NivelGuardado", 1);
        int escenariosCompletados = nivelActualGuardado - 1;

        textoProgreso.text = escenariosCompletados + " de 6 actividades";
        barraProgreso.value = escenariosCompletados;

        if (escenariosCompletados == 0) textoBotonAccion.text = "INICIAR";
        else if (escenariosCompletados >= 6) { textoBotonAccion.text = "REPETIR"; textoProgreso.text = "¡Misión Cumplida!"; }
        else textoBotonAccion.text = "CONTINUAR";

        // Lógica de Bloqueo para los 6 botones
        for (int i = 0; i < botonesLecciones.Length; i++)
        {
            // Si el índice del botón (0 al 5) es menor al nivel guardado, se activa.
            if (i < nivelActualGuardado)
            {
                botonesLecciones[i].interactable = true;
            }
            else
            {
                botonesLecciones[i].interactable = false; // Se pone gris y no hace clic
            }
        }
    }

    // --- LANZAMIENTO DE ESCENARIOS ---
    public void BotonJugarPresionado() // Botón de la tarjeta morada (Continúa donde te quedaste)
    {
        int nivelParaJugar = PlayerPrefs.GetInt("NivelGuardado", 1);
        if (nivelParaJugar > 6) nivelParaJugar = 6;
        LanzarNivelAR(nivelParaJugar);
    }

    public void SeleccionarLeccion(int nivelElegido) // Botones de la lista 1 al 6
    {
        LanzarNivelAR(nivelElegido);
    }

    private void LanzarNivelAR(int nivel)
    {
        ApagarTodasLasVistas();
        barraNavegacion.SetActive(false);
        if (contenedorVistas != null) contenedorVistas.SetActive(false);

        maestro.CambiarEscenarioActivo(nivel);
    }

    // --- REINICIAR DATOS (Para la futura vista de Ajustes) ---
    public void ReiniciarProgreso()
    {
        PlayerPrefs.SetInt("NivelGuardado", 1);
        PlayerPrefs.Save();
        ActualizarInterfazProgreso();
    }

    public void MostrarMenuPrincipal()
    {
        if (contenedorVistas != null) contenedorVistas.SetActive(true);
        if (barraNavegacion != null) barraNavegacion.SetActive(true);
        IrAInicio();
    }
}