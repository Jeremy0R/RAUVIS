/* ==============================================================================
 * PROYECTO: RAUVIS (Realidad Aumentada para la detección de Phishing y Estafas)
 * SCRIPT: ControladorBienvenida.cs
 * DESCRIPCIÓN: Gestiona el Splash Screen y el tutorial con el asistente Botty. 
 *              Incluye transiciones suaves y la integración de AUDIOS.
 * ============================================================================== */

using UnityEngine;
using TMPro;
using System.Collections;

public class ControladorBienvenida : MonoBehaviour
{
    // --- VARIABLES DE FONDOS Y TRANSICIONES ---
    [Header("Fondos Principales")]
    public GameObject panelSplashScreen;
    public GameObject panelInstrucciones;

    private CanvasGroup splashCanvasGroup;
    private CanvasGroup instruccionesCanvasGroup;

    // --- VARIABLES DE INTERFAZ (TARJETAS) ---
    [Header("Tarjeta 1: Base")]
    public GameObject tarjetaBase;
    public TextMeshProUGUI tituloBase;
    public TextMeshProUGUI contenidoBase;
    public TextMeshProUGUI textoBotonBase;

    [Header("Tarjeta 2: Explicación (Alterna)")]
    public GameObject tarjetaExplicacion;
    public TextMeshProUGUI tituloExplicacion;
    public TextMeshProUGUI contenidoExplicacion;
    public TextMeshProUGUI textoBotonExplicacion;

    [Header("Elementos Visuales Centrales")]
    public GameObject animacionAgarre;
    public GameObject animacionCamara;
    public GameObject iconoAyuda;

    [Header("Conexiones Globales")]
    public ControladorMaestro maestro;
    public ControladorNavegacion navegacion;

    [Header("Conexión de Audio")]
    public ControladorAudio gestorAudio; // NUEVO
    public AudioClip audioPaso1;
    public AudioClip audioPaso2;
    public AudioClip audioPaso3;
    public AudioClip audioPaso4;
    public AudioClip audioPaso5;
    public AudioClip audioPaso6;

    private int pasoActual = 0;
    private bool enTransicion = false;

    void Start()
    {
        splashCanvasGroup = panelSplashScreen.GetComponent<CanvasGroup>();
        instruccionesCanvasGroup = panelInstrucciones.GetComponent<CanvasGroup>();

        if (panelInstrucciones != null)
        {
            panelInstrucciones.SetActive(false);
            instruccionesCanvasGroup.alpha = 0f;
        }

        if (panelSplashScreen != null)
        {
            panelSplashScreen.SetActive(true);
            splashCanvasGroup.alpha = 1f;
        }

        MostrarPaso(0);
        StartCoroutine(RutinaSplashScreen());
    }

    private IEnumerator RutinaSplashScreen()
    {
        yield return new WaitForSeconds(4f);
        yield return StartCoroutine(TransicionSplashAInstrucciones());
        AvanzarPaso();
    }

    private IEnumerator TransicionSplashAInstrucciones()
    {
        enTransicion = true;
        panelInstrucciones.SetActive(true);

        float duracion = 1f;
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float progreso = tiempo / duracion;

            splashCanvasGroup.alpha = Mathf.Lerp(1f, 0f, progreso);
            instruccionesCanvasGroup.alpha = Mathf.Lerp(0f, 1f, progreso);
            yield return null;
        }

        splashCanvasGroup.alpha = 0f;
        instruccionesCanvasGroup.alpha = 1f;
        panelSplashScreen.SetActive(false);
        enTransicion = false;
    }

    private IEnumerator TransicionSalidaAR()
    {
        // NUEVO: Silenciamos a Botty inmediatamente al salir del tutorial o darle omitir
        if (gestorAudio != null) gestorAudio.DetenerVoz();

        enTransicion = true;
        float duracion = 0.5f;
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            instruccionesCanvasGroup.alpha = Mathf.Lerp(1f, 0f, tiempo / duracion);
            yield return null;
        }

        FinalizarBienvenida();
    }

    public void AvanzarPaso()
    {
        if (enTransicion) return;

        pasoActual++;
        if (pasoActual > 6)
        {
            StartCoroutine(TransicionSalidaAR());
        }
        else
        {
            MostrarPaso(pasoActual);
        }
    }

    public void OmitirBienvenida()
    {
        if (enTransicion) return;
        StartCoroutine(TransicionSalidaAR());
    }

    private void MostrarPaso(int paso)
    {
        tarjetaBase.SetActive(false);
        tarjetaExplicacion.SetActive(false);
        animacionAgarre.SetActive(false);
        animacionCamara.SetActive(false);
        iconoAyuda.SetActive(false);

        switch (paso)
        {
            case 0:
                break;
            case 1:
                tarjetaBase.SetActive(true);
                tituloBase.text = "Bienvenido a RAUVIS";
                contenidoBase.text = "¡Hola!\nSoy BOTTY y seré tu asistente virtual en esta aventura.";
                textoBotonBase.text = "CONTINUAR";
                if (gestorAudio != null) gestorAudio.ReproducirVoz(audioPaso1); // NUEVO
                break;
            case 2:
                tarjetaExplicacion.SetActive(true);
                tituloExplicacion.text = "Bienvenido a RAUVIS";
                contenidoExplicacion.text = "Aprenderás a detectar correos estafadores y a proteger mejor tus datos.";
                textoBotonExplicacion.text = "CONTINUAR";
                if (gestorAudio != null) gestorAudio.ReproducirVoz(audioPaso2); // NUEVO
                break;
            case 3:
                tarjetaBase.SetActive(true);
                animacionAgarre.SetActive(true);
                tituloBase.text = "¡Ponte cómodo!";
                contenidoBase.text = "Sostén tu celular con ambas manos. Esto te ayudará a mantenerlo firme y evitará que te canses rápido.";
                textoBotonBase.text = "CONTINUAR";
                if (gestorAudio != null) gestorAudio.ReproducirVoz(audioPaso3); // NUEVO
                break;
            case 4:
                tarjetaExplicacion.SetActive(true);
                animacionCamara.SetActive(true);
                tituloExplicacion.text = "Tu ventana al mundo";
                contenidoExplicacion.text = "Asegúrate de no cubrir la cámara con tus dedos. ¡La necesitaremos para ver la magia!";
                textoBotonExplicacion.text = "CONTINUAR";
                if (gestorAudio != null) gestorAudio.ReproducirVoz(audioPaso4); // NUEVO
                break;
            case 5:
                tarjetaBase.SetActive(true);
                iconoAyuda.SetActive(true);
                tituloBase.text = "¿Estás perdido?";
                contenidoBase.text = "Si necesitas ayuda, toca el botón de ayuda y yo apareceré.";
                textoBotonBase.text = "CONTINUAR";
                if (gestorAudio != null) gestorAudio.ReproducirVoz(audioPaso5); // NUEVO
                break;
            case 6:
                tarjetaExplicacion.SetActive(true);
                tituloExplicacion.text = "¡Todo listo!";
                contenidoExplicacion.text = "Ve a tu propio ritmo sin miedo a equivocarte. Estamos aquí para aprender y divertirnos paso a paso.";
                textoBotonExplicacion.text = "EMPEZAR";
                if (gestorAudio != null) gestorAudio.ReproducirVoz(audioPaso6); // NUEVO
                break;
        }
    }

    private void FinalizarBienvenida()
    {
        gameObject.SetActive(false);
        if (panelInstrucciones != null) panelInstrucciones.SetActive(false);
        navegacion.MostrarMenuPrincipal();
    }
}