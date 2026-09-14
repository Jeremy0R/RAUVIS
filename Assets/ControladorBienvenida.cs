using UnityEngine;
using TMPro;

public class ControladorBienvenida : MonoBehaviour
{
    [Header("Control de Fondo")]
    public GameObject panelFondoSolido; // Un panel de color que tapa la cámara de Vuforia

    [Header("Pantalla 0: Splash Screen")]
    public GameObject pantallaSplash;
    // Nota: El botón del Splash debe llamar a "AvanzarPaso()"

    [Header("Pantallas 1 al 6: Tarjeta Onboarding")]
    public GameObject pantallaTarjetas; // Contenedor de la Card_Base
    public TextMeshProUGUI tituloTarjeta;
    public TextMeshProUGUI contenidoTarjeta;
    public TextMeshProUGUI textoBotonContinuar; // Para cambiar entre "Continuar" y "Empezar"

    [Header("Elementos Visuales Centrales")]
    public GameObject bottyAvatar; // El modelo o imagen de Botty
    public GameObject animacionAgarre; // Animación de las dos manos
    public GameObject animacionCamara; // Animación del dedo no tapando la cámara
    public GameObject iconoAyuda; // El círculo morado con el '?'

    [Header("Conexión con el Maestro")]
    public ControladorMaestro maestro; // Para avisarle que empiece el Escenario 1

    private int pasoActual = 0;

    void Start()
    {
        // Al iniciar la app, forzamos el Splash Screen y tapamos la cámara
        if (panelFondoSolido != null) panelFondoSolido.SetActive(true);
        MostrarPaso(0);
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
        // Si el usuario presiona "Omitir", lo mandamos directo al final del onboarding
        pasoActual = 6;
        MostrarPaso(pasoActual);
    }

    private void MostrarPaso(int paso)
    {
        // Apagamos todo primero para limpiar la pantalla
        pantallaSplash.SetActive(false);
        pantallaTarjetas.SetActive(false);
        bottyAvatar.SetActive(false);
        animacionAgarre.SetActive(false);
        animacionCamara.SetActive(false);
        iconoAyuda.SetActive(false);

        switch (paso)
        {
            case 0: // SCREEN_One (Splash)
                pantallaSplash.SetActive(true);
                break;

            case 1: // BIENVENIDA
                pantallaTarjetas.SetActive(true);
                bottyAvatar.SetActive(true);
                tituloTarjeta.text = "Bienvenido a RAUVIS";
                contenidoTarjeta.text = "¡Hola!\nSoy BOTTY y seré tu asistente virtual en esta aventura.";
                textoBotonContinuar.text = "CONTINUAR";
                break;

            case 2: // BIENVENIDA_EXPLICACIÓN
                pantallaTarjetas.SetActive(true);
                bottyAvatar.SetActive(true);
                tituloTarjeta.text = "Bienvenido a RAUVIS";
                contenidoTarjeta.text = "Aprenderás a detectar correos estafadores y a proteger mejor tus datos.";
                textoBotonContinuar.text = "CONTINUAR";
                break;

            case 3: // BIENVENIDA_AGARRE
                pantallaTarjetas.SetActive(true);
                animacionAgarre.SetActive(true);
                tituloTarjeta.text = "¡Ponte cómodo!";
                contenidoTarjeta.text = "Sostén tu celular con ambas manos. Esto te ayudará a mantenerlo firme y evitará que te canses rápido.";
                textoBotonContinuar.text = "CONTINUAR";
                break;

            case 4: // BIENVENIDA_CÁMARA
                pantallaTarjetas.SetActive(true);
                animacionCamara.SetActive(true);
                tituloTarjeta.text = "Tu ventana al mundo";
                contenidoTarjeta.text = "Asegúrate de no cubrir la cámara con tus dedos. ¡La necesitaremos para ver la magia!";
                textoBotonContinuar.text = "CONTINUAR";
                break;

            case 5: // BIENVENIDA_AYUDA
                pantallaTarjetas.SetActive(true);
                iconoAyuda.SetActive(true);
                tituloTarjeta.text = "¿Estás perdido?";
                contenidoTarjeta.text = "Si necesitas ayuda, toca el botón de ayuda y yo apareceré.";
                textoBotonContinuar.text = "CONTINUAR";
                break;

            case 6: // BIENVENIDA_FIN
                pantallaTarjetas.SetActive(true);
                bottyAvatar.SetActive(true);
                tituloTarjeta.text = "¡Todo listo!";
                contenidoTarjeta.text = "Ve a tu propio ritmo sin miedo a equivocarte. Estamos aquí para aprender y divertirnos paso a paso.";
                textoBotonContinuar.text = "EMPEZAR"; // Cambia el texto del botón para el final
                break;
        }
    }

    private void FinalizarBienvenida()
    {
        // 1. Apagamos toda la UI de bienvenida
        gameObject.SetActive(false);

        // 2. Quitamos el panel sólido para revelar la cámara de Realidad Aumentada
        if (panelFondoSolido != null) panelFondoSolido.SetActive(false);

        // 3. Le avisamos al Controlador Maestro que inicie el Escenario 1
        maestro.CambiarEscenarioActivo(1);
    }
}