using UnityEngine;
using TMPro;
using System.Collections;

public class ControladorBienvenida : MonoBehaviour
{
    [Header("Fondos Principales")]
    public GameObject panelSplashScreen;
    public GameObject panelInstrucciones;

    // Referencias a los Canvas Group para las transiciones
    private CanvasGroup splashCanvasGroup;
    private CanvasGroup instruccionesCanvasGroup;

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

    [Header("Conexión con el Maestro")]
    public ControladorMaestro maestro;
    public ControladorNavegacion navegacion;

    private int pasoActual = 0;
    private bool enTransicion = false; // Para evitar que el usuario toque botones mientras se anima

    void Start()
    {
        // Obtenemos los componentes Canvas Group que agregamos en el Inspector
        splashCanvasGroup = panelSplashScreen.GetComponent<CanvasGroup>();
        instruccionesCanvasGroup = panelInstrucciones.GetComponent<CanvasGroup>();

        // Estado inicial: Splash totalmente visible, Instrucciones invisibles y apagadas
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
        // Esperamos 4 segundos con el Splash intacto
        yield return new WaitForSeconds(4f);

        // Ejecutamos la transición suave hacia las instrucciones
        yield return StartCoroutine(TransicionSplashAInstrucciones());

        AvanzarPaso();
    }

    private IEnumerator TransicionSplashAInstrucciones()
    {
        enTransicion = true;

        // Encendemos el panel verde pero invisible
        panelInstrucciones.SetActive(true);

        float duracion = 1f; // La transición durará 1 segundo
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float progreso = tiempo / duracion;

            // El azul desaparece (de 1 a 0), el verde aparece (de 0 a 1)
            splashCanvasGroup.alpha = Mathf.Lerp(1f, 0f, progreso);
            instruccionesCanvasGroup.alpha = Mathf.Lerp(0f, 1f, progreso);

            yield return null;
        }

        // Aseguramos valores finales y apagamos el azul por completo
        splashCanvasGroup.alpha = 0f;
        instruccionesCanvasGroup.alpha = 1f;
        panelSplashScreen.SetActive(false);

        enTransicion = false;
    }

    public void AvanzarPaso()
    {
        if (enTransicion) return; // Candado de seguridad

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

    private IEnumerator TransicionSalidaAR()
    {
        enTransicion = true;
        float duracion = 0.5f; // Transición rápida de medio segundo para entrar al juego
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            instruccionesCanvasGroup.alpha = Mathf.Lerp(1f, 0f, tiempo / duracion);
            yield return null;
        }

        FinalizarBienvenida();
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
                break;
            case 2:
                tarjetaExplicacion.SetActive(true);
                tituloExplicacion.text = "Bienvenido a RAUVIS";
                contenidoExplicacion.text = "Aprenderás a detectar correos estafadores y a proteger mejor tus datos.";
                textoBotonExplicacion.text = "CONTINUAR";
                break;
            case 3:
                tarjetaBase.SetActive(true);
                animacionAgarre.SetActive(true);
                tituloBase.text = "¡Ponte cómodo!";
                contenidoBase.text = "Sostén tu celular con ambas manos. Esto te ayudará a mantenerlo firme y evitará que te canses rápido.";
                textoBotonBase.text = "CONTINUAR";
                break;
            case 4:
                tarjetaExplicacion.SetActive(true);
                animacionCamara.SetActive(true);
                tituloExplicacion.text = "Tu ventana al mundo";
                contenidoExplicacion.text = "Asegúrate de no cubrir la cámara con tus dedos. ¡La necesitaremos para ver la magia!";
                textoBotonExplicacion.text = "CONTINUAR";
                break;
            case 5:
                tarjetaBase.SetActive(true);
                iconoAyuda.SetActive(true);
                tituloBase.text = "¿Estás perdido?";
                contenidoBase.text = "Si necesitas ayuda, toca el botón de ayuda y yo apareceré.";
                textoBotonBase.text = "CONTINUAR";
                break;
            case 6:
                tarjetaExplicacion.SetActive(true);
                tituloExplicacion.text = "¡Todo listo!";
                contenidoExplicacion.text = "Ve a tu propio ritmo sin miedo a equivocarte. Estamos aquí para aprender y divertirnos paso a paso.";
                textoBotonExplicacion.text = "EMPEZAR";
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