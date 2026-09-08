using UnityEngine;
using TMPro;

public class ControladorEscenario4 : MonoBehaviour
{
    [Header("Tarjetas Principales")]
    public GameObject tarjetaBase;
    public TextMeshProUGUI tituloTarjetaBase;
    public TextMeshProUGUI textoTarjetaBase;

    public GameObject tarjetaExplicacion;
    public TextMeshProUGUI tituloTarjetaExplicacion;
    public TextMeshProUGUI textoTarjetaExplicacion;

    [Header("Interfaz de Interacción")]
    public GameObject grupoOpciones; // Botones Back y Ayuda
    public GameObject pantallaCorreoE4; // La imagen del correo
    public GameObject grupoBotonesAccion; // Botones Enviar y Llamar

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
    private bool targetDetectado = false; // NUEVO: Sensor para saber si Vuforia está viendo la imagen

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
        tituloTarjetaBase.text = "UN FAVOR SOSPECHOSO";
        textoTarjetaBase.text = "Los estafadores juegan con tus emociones. Fingen ser un amigo o un familiar con una emergencia.";
    }

    public void BotonContinuarBase()
    {
        if (pasoActual == 0)
        {
            tarjetaBase.SetActive(false);
            tarjetaExplicacion.SetActive(true);

            tituloTarjetaExplicacion.text = "UN FAVOR SOSPECHOSO";
            textoTarjetaExplicacion.text = "Alguien te pide dinero urgente diciendo que no puede contestar llamadas. ¿Qué deberías hacer?";

            pasoActual++;
        }
        else
        {
            // Fase de Escaneo: Apagamos la explicación y ENCENDEMOS los botones morados
            tarjetaExplicacion.SetActive(false);
            grupoOpciones.SetActive(true);
            pasoActual++;
        }
    }

    // --- FUNCIONES DE ESCANEO (VUFORIA T_e4) ---
    public void ActivarCorreoYBotones()
    {
        if (pasoActual > 0)
        {
            targetDetectado = true; // Avisamos que la cámara ya vio el objetivo
            pantallaCorreoE4.SetActive(true);
            grupoBotonesAccion.SetActive(true);
            grupoOpciones.SetActive(true);
        }
    }

    public void DesactivarCorreoYBotones()
    {
        targetDetectado = false; // Avisamos que se perdió el objetivo
        pantallaCorreoE4.SetActive(false);
        grupoBotonesAccion.SetActive(false);
    }

    // --- INTERACCIÓN DE LOS BOTONES DE ACCIÓN ---
    public void BotonEnviarDinero()
    {
        pantallaCorreoE4.SetActive(false);
        grupoBotonesAccion.SetActive(false);
        grupoOpciones.SetActive(false); // Apagamos los morados para limpiar la pantalla

        tarjetaError.SetActive(true);
        tituloTarjetaError.text = "¡CUIDADO!";
        textoTarjetaError.text = "No actúes por miedo. Las emergencias falsas son trucos para robar tu dinero.";
        if (textoBotonError != null) textoBotonError.text = "INTENTAR DE NUEVO";
    }

    public void BotonRealizarLlamada()
    {
        pantallaCorreoE4.SetActive(false);
        grupoBotonesAccion.SetActive(false);
        grupoOpciones.SetActive(false); // Apagamos los morados

        tarjetaCorrecto.SetActive(true);
        tituloTarjetaCorrecto.text = "¡MUY INTELIGENTE!";
        textoTarjetaCorrecto.text = "Nunca envíes dinero solo por un correo. Siempre debes llamar a la persona a su teléfono conocido para comprobar si es verdad.";
        if (textoBotonCorrecto != null) textoBotonCorrecto.text = "CONTINUAR";
    }

    // --- FUNCIONES AUXILIARES ---
    public void MostrarAyuda()
    {
        // Limpiamos el fondo apagando el correo temporalmente (evita que se empalmen)
        if (pantallaCorreoE4 != null) pantallaCorreoE4.SetActive(false);
        if (grupoBotonesAccion != null) grupoBotonesAccion.SetActive(false);

        tarjetaAyuda.SetActive(true);
        tituloTarjetaAyuda.text = "¡NO TE PREOCUPES!";
        textoTarjetaAyuda.text = "Selecciona el botón con la acción que consideres correcta para este caso.";
    }

    public void OcultarAyuda()
    {
        tarjetaAyuda.SetActive(false);

        // Magia aquí: Solo devolvemos el correo a la pantalla SI la cámara sigue viendo el marcador
        if (targetDetectado)
        {
            pantallaCorreoE4.SetActive(true);
            grupoBotonesAccion.SetActive(true);
        }
    }

    public void RestaurarDespuesDeError()
    {
        tarjetaError.SetActive(false);
        grupoOpciones.SetActive(true); // Devolvemos los botones morados

        if (targetDetectado)
        {
            pantallaCorreoE4.SetActive(true);
            grupoBotonesAccion.SetActive(true);
        }
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