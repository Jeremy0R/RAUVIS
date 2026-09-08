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
    public GameObject pantallaCorreoE4; // La imagen del correo estático
    public GameObject grupoBotonesAccion; // Botones "Enviar" y "Llamar"

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
            // Apagamos la explicación y dejamos la pantalla limpia esperando que escanee el target T_e4
            tarjetaExplicacion.SetActive(false);
        }
    }

    // --- FUNCIONES DE ESCANEO (VUFORIA T_e4) ---
    public void ActivarCorreoYBotones()
    {
        if (pasoActual > 0)
        {
            pantallaCorreoE4.SetActive(true);
            grupoBotonesAccion.SetActive(true);
            grupoOpciones.SetActive(true); // Enciende botones de Ayuda y Back
        }
    }

    public void DesactivarCorreoYBotones()
    {
        pantallaCorreoE4.SetActive(false);
        grupoBotonesAccion.SetActive(false);
        // Dejamos el grupoOpciones prendido por si quiere usar el botón Back
    }

    // --- INTERACCIÓN DE LOS BOTONES DE ACCIÓN ---
    public void BotonEnviarDinero() // Acción Incorrecta
    {
        pantallaCorreoE4.SetActive(false);
        grupoBotonesAccion.SetActive(false);

        tarjetaError.SetActive(true);
        tituloTarjetaError.text = "¡CUIDADO!";
        textoTarjetaError.text = "Si te dejas llevar por el susto, puedes perder tu dinero. Los ladrones inventan emergencias para que no pienses con claridad.";
    }

    public void BotonRealizarLlamada() // Acción Correcta
    {
        pantallaCorreoE4.SetActive(false);
        grupoBotonesAccion.SetActive(false);

        tarjetaCorrecto.SetActive(true);
        tituloTarjetaCorrecto.text = "¡MUY INTELIGENTE!";
        textoTarjetaCorrecto.text = "Nunca envíes dinero solo por un correo. Siempre debes llamar a la persona a su teléfono conocido para comprobar si es verdad.";
    }

    // --- FUNCIONES AUXILIARES ---
    public void MostrarAyuda()
    {
        pantallaCorreoE4.SetActive(false);
        grupoBotonesAccion.SetActive(false);

        tarjetaAyuda.SetActive(true);
        tituloTarjetaAyuda.text = "¡NO TE PREOCUPES!";
        textoTarjetaAyuda.text = "Selecciona el botón con la acción que consideres correcta para este caso.";
    }

    public void OcultarAyuda()
    {
        tarjetaAyuda.SetActive(false);
        // Volvemos a prender la interfaz de decisión
        pantallaCorreoE4.SetActive(true);
        grupoBotonesAccion.SetActive(true);
    }

    public void RestaurarDespuesDeError()
    {
        tarjetaError.SetActive(false);
        pantallaCorreoE4.SetActive(true);
        grupoBotonesAccion.SetActive(true);
    }

    private void OcultarTodo()
    {
        tarjetaBase.SetActive(false);
        if (tarjetaExplicacion != null) tarjetaExplicacion.SetActive(false);
        tarjetaError.SetActive(false);
        tarjetaCorrecto.SetActive(false);
        tarjetaAyuda.SetActive(false);
        pantallaCorreoE4.SetActive(false);
        grupoBotonesAccion.SetActive(false);
        if (grupoOpciones != null) grupoOpciones.SetActive(false);
    }
}