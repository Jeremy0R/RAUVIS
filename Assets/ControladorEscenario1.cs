/* ==============================================================================
 * PROYECTO: RAUVIS (Realidad Aumentada para la detección de Phishing y Estafas)
 * SCRIPT: ControladorEscenario1.cs
 * DESCRIPCIÓN: Gestiona el flujo, las tarjetas de interfaz (UI) y la 
 *              retroalimentación específica del Escenario 1 (Contraseñas seguras).
 *              Controla cuándo aparecen los textos de Botty y las opciones.
 * ============================================================================== */

using UnityEngine;
using TMPro;

public class ControladorEscenario1 : MonoBehaviour
{
    // --- CONEXIONES GLOBALES ---
    [Header("Conexiones del Sistema")]
    public ControladorNavegacion navegacion; // Para poder volver al menú principal
    public ControladorMaestro maestro;       // Para controlar el estado del escáner

    // --- VARIABLES DE INTERFAZ (TARJETAS) ---
    [Header("Tarjetas de Bienvenida al Nivel")]
    public GameObject tarjetaBase;
    public TextMeshProUGUI tituloTarjetaBase;
    public TextMeshProUGUI textoTarjetaBase;

    public GameObject tarjetaExplicacion;
    public TextMeshProUGUI tituloTarjetaExplicacion;
    public TextMeshProUGUI textoTarjetaExplicacion;

    [Header("Opciones del Escenario")]
    public GameObject grupoOpciones; // Contiene los botones de contraseñas, ayuda y regresar

    [Header("Tarjetas de Retroalimentación")]
    public GameObject tarjetaError;
    public TextMeshProUGUI tituloTarjetaError;
    public TextMeshProUGUI textoTarjetaError;

    public GameObject tarjetaCorrecto;
    public TextMeshProUGUI tituloTarjetaCorrecto;
    public TextMeshProUGUI textoTarjetaCorrecto;

    public GameObject tarjetaAyuda;
    public TextMeshProUGUI tituloTarjetaAyuda;
    public TextMeshProUGUI textoTarjetaAyuda;

    [Header("Elementos Extra (Escáner)")]
    public GameObject grupoCorreos;
    public GameObject correoMalo;
    public GameObject correoBueno;
    public GameObject pantallaEscaner;

    // Controla en qué parte de la introducción del nivel estamos
    private int pasoActual = 0;

    void Start()
    {
        // Al iniciar la app, nos aseguramos de que este escenario esté invisible
        OcultarTodo();
    }

    // --- FUNCIONES (MÉTODOS / EVENTOS) ---

    // Se llama cuando el usuario o el Maestro activan este nivel
    public void IniciarEscenario()
    {
        pasoActual = 0;
        OcultarTodo();

        // Encendemos la primera tarjeta de introducción
        tarjetaBase.SetActive(true);
        tituloTarjetaBase.text = "LA LLAVE DE TU CASA";
        textoTarjetaBase.text = "¡Hola! Para tener un correo, necesitas una contraseña segura.";
    }

    // Avanza el diálogo inicial antes de mostrar las opciones
    public void BotonContinuarBase()
    {
        if (pasoActual == 0)
        {
            // Apagamos la base y pasamos a la explicación específica con Botty
            tarjetaBase.SetActive(false);
            tarjetaExplicacion.SetActive(true);

            tituloTarjetaExplicacion.text = "LA LLAVE DE TU CASA";
            textoTarjetaExplicacion.text = "Toca la contraseña que creas que es la más difícil de adivinar para un ladrón.";

            pasoActual++;
        }
        else
        {
            // Fin del diálogo: mostramos los botones para que el usuario juegue
            MostrarOpciones();
        }
    }

    // Muestra los botones de respuesta y oculta los diálogos
    public void MostrarOpciones()
    {
        OcultarTodo();
        grupoOpciones.SetActive(true);
    }

    // Se ejecuta al tocar la contraseña débil
    public void SeleccionarOpcionIncorrecta()
    {
        OcultarTodo();
        tarjetaError.SetActive(true);
        tituloTarjetaError.text = "¡CUIDADO!";
        textoTarjetaError.text = "Esa es muy fácil de adivinar. ¡Intenta mezclar letras mayúsculas, números y símbolos!";
    }

    // Se ejecuta al tocar la contraseña fuerte
    public void SeleccionarOpcionCorrecta()
    {
        OcultarTodo();
        tarjetaCorrecto.SetActive(true);
        tituloTarjetaCorrecto.text = "¡CORRECTO!";
        textoTarjetaCorrecto.text = "Al mezclar mayúsculas, minúsculas, números y símbolos extraños. ¡Tu correo está protegido!";
    }

    // Muestra un consejo si el usuario toca el botón de ayuda (?)
    public void MostrarAyuda()
    {
        OcultarTodo();
        tarjetaAyuda.SetActive(true);
        tituloTarjetaAyuda.text = "¡NO TE PREOCUPES!";
        textoTarjetaAyuda.text = "Una contraseña segura debe contener números, letras y símbolos especiales.";
    }

    // NUEVO: Se ejecuta al tocar el botón Atrás (<) para salir del nivel
    public void RegresarAlMenuPrincipal()
    {
        OcultarTodo(); // Limpiamos la pantalla
        if (maestro != null) maestro.CambiarEscenarioActivo(0); // Apagamos marcadores AR
        if (navegacion != null) navegacion.MostrarMenuPrincipal(); // Volvemos al inicio
    }

    // Función auxiliar para limpiar la pantalla (evita superposición de tarjetas)
    private void OcultarTodo()
    {
        tarjetaBase.SetActive(false);
        if (tarjetaExplicacion != null) tarjetaExplicacion.SetActive(false);
        grupoOpciones.SetActive(false);
        tarjetaError.SetActive(false);
        tarjetaCorrecto.SetActive(false);
        tarjetaAyuda.SetActive(false);

        if (grupoCorreos != null) grupoCorreos.SetActive(false);
        if (pantallaEscaner != null) pantallaEscaner.SetActive(false);
    }
}