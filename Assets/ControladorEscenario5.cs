/* ==============================================================================
 * PROYECTO: RAUVIS (Realidad Aumentada para la detección de Phishing y Estafas)
 * SCRIPT: ControladorEscenario5.cs
 * DESCRIPCIÓN: Gestiona el Escenario 5 (Links Falsos). Controla la interacción 
 *              donde el usuario usa una "lupa" para revelar un enlace oculto y 
 *              debe decidir si visitar o bloquear el sitio web sospechoso.
 * ============================================================================== */

using UnityEngine;
using TMPro;

public class ControladorEscenario5 : MonoBehaviour
{
    // --- CONEXIONES GLOBALES ---
    [Header("Conexiones del Sistema")]
    public ControladorNavegacion navegacion; // Para regresar al menú Inicio
    public ControladorMaestro maestro;       // Para apagar la AR al salir

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
    public GameObject pantallaLupa; // Marco visual de la lupa
    public GameObject cardLinkSospechoso; // Tarjeta que revela el link real
    public GameObject grupoBotonesAccion; // Botones AR: "Visitar Sitio" y "Bloquear"

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

    // Sensores de estado
    private int pasoActual = 0;
    private bool targetDetectado = false; // Rastrea si la imagen AR está enfocada

    void Start()
    {
        OcultarTodo();
    }

    // --- FUNCIONES DE FLUJO (INTRODUCCIÓN) ---

    // Inicializa el nivel y la primera instrucción
    public void IniciarEscenario()
    {
        pasoActual = 0;
        targetDetectado = false;
        OcultarTodo();

        tarjetaBase.SetActive(true);
        tituloTarjetaBase.text = "EL PUENTE ENGAÑOSO";
        textoTarjetaBase.text = "A veces, un correo te promete un premio, pero te lleva a una página falsa y peligrosa.";
    }

    // Navegación por las tarjetas de introducción
    public void BotonContinuarBase()
    {
        if (pasoActual == 0)
        {
            tarjetaBase.SetActive(false);
            tarjetaExplicacion.SetActive(true);

            tituloTarjetaExplicacion.text = "EL PUENTE ENGAÑOSO";
            textoTarjetaExplicacion.text = "Arrastra la lupa mágica sobre la tarjeta para ver a dónde te lleva realmente.";

            pasoActual++;
        }
        else
        {
            // Apagamos explicación y encendemos la interfaz de la lupa esperando el target
            tarjetaExplicacion.SetActive(false);
            grupoOpciones.SetActive(true);
            if (pantallaLupa != null) pantallaLupa.SetActive(true);
            pasoActual++;
        }
    }

    // --- FUNCIONES DE ESCANEO (VUFORIA) ---

    // Se invoca cuando Vuforia detecta la imagen (Target_E5)
    public void ActivarLinkYBotones()
    {
        // Candado: Evita que los botones AR aparezcan si el usuario está leyendo una tarjeta
        if (pasoActual > 0 && !tarjetaAyuda.activeSelf && !tarjetaError.activeSelf && !tarjetaCorrecto.activeSelf)
        {
            targetDetectado = true;
            cardLinkSospechoso.SetActive(true);
            grupoBotonesAccion.SetActive(true);
            grupoOpciones.SetActive(true);

            if (pantallaLupa != null) pantallaLupa.SetActive(false); // Oculta el marco de la lupa
        }
        else if (pasoActual > 0)
        {
            targetDetectado = true; // Solo registramos que lo vio de fondo
        }
    }

    // Se invoca cuando Vuforia pierde la imagen
    public void DesactivarLinkYBotones()
    {
        targetDetectado = false;
        cardLinkSospechoso.SetActive(false);
        grupoBotonesAccion.SetActive(false);

        // Volvemos a encender el marco de la lupa si no hay retroalimentación activa
        if (pasoActual > 0 && pantallaLupa != null && !tarjetaCorrecto.activeSelf && !tarjetaError.activeSelf)
        {
            pantallaLupa.SetActive(true);
        }
    }

    // --- INTERACCIÓN DE LOS BOTONES DE ACCIÓN (JUEGO) ---

    // Acción Incorrecta: Entró al sitio falso
    public void BotonVisitarSitio()
    {
        OcultarElementosInteraccion();

        tarjetaError.SetActive(true);
        tituloTarjetaError.text = "¡CUIDADO!";
        textoTarjetaError.text = "Si visitas un sitio web inseguro, los hackers pueden robar información personal de tu dispositivo.";
        if (textoBotonError != null) textoBotonError.text = "INTENTAR DE NUEVO";
    }

    // Acción Correcta: Bloqueó la URL sospechosa
    public void BotonBloquearSitio()
    {
        OcultarElementosInteraccion();

        tarjetaCorrecto.SetActive(true);
        tituloTarjetaCorrecto.text = "¡TRAMPA EVITADA!";
        textoTarjetaCorrecto.text = "Esa dirección es extraña y no es oficial. Si no la conoces mejor no entrar. ¡El correo ha sido bloqueado por tu seguridad!";
        if (textoBotonCorrecto != null) textoBotonCorrecto.text = "CONTINUAR";
    }

    // --- FUNCIONES AUXILIARES DE NAVEGACIÓN ---

    // Muestra la pista y esconde la AR temporalmente
    public void MostrarAyuda()
    {
        if (cardLinkSospechoso != null) cardLinkSospechoso.SetActive(false);
        if (grupoBotonesAccion != null) grupoBotonesAccion.SetActive(false);
        if (pantallaLupa != null) pantallaLupa.SetActive(false);

        tarjetaAyuda.SetActive(true);
        tituloTarjetaAyuda.text = "¡NO TE PREOCUPES!";
        textoTarjetaAyuda.text = "Escanea la imagen y selecciona el botón con la acción que consideres correcta para este caso.";
    }

    // Restaura la interfaz de AR si el objetivo sigue visible
    public void OcultarAyuda()
    {
        tarjetaAyuda.SetActive(false);
        if (targetDetectado)
        {
            cardLinkSospechoso.SetActive(true);
            grupoBotonesAccion.SetActive(true);
        }
        else if (pasoActual > 0)
        {
            if (pantallaLupa != null) pantallaLupa.SetActive(true);
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

    // NUEVO: Se ejecuta al tocar el botón Atrás (<) para salir del nivel
    public void RegresarAlMenuPrincipal()
    {
        OcultarTodo();
        if (maestro != null) maestro.CambiarEscenarioActivo(0); // Apaga AR
        if (navegacion != null) navegacion.MostrarMenuPrincipal(); // Vuelve al Inicio
    }

    // Oculta los elementos AR para limpiar la pantalla
    private void OcultarElementosInteraccion()
    {
        cardLinkSospechoso.SetActive(false);
        grupoBotonesAccion.SetActive(false);
        grupoOpciones.SetActive(false);
    }

    // Apaga todo
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