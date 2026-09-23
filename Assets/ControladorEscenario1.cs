/* ==============================================================================
 * PROYECTO: RAUVIS (Realidad Aumentada para la detección de Phishing y Estafas)
 * SCRIPT: ControladorEscenario1.cs
 * DESCRIPCIÓN: Gestiona el flujo, las tarjetas de interfaz (UI), la 
 *              retroalimentación específica y los AUDIOS del Escenario 1.
 * ============================================================================== */

using UnityEngine;
using TMPro;

public class ControladorEscenario1 : MonoBehaviour
{
    // --- CONEXIONES GLOBALES ---
    [Header("Conexiones del Sistema")]
    public ControladorNavegacion navegacion;
    public ControladorMaestro maestro;

    [Header("Conexión de Audio")]
    public ControladorAudio gestorAudio; // NUEVO: El cerebro de los audios
    public AudioClip audioBase;
    public AudioClip audioExplicacion;
    public AudioClip audioError;
    public AudioClip audioCorrecto;
    public AudioClip audioAyuda;

    // --- VARIABLES DE INTERFAZ (TARJETAS) ---
    [Header("Tarjetas de Bienvenida al Nivel")]
    public GameObject tarjetaBase;
    public TextMeshProUGUI tituloTarjetaBase;
    public TextMeshProUGUI textoTarjetaBase;

    public GameObject tarjetaExplicacion;
    public TextMeshProUGUI tituloTarjetaExplicacion;
    public TextMeshProUGUI textoTarjetaExplicacion;

    [Header("Opciones del Escenario")]
    public GameObject grupoOpciones;

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

    private int pasoActual = 0;

    void Start()
    {
        OcultarTodo();
    }

    // --- FUNCIONES (MÉTODOS / EVENTOS) ---

    public void IniciarEscenario()
    {
        pasoActual = 0;
        OcultarTodo();

        tarjetaBase.SetActive(true);
        tituloTarjetaBase.text = "LA LLAVE DE TU CASA";
        textoTarjetaBase.text = "¡Hola! Para tener un correo, necesitas una contraseña segura.";

        // NUEVO: Reproducimos el audio base
        if (gestorAudio != null) gestorAudio.ReproducirVoz(audioBase);
    }

    public void BotonContinuarBase()
    {
        if (pasoActual == 0)
        {
            tarjetaBase.SetActive(false);
            tarjetaExplicacion.SetActive(true);

            tituloTarjetaExplicacion.text = "LA LLAVE DE TU CASA";
            textoTarjetaExplicacion.text = "Toca la contraseña que creas que es la más difícil de adivinar para un ladrón.";

            // NUEVO: Reproducimos la instrucción
            if (gestorAudio != null) gestorAudio.ReproducirVoz(audioExplicacion);

            pasoActual++;
        }
        else
        {
            MostrarOpciones();
        }
    }

    public void MostrarOpciones()
    {
        OcultarTodo();
        grupoOpciones.SetActive(true);
    }

    public void SeleccionarOpcionIncorrecta()
    {
        OcultarTodo();
        tarjetaError.SetActive(true);
        tituloTarjetaError.text = "¡CUIDADO!";
        textoTarjetaError.text = "Esa es muy fácil de adivinar. ¡Intenta mezclar letras mayúsculas, números y símbolos!";

        // NUEVO: Reproducimos el audio de error
        if (gestorAudio != null) gestorAudio.ReproducirVoz(audioError);
    }

    public void SeleccionarOpcionCorrecta()
    {
        OcultarTodo();
        tarjetaCorrecto.SetActive(true);
        tituloTarjetaCorrecto.text = "¡CORRECTO!";
        textoTarjetaCorrecto.text = "Al mezclar mayúsculas, minúsculas, números y símbolos extraños. ¡Tu correo está protegido!";

        // NUEVO: Reproducimos el audio de acierto
        if (gestorAudio != null) gestorAudio.ReproducirVoz(audioCorrecto);
    }

    public void MostrarAyuda()
    {
        OcultarTodo();
        tarjetaAyuda.SetActive(true);
        tituloTarjetaAyuda.text = "¡NO TE PREOCUPES!";
        textoTarjetaAyuda.text = "Una contraseña segura debe contener números, letras y símbolos especiales.";

        // NUEVO: Reproducimos el audio de ayuda
        if (gestorAudio != null) gestorAudio.ReproducirVoz(audioAyuda);
    }

    public void RegresarAlMenuPrincipal()
    {
        // NUEVO: Detenemos a Botty inmediatamente si el usuario cancela el nivel
        if (gestorAudio != null) gestorAudio.DetenerVoz();

        OcultarTodo();
        if (maestro != null) maestro.CambiarEscenarioActivo(0);
        if (navegacion != null) navegacion.MostrarMenuPrincipal();
    }

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