/* ==============================================================================
 * PROYECTO: RAUVIS (Realidad Aumentada para la detección de Phishing y Estafas)
 * SCRIPT: ControladorEscenario3.cs
 * DESCRIPCIÓN: Gestiona el Escenario 3 (Archivos Adjuntos Peligrosos). 
 *              Implementa la lógica para escanear un objetivo con Vuforia, 
 *              mostrar una animación de análisis de virus y dar retroalimentación.
 * ============================================================================== */

using UnityEngine;
using TMPro;
using System.Collections;

public class ControladorEscenario3 : MonoBehaviour
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

    [Header("Opciones del Escenario")]
    public GameObject grupoOpciones; // Botones globales: Ayuda (?) y Atrás (<)

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

    [Header("Elementos de Escaneo y Animación")]
    public GameObject botonAnalizar; // Botón flotante AR
    public GameObject animacionScanner; // Animación de escaneo de virus

    // Controla la fase de la introducción
    private int pasoActual = 0;

    void Start()
    {
        OcultarTodo();
    }

    // --- FUNCIONES DE FLUJO (INTRODUCCIÓN) ---

    // Se invoca al iniciar el nivel para configurar el estado inicial
    public void IniciarEscenario()
    {
        pasoActual = 0;
        OcultarTodo();

        // Solo encendemos la base al inicio
        tarjetaBase.SetActive(true);

        tituloTarjetaBase.text = "EL PAQUETE SORPRESA";
        textoTarjetaBase.text = "Este correo contiene un archivo adjunto. Antes de abrir archivos en internet, siempre debemos revisarlo.";
    }

    // Controla la navegación de las tarjetas de diálogo
    public void BotonContinuarBase()
    {
        if (pasoActual == 0)
        {
            // Apagamos la base y prendemos la explicación con Botty
            tarjetaBase.SetActive(false);
            tarjetaExplicacion.SetActive(true);

            tituloTarjetaExplicacion.text = "ANALIZA EL DOCUMENTO";
            textoTarjetaExplicacion.text = "Escanea la imagen y toca el botón flotante de 'Analizar' que aparece sobre el documento para comprobar si es seguro.";

            pasoActual++;
        }
        else
        {
            // Apagamos la explicación y pasamos a la fase de escáner (Vuforia)
            tarjetaExplicacion.SetActive(false);
            grupoOpciones.SetActive(true);
        }
    }

    // --- FUNCIONES DE ESCANEO (VUFORIA) ---

    // Llamado por Vuforia cuando detecta el marcador (Image Target)
    public void ActivarBotonAnalizar()
    {
        if (pasoActual > 0)
        {
            botonAnalizar.SetActive(true);
        }
    }

    // Llamado por Vuforia cuando se pierde el marcador
    public void DesactivarBotonAnalizar()
    {
        botonAnalizar.SetActive(false);
    }

    // --- INTERACCIÓN DE ANÁLISIS Y VIRUS ---

    // Se ejecuta al tocar el botón flotante "Analizar" en el mundo AR
    public void IniciarAnalisis()
    {
        botonAnalizar.SetActive(false);
        grupoOpciones.SetActive(false); // Ocultamos menús para limpiar la vista
        animacionScanner.SetActive(true); // Iniciamos el GIF/Animación

        StartCoroutine(RutinaAnalisis());
    }

    // Simula el tiempo de escaneo del antivirus
    private IEnumerator RutinaAnalisis()
    {
        yield return new WaitForSeconds(7f); // Espera 7 segundos

        animacionScanner.SetActive(false);
        MostrarAlertaVirus();
    }

    // Muestra la tarjeta de alerta tras finalizar el análisis
    private void MostrarAlertaVirus()
    {
        tarjetaError.SetActive(true);
        grupoOpciones.SetActive(true);
        tituloTarjetaError.text = "¡PELIGRO!";
        textoTarjetaError.text = "El scanner detectó un virus. Está diseñado para robar tu información.";

        if (textoBotonError != null) textoBotonError.text = "BORRAR";
    }

    // Se ejecuta al decidir "Borrar" el virus
    public void BotonBorrarPresionado()
    {
        tarjetaError.SetActive(false);
        tarjetaCorrecto.SetActive(true);
        grupoOpciones.SetActive(true);

        tituloTarjetaCorrecto.text = "¡A SALVO!";
        textoTarjetaCorrecto.text = "Eliminaste la amenaza. Recuerda: Nunca descargues archivos de personas que no conoces. ¡Excelente trabajo!";

        if (textoBotonCorrecto != null) textoBotonCorrecto.text = "CONTINUAR";
    }

    // --- FUNCIONES AUXILIARES DE NAVEGACIÓN ---

    public void MostrarAyuda()
    {
        OcultarTodo();
        tarjetaAyuda.SetActive(true);
        tituloTarjetaAyuda.text = "¡NO TE PREOCUPES!";
        textoTarjetaAyuda.text = "Busca la imagen con la cámara para analizar el documento que te llegó.";
    }

    public void OcultarAyuda()
    {
        tarjetaAyuda.SetActive(false);
        grupoOpciones.SetActive(true);
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
        botonAnalizar.SetActive(false);
        animacionScanner.SetActive(false);
        if (grupoOpciones != null) grupoOpciones.SetActive(false);
    }
}