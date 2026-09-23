/* ==============================================================================
 * PROYECTO: RAUVIS (Realidad Aumentada para la detección de Phishing y Estafas)
 * SCRIPT: ControladorEscenario5.cs
 * DESCRIPCIÓN: Gestiona el Escenario 5 (Links Falsos). Controla la interacción 
 *              donde el usuario usa una "lupa" para revelar un enlace oculto,
 *              decide qué hacer, y controla la inyección de AUDIOS de Botty.
 * ============================================================================== */

using UnityEngine;
using TMPro;

public class ControladorEscenario5 : MonoBehaviour
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
    public GameObject pantallaLupa;
    public GameObject cardLinkSospechoso;
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
        tituloTarjetaBase.text = "EL PUENTE ENGAÑOSO";
        textoTarjetaBase.text = "A veces, un correo te promete un premio, pero te lleva a una página falsa y peligrosa.";

        // NUEVO: Reproducimos el audio base
        if (gestorAudio != null) gestorAudio.ReproducirVoz(audioBase);
    }

    public void BotonContinuarBase()
    {
        if (pasoActual == 0)
        {
            tarjetaBase.SetActive(false);
            tarjetaExplicacion.SetActive(true);

            tituloTarjetaExplicacion.text = "EL PUENTE ENGAÑOSO";
            textoTarjetaExplicacion.text = "Arrastra la lupa mágica sobre la tarjeta para ver a dónde te lleva realmente.";

            // NUEVO: Reproducimos la instrucción
            if (gestorAudio != null) gestorAudio.ReproducirVoz(audioExplicacion);

            pasoActual++;
        }
        else
        {
            // NUEVO: Callamos a Botty al entrar a la lupa
            if (gestorAudio != null) gestorAudio.DetenerVoz();

            tarjetaExplicacion.SetActive(false);
            grupoOpciones.SetActive(true);
            if (pantallaLupa != null) pantallaLupa.SetActive(true);
            pasoActual++;
        }
    }

    // --- FUNCIONES DE ESCANEO (VUFORIA) ---

    public void ActivarLinkYBotones()
    {
        if (pasoActual > 0 && !tarjetaAyuda.activeSelf && !tarjetaError.activeSelf && !tarjetaCorrecto.activeSelf)
        {
            targetDetectado = true;
            cardLinkSospechoso.SetActive(true);
            grupoBotonesAccion.SetActive(true);
            grupoOpciones.SetActive(true);

            if (pantallaLupa != null) pantallaLupa.SetActive(false);
        }
        else if (pasoActual > 0)
        {
            targetDetectado = true;
        }
    }

    public void DesactivarLinkYBotones()
    {
        targetDetectado = false;
        cardLinkSospechoso.SetActive(false);
        grupoBotonesAccion.SetActive(false);

        if (pasoActual > 0 && pantallaLupa != null && !tarjetaCorrecto.activeSelf && !tarjetaError.activeSelf)
        {
            pantallaLupa.SetActive(true);
        }
    }

    // --- INTERACCIÓN DE LOS BOTONES DE ACCIÓN (JUEGO) ---

    public void BotonVisitarSitio()
    {
        OcultarElementosInteraccion();

        tarjetaError.SetActive(true);
        tituloTarjetaError.text = "¡CUIDADO!";
        textoTarjetaError.text = "Si visitas un sitio web inseguro, los hackers pueden robar información personal de tu dispositivo.";
        if (textoBotonError != null) textoBotonError.text = "INTENTAR DE NUEVO";

        // NUEVO: Reproducimos el audio de error
        if (gestorAudio != null) gestorAudio.ReproducirVoz(audioError);
    }

    public void BotonBloquearSitio()
    {
        OcultarElementosInteraccion();

        tarjetaCorrecto.SetActive(true);
        tituloTarjetaCorrecto.text = "¡TRAMPA EVITADA!";
        textoTarjetaCorrecto.text = "Esa dirección es extraña y no es oficial. Si no la conoces mejor no entrar. ¡El correo ha sido bloqueado por tu seguridad!";
        if (textoBotonCorrecto != null) textoBotonCorrecto.text = "CONTINUAR";

        // NUEVO: Reproducimos el audio de acierto
        if (gestorAudio != null) gestorAudio.ReproducirVoz(audioCorrecto);
    }

    // --- FUNCIONES AUXILIARES DE NAVEGACIÓN ---

    public void MostrarAyuda()
    {
        if (cardLinkSospechoso != null) cardLinkSospechoso.SetActive(false);
        if (grupoBotonesAccion != null) grupoBotonesAccion.SetActive(false);
        if (pantallaLupa != null) pantallaLupa.SetActive(false);

        tarjetaAyuda.SetActive(true);
        tituloTarjetaAyuda.text = "¡NO TE PREOCUPES!";
        textoTarjetaAyuda.text = "Escanea la imagen y selecciona el botón con la acción que consideres correcta para este caso.";

        // NUEVO: Reproducimos el audio de ayuda
        if (gestorAudio != null) gestorAudio.ReproducirVoz(audioAyuda);
    }

    public void OcultarAyuda()
    {
        // NUEVO: Callamos la ayuda si el usuario vuelve a jugar rápido
        if (gestorAudio != null) gestorAudio.DetenerVoz();

        tarjetaAyuda.SetActive(false);
        if (targetDetectado)
        {
            cardLinkSospechoso.SetActive(true);
            grupoBotonesAccion.SetActive(true);
        }
        else if (pasoActual > 0)
        {
            if (pantallaLupa != null) pantallaLupa.SetActive(true);
        }
    }

    public void RestaurarDespuesDeError()
    {
        // NUEVO: Callamos la explicación del error
        if (gestorAudio != null) gestorAudio.DetenerVoz();

        tarjetaError.SetActive(false);
        grupoOpciones.SetActive(true);

        if (targetDetectado)
        {
            cardLinkSospechoso.SetActive(true);
            grupoBotonesAccion.SetActive(true);
        }
    }

    public void RegresarAlMenuPrincipal()
    {
        // NUEVO: Callamos cualquier voz si el usuario abandona el nivel
        if (gestorAudio != null) gestorAudio.DetenerVoz();

        OcultarTodo();
        if (maestro != null) maestro.CambiarEscenarioActivo(0);
        if (navegacion != null) navegacion.MostrarMenuPrincipal();
    }

    private void OcultarElementosInteraccion()
    {
        cardLinkSospechoso.SetActive(false);
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
        if (pantallaLupa != null) pantallaLupa.SetActive(false);
        if (cardLinkSospechoso != null) cardLinkSospechoso.SetActive(false);
        if (grupoBotonesAccion != null) grupoBotonesAccion.SetActive(false);
        if (grupoOpciones != null) grupoOpciones.SetActive(false);
    }
}