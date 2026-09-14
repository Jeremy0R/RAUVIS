using UnityEngine;
using TMPro;
using System.Collections; // Necesario para la corrutina de tiempo

public class ControladorBienvenida : MonoBehaviour
{
    [Header("Fondos Principales")]
    public GameObject panelSplashScreen; // El fondo azul con el logo (SCREEN_One)
    public GameObject panelInstrucciones; // El fondo verde que se queda fijo

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
    public GameObject bottyAvatar;
    public GameObject animacionAgarre;
    public GameObject animacionCamara;
    public GameObject iconoAyuda;

    [Header("Conexión con el Maestro")]
    public ControladorMaestro maestro;

    private int pasoActual = 0;

    void Start()
    {
        // Iniciamos apagando el fondo verde y mostrando solo el Splash
        if (panelInstrucciones != null) panelInstrucciones.SetActive(false);
        if (panelSplashScreen != null) panelSplashScreen.SetActive(true);

        MostrarPaso(0);

        // Iniciamos el temporizador de 5 segundos
        StartCoroutine(RutinaSplashScreen());
    }

    private IEnumerator RutinaSplashScreen()
    {
        yield return new WaitForSeconds(5f);
        AvanzarPaso(); // Pasa automáticamente al paso 1 después de 5 segundos
    }

    public void AvanzarPaso()
    {
        pasoActual++;
        if (pasoActual > 6)
        {
            FinalizarBienvenida();
        }
        else
        {
            MostrarPaso(pasoActual);
        }
    }

    public void OmitirBienvenida()
    {
        // El botón omitir finaliza el onboarding directo y nos lleva al inicio/AR
        FinalizarBienvenida();
    }

    private void MostrarPaso(int paso)
    {
        // Limpiamos los elementos dinámicos en cada cambio
        tarjetaBase.SetActive(false);
        tarjetaExplicacion.SetActive(false);
        bottyAvatar.SetActive(false);
        animacionAgarre.SetActive(false);
        animacionCamara.SetActive(false);
        iconoAyuda.SetActive(false);

        switch (paso)
        {
            case 0:
                // El Splash Screen se maneja en el Start, aquí solo esperamos.
                break;

            case 1: // BIENVENIDA (Usa Tarjeta Base)
                panelSplashScreen.SetActive(false); // Apagamos el splash azul
                panelInstrucciones.SetActive(true); // Encendemos el fondo verde

                tarjetaBase.SetActive(true);
                bottyAvatar.SetActive(true);
                tituloBase.text = "Bienvenido a RAUVIS";
                contenidoBase.text = "¡Hola!\nSoy BOTTY y seré tu asistente virtual en esta aventura.";
                textoBotonBase.text = "CONTINUAR";
                break;

            case 2: // BIENVENIDA_EXPLICACIÓN (Usa Tarjeta Explicación)
                tarjetaExplicacion.SetActive(true);
                bottyAvatar.SetActive(true);
                tituloExplicacion.text = "Bienvenido a RAUVIS";
                contenidoExplicacion.text = "Aprenderás a detectar correos estafadores y a proteger mejor tus datos.";
                textoBotonExplicacion.text = "CONTINUAR";
                break;

            case 3: // BIENVENIDA_AGARRE (Usa Tarjeta Base)
                tarjetaBase.SetActive(true);
                animacionAgarre.SetActive(true);
                tituloBase.text = "¡Ponte cómodo!";
                contenidoBase.text = "Sostén tu celular con ambas manos. Esto te ayudará a mantenerlo firme y evitará que te canses rápido.";
                textoBotonBase.text = "CONTINUAR";
                break;

            case 4: // BIENVENIDA_CÁMARA (Usa Tarjeta Explicación)
                tarjetaExplicacion.SetActive(true);
                animacionCamara.SetActive(true);
                tituloExplicacion.text = "Tu ventana al mundo";
                contenidoExplicacion.text = "Asegúrate de no cubrir la cámara con tus dedos. ¡La necesitaremos para ver la magia!";
                textoBotonExplicacion.text = "CONTINUAR";
                break;

            case 5: // BIENVENIDA_AYUDA (Usa Tarjeta Base)
                tarjetaBase.SetActive(true);
                iconoAyuda.SetActive(true);
                tituloBase.text = "¿Estás perdido?";
                contenidoBase.text = "Si necesitas ayuda, toca el botón de ayuda y yo apareceré.";
                textoBotonBase.text = "CONTINUAR";
                break;

            case 6: // BIENVENIDA_FIN (Usa Tarjeta Explicación)
                tarjetaExplicacion.SetActive(true);
                bottyAvatar.SetActive(true);
                tituloExplicacion.text = "¡Todo listo!";
                contenidoExplicacion.text = "Ve a tu propio ritmo sin miedo a equivocarte. Estamos aquí para aprender y divertirnos paso a paso.";
                textoBotonExplicacion.text = "EMPEZAR";
                break;
        }
    }

    private void FinalizarBienvenida()
    {
        // Apagamos todo el onboarding
        gameObject.SetActive(false);
        if (panelInstrucciones != null) panelInstrucciones.SetActive(false);

        // Despertamos al Maestro para iniciar la experiencia en el Nivel 1
        maestro.CambiarEscenarioActivo(1);
    }
}