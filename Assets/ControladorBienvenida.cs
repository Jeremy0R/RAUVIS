/* ==============================================================================
 * PROYECTO: RAUVIS (Realidad Aumentada para la detección de Phishing y Estafas)
 * SCRIPT: ControladorBienvenida.cs
 * DESCRIPCIÓN: Gestiona la pantalla de inicio (Splash Screen) y el tutorial 
 *              de Onboarding con el asistente Botty. Utiliza Corrutinas para 
 *              transiciones suaves (Fade In/Out) manejando el componente Alpha 
 *              de los Canvas Groups.
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

    // Componentes que permiten modificar la transparencia (Alpha) de todo un panel
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
    public ControladorNavegacion navegacion; // Le cederá el control al terminar

    // Control de flujo
    private int pasoActual = 0;
    private bool enTransicion = false; // Candado: evita clics mientras hay una animación en curso

    void Start()
    {
        // Vinculamos los CanvasGroup para manipular la opacidad
        splashCanvasGroup = panelSplashScreen.GetComponent<CanvasGroup>();
        instruccionesCanvasGroup = panelInstrucciones.GetComponent<CanvasGroup>();

        // Estado inicial rígido: El Splash azul es visible, el panel verde es invisible
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

        // Preparamos el paso 0 de las tarjetas y lanzamos el temporizador del Splash
        MostrarPaso(0);
        StartCoroutine(RutinaSplashScreen());
    }

    // ==============================================================================
    // CORRUTINAS (ANIMACIONES Y TIEMPOS)
    // ==============================================================================

    // Mantiene el logotipo en pantalla por 4 segundos
    private IEnumerator RutinaSplashScreen()
    {
        yield return new WaitForSeconds(4f);

        // Espera a que termine la animación de difuminado antes de continuar
        yield return StartCoroutine(TransicionSplashAInstrucciones());

        // Inicia el diálogo de Botty (Paso 1)
        AvanzarPaso();
    }

    // Realiza un "Crossfade" (Cruce mágico): El azul desaparece mientras el verde aparece
    private IEnumerator TransicionSplashAInstrucciones()
    {
        enTransicion = true;

        panelInstrucciones.SetActive(true); // Encendemos el panel, pero sigue invisible (Alpha 0)

        float duracion = 1f;
        float tiempo = 0f;

        // Bucle que se ejecuta frame por frame hasta que pasa 1 segundo
        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float progreso = tiempo / duracion;

            // Mathf.Lerp calcula valores intermedios. (ej. de 1 a 0 progresivamente)
            splashCanvasGroup.alpha = Mathf.Lerp(1f, 0f, progreso);
            instruccionesCanvasGroup.alpha = Mathf.Lerp(0f, 1f, progreso);

            yield return null; // Espera al siguiente frame
        }

        // Aseguramos que los valores queden exactos al terminar el bucle
        splashCanvasGroup.alpha = 0f;
        instruccionesCanvasGroup.alpha = 1f;
        panelSplashScreen.SetActive(false); // Apagamos el objeto azul para ahorrar memoria

        enTransicion = false;
    }

    // Desvanece el panel de instrucciones para descubrir la interfaz del juego
    private IEnumerator TransicionSalidaAR()
    {
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

    // ==============================================================================
    // CONTROL DEL TUTORIAL (MÁQUINA DE ESTADOS)
    // ==============================================================================

    // Avanza un paso en la lista de diálogos. Se invoca desde ControladorMaestro (BotonBasePresionado)
    public void AvanzarPaso()
    {
        if (enTransicion) return; // Si la pantalla se está moviendo, ignoramos el clic

        pasoActual++;
        if (pasoActual > 6)
        {
            StartCoroutine(TransicionSalidaAR()); // Fin del tutorial
        }
        else
        {
            MostrarPaso(pasoActual);
        }
    }

    // Permite al usuario saltarse el tutorial con el botón "Omitir"
    public void OmitirBienvenida()
    {
        if (enTransicion) return;
        StartCoroutine(TransicionSalidaAR());
    }

    // Configura qué tarjeta, texto e imagen de apoyo se muestra en cada momento
    private void MostrarPaso(int paso)
    {
        // Limpiamos la pantalla apagando todo
        tarjetaBase.SetActive(false);
        tarjetaExplicacion.SetActive(false);
        animacionAgarre.SetActive(false);
        animacionCamara.SetActive(false);
        iconoAyuda.SetActive(false);

        switch (paso)
        {
            case 0:
                // Estado inactivo durante el Splash Screen
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

    // Última acción: Apaga el controlador por completo y enciende el menú principal
    private void FinalizarBienvenida()
    {
        gameObject.SetActive(false);
        if (panelInstrucciones != null) panelInstrucciones.SetActive(false);

        navegacion.MostrarMenuPrincipal();
    }
}