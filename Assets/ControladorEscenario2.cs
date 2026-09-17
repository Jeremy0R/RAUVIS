/* ==============================================================================
 * PROYECTO: RAUVIS (Realidad Aumentada para la detección de Phishing y Estafas)
 * SCRIPT: ControladorEscenario2.cs
 * DESCRIPCIÓN: Gestiona la lógica del Escenario 2 (El Cartero Falso). 
 *              Controla la transición entre la introducción, el uso de la lupa 
 *              (escáner) y la retroalimentación al identificar correos falsos.
 * ============================================================================== */

using UnityEngine;
using TMPro;

public class ControladorEscenario2 : MonoBehaviour
{
    // --- CONEXIONES GLOBALES ---
    [Header("Conexiones del Sistema")]
    public ControladorNavegacion navegacion; // Para regresar a Inicio
    public ControladorMaestro maestro;       // Para apagar la cámara al salir

    // --- VARIABLES DE INTERFAZ (TARJETAS) ---
    [Header("Tarjetas Principales")]
    public GameObject tarjetaBase;
    public TextMeshProUGUI tituloTarjetaBase;
    public TextMeshProUGUI textoTarjetaBase;

    public GameObject tarjetaExplicacion;
    public TextMeshProUGUI tituloTarjetaExplicacion;
    public TextMeshProUGUI textoTarjetaExplicacion;

    [Header("Opciones del Escenario")]
    public GameObject grupoOpciones; // Contiene los botones de Ayuda (?) y Regresar (<)

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

    [Header("Elementos del Escáner (Lupa) y Correos")]
    public GameObject pantallaLupa;
    public GameObject mailCardMalo;
    public GameObject mailCardBueno;

    // Controla el avance del diálogo inicial
    private int pasoActual = 0;

    void Start()
    {
        // Limpieza inicial para que no se superpongan cosas al abrir la app
        OcultarTodo();
    }

    // --- FUNCIONES DE FLUJO ---

    // Configura el nivel desde cero cuando se activa
    public void IniciarEscenario()
    {
        pasoActual = 0;
        OcultarTodo();

        // Encendemos la tarjeta base y apagamos botones secundarios para enfocar la lectura
        tarjetaBase.SetActive(true);
        grupoOpciones.SetActive(false);

        tituloTarjetaBase.text = "EL CARTERO FALSO";
        textoTarjetaBase.text = "Alguien envió un correo del banco, pero los ladrones a veces se disfrazan.";
    }

    // Controla los clics en el botón "Continuar" de las tarjetas de diálogo
    public void BotonContinuarBase()
    {
        if (pasoActual == 0)
        {
            tarjetaBase.SetActive(false);
            tarjetaExplicacion.SetActive(true);

            tituloTarjetaExplicacion.text = "EL CARTERO FALSO";
            textoTarjetaExplicacion.text = "Usa la lupa para buscar los correos. Cuando los veas, toca el que creas que es falso.";

            pasoActual++;
        }
        else
        {
            // Fin del diálogo: Pasamos a la pantalla de la lupa y mostramos las opciones globales
            tarjetaBase.SetActive(false);
            tarjetaExplicacion.SetActive(false);
            grupoOpciones.SetActive(true);
            pantallaLupa.SetActive(true);
        }
    }

    // --- FUNCIONES DE DECISIÓN (JUEGO) ---

    // El usuario se equivocó y seleccionó el correo real
    public void SeleccionarCorreoBueno()
    {
        OcultarTodo();
        tarjetaError.SetActive(true);
        grupoOpciones.SetActive(true); // Dejamos las opciones encendidas por si quiere ayuda

        tituloTarjetaError.text = "¡¡Revisa los detalles!!";
        textoTarjetaError.text = "Ese es un correo oficial. Revisa el otro. Fíjate que usa '@gmail' y trata de asustarte con 'urgencias'.";
    }

    // El usuario acertó y seleccionó el correo estafa
    public void SeleccionarCorreoMalo()
    {
        OcultarTodo();
        tarjetaCorrecto.SetActive(true);
        grupoOpciones.SetActive(true);

        tituloTarjetaCorrecto.text = "¡CORRECTO!";
        textoTarjetaCorrecto.text = "Los bancos reales nunca usan '@gmail' ni te envían amenazas urgentes. ¡Esquivaste una trampa!";
    }

    // Cierra la tarjeta de error para seguir jugando
    public void BotonIntentarDeNuevo()
    {
        tarjetaError.SetActive(false);
        tarjetaAyuda.SetActive(false);
        grupoOpciones.SetActive(true);
        pantallaLupa.SetActive(true); // Regresamos a la lupa
    }

    // --- FUNCIONES AUXILIARES DE NAVEGACIÓN ---

    // Muestra la tarjeta de pistas
    public void MostrarAyuda()
    {
        OcultarTodo();
        tarjetaAyuda.SetActive(true);
        tituloTarjetaAyuda.text = "NO TE PREOCUPES";
        textoTarjetaAyuda.text = "Uno de los correos quiere robar tus datos, analiza la dirección del usuario de cada uno.";
    }

    // NUEVO: Se ejecuta al tocar el botón Atrás (<) para salir del nivel y volver al Home
    public void RegresarAlMenuPrincipal()
    {
        OcultarTodo();
        if (maestro != null) maestro.CambiarEscenarioActivo(0); // Apaga la experiencia AR
        if (navegacion != null) navegacion.MostrarMenuPrincipal(); // Muestra la interfaz de Inicio
    }

    // Apaga todas las tarjetas y paneles visuales para limpiar la pantalla
    private void OcultarTodo()
    {
        tarjetaBase.SetActive(false);
        tarjetaExplicacion.SetActive(false);
        tarjetaError.SetActive(false);
        tarjetaCorrecto.SetActive(false);
        tarjetaAyuda.SetActive(false);
        pantallaLupa.SetActive(false);
        mailCardMalo.SetActive(false);
        mailCardBueno.SetActive(false);

        if (grupoOpciones != null) grupoOpciones.SetActive(false);
    }
}