/* ==============================================================================
 * PROYECTO: RAUVIS (Realidad Aumentada para la detección de Phishing y Estafas)
 * SCRIPT: ControladorEscenario4.cs
 * DESCRIPCIÓN: Gestiona el Escenario 4 (Ingeniería Social / Favores Falsos).
 *              Controla el estado de detección del objetivo AR y despliega
 *              botones de acción (Enviar Dinero / Llamar) condicionados a la cámara.
 * ============================================================================== */

using UnityEngine;
using TMPro;

public class ControladorEscenario4 : MonoBehaviour
{
    // --- CONEXIONES GLOBALES ---
    [Header("Conexiones del Sistema")]
    public ControladorNavegacion navegacion; // NUEVO: Permite regresar al Hub principal
    public ControladorMaestro maestro;       // NUEVO: Controla el estado general y el AR

    // --- VARIABLES DE INTERFAZ (TARJETAS) ---
    [Header("Tarjetas Principales")]
    public GameObject tarjetaBase;
    public TextMeshProUGUI tituloTarjetaBase;
    public TextMeshProUGUI textoTarjetaBase;

    public GameObject tarjetaExplicacion;
    public TextMeshProUGUI tituloTarjetaExplicacion;
    public TextMeshProUGUI textoTarjetaExplicacion;

    [Header("Interfaz de Interacción")]
    public GameObject grupoOpciones; // Botones Atrás (<) y Ayuda (?)
    public GameObject pantallaCorreoE4; // Interfaz del correo falso
    public GameObject grupoBotonesAccion; // Botones flotantes: Enviar y Llamar

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

    // Sensores de estado del nivel
    private int pasoActual = 0;
    private bool targetDetectado = false; // Rastrea si Vuforia está viendo la imagen activa

    void Start()
    {
        OcultarTodo();
    }

    // --- FUNCIONES DE FLUJO (INTRODUCCIÓN) ---

    // Inicia el escenario y reinicia los sensores
    public void IniciarEscenario()
    {
        pasoActual = 0;
        targetDetectado = false;
        OcultarTodo();

        tarjetaBase.SetActive(true);
        tituloTarjetaBase.text = "UN FAVOR SOSPECHOSO";
        textoTarjetaBase.text = "Los estafadores juegan con tus emociones. Fingen ser un amigo o un familiar con una emergencia.";
    }

    // Controla la transición de las tarjetas iniciales
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
            // Fase de Escaneo: Apagamos la explicación y habilitamos los menús globales
            tarjetaExplicacion.SetActive(false);
            grupoOpciones.SetActive(true);
            pasoActual++;
        }
    }

    // --- FUNCIONES DE ESCANEO (VUFORIA) ---

    // Llamado por Vuforia cuando detecta la imagen (Image Target)
    public void ActivarCorreoYBotones()
    {
        if (pasoActual > 0)
        {
            targetDetectado = true; // El sensor avisa que la cámara ve el objetivo
            pantallaCorreoE4.SetActive(true);
            grupoBotonesAccion.SetActive(true);
            grupoOpciones.SetActive(true);
        }
    }

    // Llamado por Vuforia cuando pierde de vista la imagen
    public void DesactivarCorreoYBotones()
    {
        targetDetectado = false; // Se perdió el objetivo
        pantallaCorreoE4.SetActive(false);
        grupoBotonesAccion.SetActive(false);
    }

    // --- INTERACCIÓN DE LOS BOTONES DE ACCIÓN (JUEGO) ---

    // Acción Incorrecta: El usuario cayó en la estafa
    public void BotonEnviarDinero()
    {
        pantallaCorreoE4.SetActive(false);
        grupoBotonesAccion.SetActive(false);
        grupoOpciones.SetActive(false); // Limpiamos la pantalla

        tarjetaError.SetActive(true);
        tituloTarjetaError.text = "¡CUIDADO!";
        textoTarjetaError.text = "No actúes por miedo. Las emergencias falsas son trucos para robar tu dinero.";
        if (textoBotonError != null) textoBotonError.text = "INTENTAR DE NUEVO";
    }

    // Acción Correcta: El usuario verificó la identidad
    public void BotonRealizarLlamada()
    {
        pantallaCorreoE4.SetActive(false);
        grupoBotonesAccion.SetActive(false);
        grupoOpciones.SetActive(false);

        tarjetaCorrecto.SetActive(true);
        tituloTarjetaCorrecto.text = "¡MUY INTELIGENTE!";
        textoTarjetaCorrecto.text = "Nunca envíes dinero solo por un correo. Siempre debes llamar a la persona a su teléfono conocido para comprobar si es verdad.";
        if (textoBotonCorrecto != null) textoBotonCorrecto.text = "CONTINUAR";
    }

    // --- FUNCIONES AUXILIARES DE NAVEGACIÓN ---

    // Muestra la tarjeta de pista y apaga temporalmente el correo AR para evitar encimados
    public void MostrarAyuda()
    {
        if (pantallaCorreoE4 != null) pantallaCorreoE4.SetActive(false);
        if (grupoBotonesAccion != null) grupoBotonesAccion.SetActive(false);

        tarjetaAyuda.SetActive(true);
        tituloTarjetaAyuda.text = "¡NO TE PREOCUPES!";
        textoTarjetaAyuda.text = "Escanea la imagen y selecciona el botón con la acción que consideres correcta para este caso.";
    }

    // Oculta la pista y devuelve el correo AR SOLO si la cámara sigue viendo la imagen
    public void OcultarAyuda()
    {
        tarjetaAyuda.SetActive(false);

        if (targetDetectado)
        {
            pantallaCorreoE4.SetActive(true);
            grupoBotonesAccion.SetActive(true);
        }
    }

    // Reinicia el escenario tras un error, devolviendo el correo AR si está en cámara
    public void RestaurarDespuesDeError()
    {
        tarjetaError.SetActive(false);
        grupoOpciones.SetActive(true);

        if (targetDetectado)
        {
            pantallaCorreoE4.SetActive(true);
            grupoBotonesAccion.SetActive(true);
        }
    }

    // NUEVO: Se ejecuta al tocar el botón Atrás (<) para salir del nivel
    public void RegresarAlMenuPrincipal()
    {
        OcultarTodo();
        if (maestro != null) maestro.CambiarEscenarioActivo(0); // Apaga AR
        if (navegacion != null) navegacion.MostrarMenuPrincipal(); // Vuelve a Inicio
    }

    // Limpia la pantalla apagando todas las tarjetas
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