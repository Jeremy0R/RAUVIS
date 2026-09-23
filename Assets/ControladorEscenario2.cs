/* ==============================================================================
 * PROYECTO: RAUVIS (Realidad Aumentada para la detección de Phishing y Estafas)
 * SCRIPT: ControladorEscenario2.cs
 * DESCRIPCIÓN: Gestiona la lógica del Escenario 2 (El Cartero Falso). 
 *              Controla la transición entre la introducción, el uso de la lupa 
 *              (escáner), la retroalimentación y los AUDIOS de Botty.
 * ============================================================================== */

using UnityEngine;
using TMPro;

public class ControladorEscenario2 : MonoBehaviour
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

    [Header("Elementos del Escáner (Lupa) y Correos")]
    public GameObject pantallaLupa;
    public GameObject mailCardMalo;
    public GameObject mailCardBueno;

    private int pasoActual = 0;

    void Start()
    {
        OcultarTodo();
    }

    // --- FUNCIONES DE FLUJO ---

    public void IniciarEscenario()
    {
        pasoActual = 0;
        OcultarTodo();

        tarjetaBase.SetActive(true);
        grupoOpciones.SetActive(false);

        tituloTarjetaBase.text = "EL CARTERO FALSO";
        textoTarjetaBase.text = "Alguien envió un correo del banco, pero los ladrones a veces se disfrazan.";

        // NUEVO: Reproducimos el audio base
        if (gestorAudio != null) gestorAudio.ReproducirVoz(audioBase);
    }

    public void BotonContinuarBase()
    {
        if (pasoActual == 0)
        {
            tarjetaBase.SetActive(false);
            tarjetaExplicacion.SetActive(true);

            tituloTarjetaExplicacion.text = "EL CARTERO FALSO";
            textoTarjetaExplicacion.text = "Usa la lupa para buscar los correos. Cuando los veas, toca el que creas que es falso.";

            // NUEVO: Reproducimos la instrucción de la lupa
            if (gestorAudio != null) gestorAudio.ReproducirVoz(audioExplicacion);

            pasoActual++;
        }
        else
        {
            // NUEVO: Detenemos a Botty si seguía hablando al entrar al escáner
            if (gestorAudio != null) gestorAudio.DetenerVoz();

            tarjetaBase.SetActive(false);
            tarjetaExplicacion.SetActive(false);
            grupoOpciones.SetActive(true);
            pantallaLupa.SetActive(true);
        }
    }

    // --- FUNCIONES DE DECISIÓN (JUEGO) ---

    public void SeleccionarCorreoBueno()
    {
        OcultarTodo();
        tarjetaError.SetActive(true);
        grupoOpciones.SetActive(true);

        tituloTarjetaError.text = "¡¡Revisa los detalles!!";
        textoTarjetaError.text = "Ese es un correo oficial. Revisa el otro. Fíjate que usa '@gmail' y trata de asustarte con 'urgencias'.";

        // NUEVO: Reproducimos el audio de error
        if (gestorAudio != null) gestorAudio.ReproducirVoz(audioError);
    }

    public void SeleccionarCorreoMalo()
    {
        OcultarTodo();
        tarjetaCorrecto.SetActive(true);
        grupoOpciones.SetActive(true);

        tituloTarjetaCorrecto.text = "¡CORRECTO!";
        textoTarjetaCorrecto.text = "Los bancos reales nunca usan '@gmail' ni te envían amenazas urgentes. ¡Esquivaste una trampa!";

        // NUEVO: Reproducimos el audio de acierto
        if (gestorAudio != null) gestorAudio.ReproducirVoz(audioCorrecto);
    }

    public void BotonIntentarDeNuevo()
    {
        // NUEVO: Detenemos a Botty para que haya silencio mientras vuelve a buscar
        if (gestorAudio != null) gestorAudio.DetenerVoz();

        tarjetaError.SetActive(false);
        tarjetaAyuda.SetActive(false);
        grupoOpciones.SetActive(true);
        pantallaLupa.SetActive(true);
    }

    // --- FUNCIONES AUXILIARES DE NAVEGACIÓN ---

    public void MostrarAyuda()
    {
        OcultarTodo();
        tarjetaAyuda.SetActive(true);
        tituloTarjetaAyuda.text = "NO TE PREOCUPES";
        textoTarjetaAyuda.text = "Uno de los correos quiere robar tus datos, analiza la dirección del usuario de cada uno.";

        // NUEVO: Reproducimos el audio de ayuda
        if (gestorAudio != null) gestorAudio.ReproducirVoz(audioAyuda);
    }

    public void RegresarAlMenuPrincipal()
    {
        // NUEVO: Callamos a Botty inmediatamente al salir
        if (gestorAudio != null) gestorAudio.DetenerVoz();

        OcultarTodo();
        if (maestro != null) maestro.CambiarEscenarioActivo(0);
        if (navegacion != null) navegacion.MostrarMenuPrincipal();
    }

    private void OcultarTodo()
    {
        tarjetaBase.SetActive(false);
        tarjetaExplicacion.SetActive(false);
        tarjetaError.SetActive(false);
        tarjetaCorrecto.SetActive(false);
        tarjetaAyuda.SetActive(false);
        pantallaLupa.SetActive(false);
        mailCardMalo.SetActive(false);
        mailCardBueno.SetActive(false);

        if (grupoOpciones != null) grupoOpciones.SetActive(false);
    }
}