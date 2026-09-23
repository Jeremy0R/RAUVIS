/* ==============================================================================
 * PROYECTO: RAUVIS (Realidad Aumentada para la detección de Phishing y Estafas)
 * SCRIPT: ControladorEscenario6.cs
 * DESCRIPCIÓN: Gestiona el 6to y último Escenario (Cadenas de Correos). 
 *              Incluye la lógica de decisión final, la pantalla de felicitación 
 *              y la integración completa de los AUDIOS de cierre.
 * ============================================================================== */

using UnityEngine;
using TMPro;

public class ControladorEscenario6 : MonoBehaviour
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
    public AudioClip audioFelicitacion; // NUEVO: Audio exclusivo para el final del juego

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
    public GameObject pantallaCorreoE6;
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

    [Header("Cierre del Juego")]
    public GameObject tarjetaFelicitacion;
    public TextMeshProUGUI tituloTarjetaFelicitacion;
    public TextMeshProUGUI textoTarjetaFelicitacion;

    // Sensores de estado
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
        tituloTarjetaBase.text = "LAS CADENAS DE MENTIRAS";
        textoTarjetaBase.text = "Algunos correos prometen milagros si los reenvías a 10 amigos. ¡Son engañosos para saturar tu bandeja!";

        // NUEVO: Reproducimos el audio base
        if (gestorAudio != null) gestorAudio.ReproducirVoz(audioBase);
    }

    public void BotonContinuarBase()
    {
        if (pasoActual == 0)
        {
            tarjetaBase.SetActive(false);
            tarjetaExplicacion.SetActive(true);

            tituloTarjetaExplicacion.text = "ROMPE LA CADENA";
            textoTarjetaExplicacion.text = "Este correo pide que lo reenvíes. ¿Qué debes hacer?";

            // NUEVO: Reproducimos la instrucción
            if (gestorAudio != null) gestorAudio.ReproducirVoz(audioExplicacion);

            pasoActual++;
        }
        else
        {
            // NUEVO: Callamos a Botty al entrar a la cámara
            if (gestorAudio != null) gestorAudio.DetenerVoz();

            tarjetaExplicacion.SetActive(false);
            grupoOpciones.SetActive(true);
            pasoActual++;
        }
    }

    // --- FUNCIONES DE ESCANEO (VUFORIA) ---
    public void ActivarCorreoYBotones()
    {
        if (pasoActual > 0 && !tarjetaAyuda.activeSelf && !tarjetaError.activeSelf && !tarjetaCorrecto.activeSelf && !tarjetaFelicitacion.activeSelf)
        {
            targetDetectado = true;
            pantallaCorreoE6.SetActive(true);
            grupoBotonesAccion.SetActive(true);
            grupoOpciones.SetActive(true);
        }
        else if (pasoActual > 0)
        {
            targetDetectado = true;
        }
    }

    public void DesactivarCorreoYBotones()
    {
        targetDetectado = false;
        pantallaCorreoE6.SetActive(false);
        grupoBotonesAccion.SetActive(false);
    }

    // --- INTERACCIÓN DE LOS BOTONES DE ACCIÓN (JUEGO) ---

    public void BotonReenviar()
    {
        OcultarElementosInteraccion();

        tarjetaError.SetActive(true);
        tituloTarjetaError.text = "¡CUIDADO!";
        textoTarjetaError.text = "Al reenviarlo, ayudas a los estafadores a engañar a tus seres queridos y llenas sus correos de basura.";
        if (textoBotonError != null) textoBotonError.text = "INTENTAR DE NUEVO";

        // NUEVO: Reproducimos el audio de error
        if (gestorAudio != null) gestorAudio.ReproducirVoz(audioError);
    }

    public void BotonBorrar()
    {
        OcultarElementosInteraccion();

        tarjetaCorrecto.SetActive(true);
        tituloTarjetaCorrecto.text = "¡CADENA ROTA!";
        textoTarjetaCorrecto.text = "¡Muy bien! Al borrar el correo, detuviste las mentiras y protegiste a tus contactos. ¡Tu bandeja está limpia y segura!";
        if (textoBotonCorrecto != null) textoBotonCorrecto.text = "CONTINUAR";

        // NUEVO: Reproducimos el audio de acierto
        if (gestorAudio != null) gestorAudio.ReproducirVoz(audioCorrecto);
    }

    // --- EL GRAN FINAL ---

    public void MostrarFelicitacion()
    {
        // NUEVO: Por si Botty seguía felicitando por el nivel, lo callamos antes del mensaje final
        if (gestorAudio != null) gestorAudio.DetenerVoz();

        OcultarTodo();
        tarjetaFelicitacion.SetActive(true);
        tituloTarjetaFelicitacion.text = "¡FELICITACIONES!";
        textoTarjetaFelicitacion.text = "La tecnología no tiene por qué ser difícil. Tómate tu tiempo al leer, confía en tu instinto y ¡navega seguro!";

        // NUEVO: Reproducimos el audio de felicitación final
        if (gestorAudio != null) gestorAudio.ReproducirVoz(audioFelicitacion);
    }

    public void BotonFinalizarJuego()
    {
        // NUEVO: Callamos la voz al presionar Finalizar
        if (gestorAudio != null) gestorAudio.DetenerVoz();

        OcultarTodo();
        if (maestro != null) maestro.CambiarEscenarioActivo(0);

        PlayerPrefs.SetInt("NivelGuardado", 7);
        PlayerPrefs.Save();

        if (navegacion != null) navegacion.MostrarMenuPrincipal();
    }

    // --- FUNCIONES AUXILIARES DE NAVEGACIÓN ---
    public void MostrarAyuda()
    {
        if (pantallaCorreoE6 != null) pantallaCorreoE6.SetActive(false);
        if (grupoBotonesAccion != null) grupoBotonesAccion.SetActive(false);

        tarjetaAyuda.SetActive(true);
        tituloTarjetaAyuda.text = "¡NO TE PREOCUPES!";
        textoTarjetaAyuda.text = "Escanea la imagen y selecciona el botón con la acción que consideres que te salva de los estafadores.";

        // NUEVO: Reproducimos el audio de ayuda
        if (gestorAudio != null) gestorAudio.ReproducirVoz(audioAyuda);
    }

    public void OcultarAyuda()
    {
        // NUEVO: Detenemos el audio si la cierra rápido
        if (gestorAudio != null) gestorAudio.DetenerVoz();

        tarjetaAyuda.SetActive(false);
        if (targetDetectado)
        {
            pantallaCorreoE6.SetActive(true);
            grupoBotonesAccion.SetActive(true);
        }
    }

    public void RestaurarDespuesDeError()
    {
        // NUEVO: Detenemos el audio de error
        if (gestorAudio != null) gestorAudio.DetenerVoz();

        tarjetaError.SetActive(false);
        grupoOpciones.SetActive(true);

        if (targetDetectado)
        {
            pantallaCorreoE6.SetActive(true);
            grupoBotonesAccion.SetActive(true);
        }
    }

    public void RegresarAlMenuPrincipal()
    {
        // NUEVO: Silenciamos todo al escapar del nivel
        if (gestorAudio != null) gestorAudio.DetenerVoz();

        OcultarTodo();
        if (maestro != null) maestro.CambiarEscenarioActivo(0);
        if (navegacion != null) navegacion.MostrarMenuPrincipal();
    }

    private void OcultarElementosInteraccion()
    {
        pantallaCorreoE6.SetActive(false);
        grupoBotonesAccion.SetActive(false);
        grupoOpciones.SetActive(false);
    }

    private void OcultarTodo()
    {
        tarjetaBase.SetActive(false);
        if (tarjetaExplicacion != null) tarjetaExplicacion.SetActive(false);
        tarjetaError.SetActive(false);
        tarjetaCorrecto.SetActive(false);
        tarjetaAyuda.SetActive(false);
        if (tarjetaFelicitacion != null) tarjetaFelicitacion.SetActive(false);
        if (pantallaCorreoE6 != null) pantallaCorreoE6.SetActive(false);
        if (grupoBotonesAccion != null) grupoBotonesAccion.SetActive(false);
        if (grupoOpciones != null) grupoOpciones.SetActive(false);
    }
}