using UnityEngine;
using UnityEngine.UI; // Para el Slider
using TMPro;

public class ControladorNavegacion : MonoBehaviour
{
    [Header("Vistas Principales")]
    public GameObject vistaInicio;
    public GameObject vistaLecciones;
    public GameObject vistaEscaner;
    public GameObject vistaAjustes;

    [Header("Barra de Navegación Inferior")]
    public GameObject barraNavegacion;

    [Header("UI Tarjeta Progreso")]
    public TextMeshProUGUI textoBotonAccion; // El que dirá "INICIAR" o "CONTINUAR"
    public TextMeshProUGUI textoProgreso; // "X de 6 actividades"
    public Slider barraProgreso; // La barrita visual

    [Header("Conexiones")]
    public ControladorMaestro maestro;

    private int nivelActualGuardado = 1; // Por defecto empezamos en el 1

    void Start()
    {
        // Leemos el progreso guardado en la memoria del teléfono. Si no hay, devuelve 1.
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
        // Al tocar "Escáner" en la barra inferior, podemos mandar al usuario a la vista de escáner en espera
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
        // Recalculamos por si el Maestro guardó un nuevo nivel
        nivelActualGuardado = PlayerPrefs.GetInt("NivelGuardado", 1);

        int escenariosCompletados = nivelActualGuardado - 1; // Si estás en el E1, has completado 0.

        // Actualizamos textos
        textoProgreso.text = escenariosCompletados + " de 6 actividades";
        barraProgreso.value = escenariosCompletados;

        // Lógica del botón INICIAR / CONTINUAR
        if (escenariosCompletados == 0)
        {
            textoBotonAccion.text = "INICIAR";
        }
        else if (escenariosCompletados >= 6)
        {
            textoBotonAccion.text = "REPETIR"; // Si ya acabó todo
            textoProgreso.text = "¡Misión Cumplida!";
        }
        else
        {
            textoBotonAccion.text = "CONTINUAR";
        }
    }

    // Se ejecuta al presionar el botón morado de la tarjeta "Detecta una estafa"
    public void BotonJugarPresionado()
    {
        // Apagamos los menús para ver la cámara
        ApagarTodasLasVistas();
        barraNavegacion.SetActive(false); // Ocultamos la barra inferior para que no estorbe en AR

        // Le decimos al Maestro que inicie en el nivel guardado
        int nivelParaJugar = PlayerPrefs.GetInt("NivelGuardado", 1);
        if (nivelParaJugar > 6) nivelParaJugar = 6; // Límite de seguridad

        maestro.CambiarEscenarioActivo(nivelParaJugar);
    }

    // Función para mostrar el menú desde otras partes (Ej. al terminar el Onboarding o picar un botón Back)
    public void MostrarMenuPrincipal()
    {
        barraNavegacion.SetActive(true);
        IrAInicio();
    }
}