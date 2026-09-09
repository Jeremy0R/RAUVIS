using UnityEngine;
using TMPro;

public class ControladorEscenario5 : MonoBehaviour
{
    [Header("Tarjetas Principales")]
    public GameObject tarjetaBase;
    public TextMeshProUGUI tituloTarjetaBase;
    public TextMeshProUGUI textoTarjetaBase;

    public GameObject tarjetaExplicacion;
    public TextMeshProUGUI tituloTarjetaExplicacion;
    public TextMeshProUGUI textoTarjetaExplicacion;

    [Header("Interfaz de Interacción")]
    public GameObject grupoOpciones; // Botones Back y Ayuda (los morados)
    public GameObject pantallaLupa; // El marco de la lupa (si aplica)
    public GameObject cardLinkSospechoso; // La tarjeta que revela el link falso
    public GameObject grupoBotonesAccion; // Botones "Visitar Sitio" y "Bloquear"

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

    private int pasoActual = 0;
    private bool targetDetectado = false;

    void Start()
    {
        OcultarTodo();
    }

    // --- FUNCIONES DE FLUJO ---
    public void IniciarEscenario()
    {
        pasoActual = 0;
        targetDetectado = false;
        OcultarTodo();

        tarjetaBase.SetActive(true);
        tituloTarjetaBase.text = "EL PUENTE ENGAÑOSO";
        textoTarjetaBase.text = "A veces, un correo te promete un premio, pero te lleva a una página falsa y peligrosa.";
    }

    public void BotonContinuarBase()
    {
        if (pasoActual == 0)
        {
            tarjetaBase.SetActive(false);
            tarjetaExplicacion.SetActive(true);

            tituloTarjetaExplicacion.text = "EL PUENTE ENGAÑOSO";
            textoTarjetaExplicacion.text = "Arrastra la lupa mágica sobre el botón para ver a dónde te lleva realmente.";

            pasoActual++;
        }
        else
        {
            // Apagamos explicación y prendemos los botones morados y la interfaz de la lupa
            tarjetaExplicacion.SetActive(false);
            grupoOpciones.SetActive(true);
            if (pantallaLupa != null) pantallaLupa.SetActive(true);
            pasoActual++;
        }
    }

    // --- FUNCIONES DE ESCANEO (VUFORIA target_E5) ---
    public void ActivarLinkYBotones()
    {
        if (pasoActual > 0)
        {
            targetDetectado = true;
            cardLinkSospechoso.SetActive(true);
            grupoBotonesAccion.SetActive(true);
            grupoOpciones.SetActive(true);
        }
    }

    public void DesactivarLinkYBotones()
    {
        targetDetectado = false;
        cardLinkSospechoso.SetActive(false);
        grupoBotonesAccion.SetActive(false);
    }

    // --- INTERACCIÓN DE LOS BOTONES DE ACCIÓN ---
    public void BotonVisitarSitio() // Acción Incorrecta
    {
        OcultarElementosInteraccion();

        tarjetaError.SetActive(true);
        tituloTarjetaError.text = "¡CUIDADO!";
        textoTarjetaError.text = "Si visitas un sitio web inseguro, los hackers pueden robar información personal de tu dispositivo.";
        if (textoBotonError != null) textoBotonError.text = "INTENTAR DE NUEVO";
    }

    public void BotonBloquearSitio() // Acción Correcta
    {
        OcultarElementosInteraccion();

        tarjetaCorrecto.SetActive(true);
        tituloTarjetaCorrecto.text = "¡TRAMPA EVITADA!";
        textoTarjetaCorrecto.text = "Esa dirección es extraña y no es oficial. Si no la conoces mejor no entrar. ¡El botón ha sido bloqueado por tu seguridad!";
        if (textoBotonCorrecto != null) textoBotonCorrecto.text = "CONTINUAR";
    }

    // --- FUNCIONES AUXILIARES ---
    public void MostrarAyuda()
    {
        if (cardLinkSospechoso != null) cardLinkSospechoso.SetActive(false);
        if (grupoBotonesAccion != null) grupoBotonesAccion.SetActive(false);

        tarjetaAyuda.SetActive(true);
        tituloTarjetaAyuda.text = "¡NO TE PREOCUPES!";
        textoTarjetaAyuda.text = "Selecciona el botón con la acción que consideres correcta para este caso.";
    }

    public void OcultarAyuda()
    {
        tarjetaAyuda.SetActive(false);
        if (targetDetectado)
        {
            cardLinkSospechoso.SetActive(true);
            grupoBotonesAccion.SetActive(true);
        }
    }

    public void RestaurarDespuesDeError()
    {
        tarjetaError.SetActive(false);
        grupoOpciones.SetActive(true);

        if (targetDetectado)
        {
            cardLinkSospechoso.SetActive(true);
            grupoBotonesAccion.SetActive(true);
        }
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