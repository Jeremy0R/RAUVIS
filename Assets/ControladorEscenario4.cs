/* ==============================================================================
 * PROYECTO: RAUVIS (Realidad Aumentada para la detección de Phishing y Estafas)
 * SCRIPT: ControladorEscenario4.cs
 * DESCRIPCIÓN: Gestiona el Escenario 4 (Ingeniería Social / Favores Falsos).
 *              Controla el estado de detección del objetivo AR, los botones
 *              de acción condicionados y la integración de AUDIOS.
 * ============================================================================== */

using UnityEngine;
using TMPro;

public class ControladorEscenario4 : MonoBehaviour
{
    // --- CONEXIONES GLOBALES ---
    [Header("Conexiones del Sistema")]
    public ControladorNavegacion navegacion;
    public ControladorMaestro maestro;

    [Header("Conexión de Audio")]
    public ControladorAudio gestorAudio; // NUEVO: Cerebro de audios
    public AudioClip audioBase;
    public AudioClip audioExplicacion;
    public AudioClip audioError;
    public AudioClip audioCorrecto;
    public AudioClip audioAyuda;

    // --- VARIABLES DE INTERFAZ (TARJETAS) ---
    [Header("Tarjetas Principales")]
    public GameObject tarjetaBase;
    public TextMeshProUGUI tituloTarjetaBase;
    public TextMeshProUGUI textoTarjetaBase;

    public GameObject tarjetaExplicacion;
    public TextMeshProUGUI tituloTarjetaExplicacion;
    public TextMeshProUGUI textoTarjetaExplicacion;

    [Header("Interfaz de Interacción")]
    public GameObject grupoOpciones;
    public GameObject pantallaCorreoE4;
    public GameObject grupoBotonesAccion;

    [Header("Tarjetas de Retroalimentación")]
    public GameObject tarjetaError;
    public TextMeshProUGUI tituloTarjetaError;
    public TextMeshProUGUI textoTarjetaError;
    public TextMeshProUGUI textoBotonError;

    public GameObject tarjetaCorrecto;
    public TextMeshProUGUI tituloTarjetaCorrecto;
    public TextMeshProUGUI textoTarjetaCorrecto;
    public TextMeshProUGUI textoBotonCorrecto;

    public GameObject tarjetaAyuda;
    public TextMeshProUGUI tituloTarjetaAyuda;
    public TextMeshProUGUI textoTarjetaAyuda;

    // Sensores de estado del nivel
    private int pasoActual = 0;
    private bool targetDetectado = false;

    void Start()
    {
        OcultarTodo();
    }

    // --- FUNCIONES DE FLUJO (INTRODUCCIÓN) ---

    public void IniciarEscenario()
    {
        pasoActual = 0;
        targetDetectado = false;
        OcultarTodo();

        tarjetaBase.SetActive(true);
        tituloTarjetaBase.text = "UN FAVOR SOSPECHOSO";
        textoTarjetaBase.text = "Los estafadores juegan con tus emociones. Fingen ser un amigo o un familiar con una emergencia.";

        // NUEVO: Reproducimos el audio base
        if (gestorAudio != null) gestorAudio.ReproducirVoz(audioBase);
    }

    public void BotonContinuarBase()
    {
        if (pasoActual == 0)
        {
            tarjetaBase.SetActive(false);
            tarjetaExplicacion.SetActive(true);

            tituloTarjetaExplicacion.text = "UN FAVOR SOSPECHOSO";
            textoTarjetaExplicacion.text = "Alguien te pide dinero urgente diciendo que no puede contestar llamadas. ¿Qué deberías hacer?";

            // NUEVO: Reproducimos la instrucción
            if (gestorAudio != null) gestorAudio.ReproducirVoz(audioExplicacion);

            pasoActual++;
        }
        else
        {
            // NUEVO: Callamos a Botty si seguía hablando al entrar al escáner
            if (gestorAudio != null) gestorAudio.DetenerVoz();

            tarjetaExplicacion.SetActive(false);
            grupoOpciones.SetActive(true);
            pasoActual++;
        }
    }

    // --- FUNCIONES DE ESCANEO (VUFORIA) ---

    public void ActivarCorreoYBotones()
    {
        if (pasoActual > 0)
        {
            targetDetectado = true;
            pantallaCorreoE4.SetActive(true);
            grupoBotonesAccion.SetActive(true);
            grupoOpciones.SetActive(true);
        }
    }

    public void DesactivarCorreoYBotones()
    {
        targetDetectado = false;
        pantallaCorreoE4.SetActive(false);
        grupoBotonesAccion.SetActive(false);
    }

    // --- INTERACCIÓN DE LOS BOTONES DE ACCIÓN (JUEGO) ---

    public void BotonEnviarDinero()
    {
        pantallaCorreoE4.SetActive(false);
        grupoBotonesAccion.SetActive(false);
        grupoOpciones.SetActive(false);

        tarjetaError.SetActive(true);
        tituloTarjetaError.text = "¡CUIDADO!";
        textoTarjetaError.text = "No actúes por miedo. Las emergencias falsas son trucos para robar tu dinero.";
        if (textoBotonError != null) textoBotonError.text = "INTENTAR DE NUEVO";

        // NUEVO: Reproducimos el audio de error
        if (gestorAudio != null) gestorAudio.ReproducirVoz(audioError);
    }

    public void BotonRealizarLlamada()
    {
        pantallaCorreoE4.SetActive(false);
        grupoBotonesAccion.SetActive(false);
        grupoOpciones.SetActive(false);

        tarjetaCorrecto.SetActive(true);
        tituloTarjetaCorrecto.text = "¡MUY INTELIGENTE!";
        textoTarjetaCorrecto.text = "Nunca envíes dinero solo por un correo. Siempre debes llamar a la persona a su teléfono conocido para comprobar si es verdad.";
        if (textoBotonCorrecto != null) textoBotonCorrecto.text = "CONTINUAR";

        // NUEVO: Reproducimos el audio de acierto
        if (gestorAudio != null) gestorAudio.ReproducirVoz(audioCorrecto);
    }

    // --- FUNCIONES AUXILIARES DE NAVEGACIÓN ---

    public void MostrarAyuda()
    {
        if (pantallaCorreoE4 != null) pantallaCorreoE4.SetActive(false);
        if (grupoBotonesAccion != null) grupoBotonesAccion.SetActive(false);

        tarjetaAyuda.SetActive(true);
        tituloTarjetaAyuda.text = "¡NO TE PREOCUPES!";
        textoTarjetaAyuda.text = "Escanea la imagen y selecciona el botón con la acción que consideres correcta para este caso.";

        // NUEVO: Reproducimos el audio de ayuda
        if (gestorAudio != null) gestorAudio.ReproducirVoz(audioAyuda);
    }

    public void OcultarAyuda()
    {
        // NUEVO: Detenemos el audio si cerramos la ayuda rápido
        if (gestorAudio != null) gestorAudio.DetenerVoz();

        tarjetaAyuda.SetActive(false);

        if (targetDetectado)
        {
            pantallaCorreoE4.SetActive(true);
            grupoBotonesAccion.SetActive(true);
        }
    }

    public void RestaurarDespuesDeError()
    {
        // NUEVO: Silenciamos por si la explicación de error seguía reproduciéndose
        if (gestorAudio != null) gestorAudio.DetenerVoz();

        tarjetaError.SetActive(false);
        grupoOpciones.SetActive(true);

        if (targetDetectado)
        {
            pantallaCorreoE4.SetActive(true);
            grupoBotonesAccion.SetActive(true);
        }
    }

    public void RegresarAlMenuPrincipal()
    {
        // NUEVO: Detenemos a Botty inmediatamente al salir del nivel
        if (gestorAudio != null) gestorAudio.DetenerVoz();

        OcultarTodo();
        if (maestro != null) maestro.CambiarEscenarioActivo(0);
        if (navegacion != null) navegacion.MostrarMenuPrincipal();
    }

    private void OcultarTodo()
    {
        tarjetaBase.SetActive(false);
        if (tarjetaExplicacion != null) tarjetaExplicacion.SetActive(false);
        tarjetaError.SetActive(false);
        tarjetaCorrecto.SetActive(false);
        tarjetaAyuda.SetActive(false);
        if (pantallaCorreoE4 != null) pantallaCorreoE4.SetActive(false);
        if (grupoBotonesAccion != null) grupoBotonesAccion.SetActive(false);
        if (grupoOpciones != null) grupoOpciones.SetActive(false);
    }
}