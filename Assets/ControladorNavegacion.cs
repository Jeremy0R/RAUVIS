/* ==============================================================================
 * PROYECTO: RAUVIS (Realidad Aumentada para la detección de Phishing y Estafas)
 * SCRIPT: ControladorNavegacion.cs
 * DESCRIPCIÓN: Funciona como el "Enrutador Principal" de las vistas 2D (Menús).
 *              Gestiona la barra de navegación inferior, lee el progreso guardado
 *              en la memoria del dispositivo y bloquea/desbloquea dinámicamente 
 *              el acceso a las lecciones.
 * ============================================================================== */

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControladorNavegacion : MonoBehaviour
{
    // --- VARIABLES DE INTERFAZ ---
    [Header("Vistas Principales")]
    public GameObject contenedorVistas; // Carpeta padre que agrupa todos los paneles
    public GameObject vistaInicio;
    public GameObject vistaLecciones;
    public GameObject vistaEscaner;
    public GameObject vistaAjustes;

    [Header("Barra de Navegación Inferior")]
    public GameObject barraNavegacion;

    [Header("UI Tarjeta Progreso (Vista Inicio)")]
    public TextMeshProUGUI textoBotonAccion; // Cambia entre "INICIAR", "CONTINUAR" o "REPETIR"
    public TextMeshProUGUI textoProgreso;    // Texto dinámico: "X de 6 actividades"
    public Slider barraProgreso;             // Barra visual de llenado

    [Header("Botones de Lecciones")]
    public Button[] botonesLecciones; // Arreglo que contiene los 6 botones de la vista Lecciones

    [Header("Conexiones")]
    public ControladorMaestro maestro;

    // Almacena el nivel más alto al que ha llegado el usuario
    private int nivelActualGuardado = 1;

    void Start()
    {
        // Al arrancar la app, apagamos los menús para no interferir con la Bienvenida/Splash Screen
        if (contenedorVistas != null) contenedorVistas.SetActive(false);
        if (barraNavegacion != null) barraNavegacion.SetActive(false);

        ActualizarInterfazProgreso();
    }

    // ==============================================================================
    // FUNCIONES DE LA BARRA DE NAVEGACIÓN INFERIOR
    // ==============================================================================

    public void IrAInicio()
    {
        ApagarTodasLasVistas();
        vistaInicio.SetActive(true);
        ActualizarInterfazProgreso(); // Refresca la barra morada al volver
    }

    public void IrALecciones()
    {
        ApagarTodasLasVistas();
        vistaLecciones.SetActive(true);
        ActualizarInterfazProgreso(); // Refresca los candados de los botones al abrir
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

    // Función auxiliar para limpiar la pantalla antes de abrir una vista nueva
    private void ApagarTodasLasVistas()
    {
        vistaInicio.SetActive(false);
        vistaLecciones.SetActive(false);
        vistaEscaner.SetActive(false);
        vistaAjustes.SetActive(false);
    }

    // ==============================================================================
    // LÓGICA DE PROGRESO Y BLOQUEOS (PERSISTENCIA DE DATOS)
    // ==============================================================================

    public void ActualizarInterfazProgreso()
    {
        // Leemos la memoria del teléfono. Si no hay datos, por defecto devuelve 1.
        nivelActualGuardado = PlayerPrefs.GetInt("NivelGuardado", 1);
        int escenariosCompletados = nivelActualGuardado - 1;

        // Actualizamos la tarjeta de inicio
        textoProgreso.text = escenariosCompletados + " de 6 actividades";
        barraProgreso.value = escenariosCompletados;

        // Adaptamos el texto del botón principal según el estado del jugador
        if (escenariosCompletados == 0) textoBotonAccion.text = "INICIAR";
        else if (escenariosCompletados >= 6) { textoBotonAccion.text = "REPETIR"; textoProgreso.text = "¡Misión Cumplida!"; }
        else textoBotonAccion.text = "CONTINUAR";

        // Iteramos sobre los 6 botones de la vista Lecciones para bloquear o desbloquear
        for (int i = 0; i < botonesLecciones.Length; i++)
        {
            // El índice 'i' va de 0 a 5. El 'nivelActualGuardado' va de 1 a 6.
            if (i < nivelActualGuardado)
            {
                botonesLecciones[i].interactable = true; // Botón encendido y clickeable
            }
            else
            {
                botonesLecciones[i].interactable = false; // Botón apagado, gris y bloqueado
            }
        }
    }

    // ==============================================================================
    // LANZAMIENTO DE ESCENARIOS HACIA EL CONTROLADOR MAESTRO
    // ==============================================================================

    // Se ejecuta al presionar el botón morado de la tarjeta "Detecta una estafa"
    public void BotonJugarPresionado()
    {
        int nivelParaJugar = PlayerPrefs.GetInt("NivelGuardado", 1);
        if (nivelParaJugar > 6) nivelParaJugar = 6; // Tope de seguridad
        LanzarNivelAR(nivelParaJugar);
    }

    // Se ejecuta al tocar cualquier botón de la lista de Lecciones
    public void SeleccionarLeccion(int nivelElegido)
    {
        LanzarNivelAR(nivelElegido);
    }

    // Prepara la interfaz 2D para cederle el control a la cámara AR
    private void LanzarNivelAR(int nivel)
    {
        ApagarTodasLasVistas();
        barraNavegacion.SetActive(false);
        if (contenedorVistas != null) contenedorVistas.SetActive(false); // Apagamos el panel padre

        maestro.CambiarEscenarioActivo(nivel);
    }

    // ==============================================================================
    // FUNCIONES GLOBALES Y MANTENIMIENTO
    // ==============================================================================

    // Resetea el guardado a 1. Se usará desde la vista de Ajustes.
    public void ReiniciarProgreso()
    {
        PlayerPrefs.SetInt("NivelGuardado", 1);
        PlayerPrefs.Save();
        ActualizarInterfazProgreso();
    }

    // Llamada por la Bienvenida o el botón Back para encender los menús
    public void MostrarMenuPrincipal()
    {
        if (contenedorVistas != null) contenedorVistas.SetActive(true);
        if (barraNavegacion != null) barraNavegacion.SetActive(true);
        IrAInicio();
    }
}