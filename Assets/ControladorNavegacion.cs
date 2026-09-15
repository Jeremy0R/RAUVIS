using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControladorNavegacion : MonoBehaviour
{
    [Header("Vistas Principales")]
    public GameObject contenedorVistas; // NUEVO: La carpeta padre "Vistas_Menu"
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

    [Header("Conexiones")]
    public ControladorMaestro maestro;

    private int nivelActualGuardado = 1;

    void Start()
    {
        // ¡NUEVO! Apagamos los menús al arrancar para que no estorben a la Bienvenida
        if (contenedorVistas != null) contenedorVistas.SetActive(false);
        if (barraNavegacion != null) barraNavegacion.SetActive(false);

        nivelActualGuardado = PlayerPrefs.GetInt("NivelGuardado", 1);
        ActualizarInterfazProgreso();
    }

    // --- FUNCIONES DE LA BARRA INFERIOR ---
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
    }

    public void IrAEscaner()
    {
        ApagarTodasLasVistas();
        vistaEscaner.SetActive(true);
    }

    public void IrAAjustes()
    {
        ApagarTodasLasVistas();
        vistaAjustes.SetActive(true);
    }

    private void ApagarTodasLasVistas()
    {
        vistaInicio.SetActive(false);
        vistaLecciones.SetActive(false);
        vistaEscaner.SetActive(false);
        vistaAjustes.SetActive(false);
    }

    // --- LÓGICA DE PROGRESO Y BOTÓN CENTRAL ---
    public void ActualizarInterfazProgreso()
    {
        nivelActualGuardado = PlayerPrefs.GetInt("NivelGuardado", 1);
        int escenariosCompletados = nivelActualGuardado - 1;

        textoProgreso.text = escenariosCompletados + " de 6 actividades";
        barraProgreso.value = escenariosCompletados;

        if (escenariosCompletados == 0)
        {
            textoBotonAccion.text = "INICIAR";
        }
        else if (escenariosCompletados >= 6)
        {
            textoBotonAccion.text = "REPETIR";
            textoProgreso.text = "¡Misión Cumplida!";
        }
        else
        {
            textoBotonAccion.text = "CONTINUAR";
        }
    }

    public void BotonJugarPresionado()
    {
        ApagarTodasLasVistas();
        barraNavegacion.SetActive(false);

        // ¡NUEVO! También apagamos el contenedor padre para que no estorbe la cámara AR
        if (contenedorVistas != null) contenedorVistas.SetActive(false);

        int nivelParaJugar = PlayerPrefs.GetInt("NivelGuardado", 1);
        if (nivelParaJugar > 6) nivelParaJugar = 6;

        maestro.CambiarEscenarioActivo(nivelParaJugar);
    }

    public void MostrarMenuPrincipal()
    {
        // ¡NUEVO! Encendemos la carpeta padre y la barra
        if (contenedorVistas != null) contenedorVistas.SetActive(true);
        if (barraNavegacion != null) barraNavegacion.SetActive(true);

        IrAInicio();
    }
}