using UnityEngine;
using TMPro;
using System.Collections;

public class ControladorEscenario3 : MonoBehaviour
{
    [Header("Tarjetas Principales")]
    public GameObject tarjetaBase;
    public TextMeshProUGUI tituloTarjetaBase;
    public TextMeshProUGUI textoTarjetaBase;

    // NUEVO: Variables para la tarjeta con Botty explicando
    public GameObject tarjetaExplicacion;
    public TextMeshProUGUI tituloTarjetaExplicacion;
    public TextMeshProUGUI textoTarjetaExplicacion;

    [Header("Opciones del Escenario")]
    public GameObject grupoOpciones;

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
    public GameObject botonAnalizar;
    public GameObject animacionScanner;

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

        // Solo encendemos la base al inicio
        tarjetaBase.SetActive(true);

        tituloTarjetaBase.text = "EL PAQUETE SORPRESA";
        textoTarjetaBase.text = "Este correo contiene un archivo adjunto. Antes de abrir archivos en internet, siempre debemos revisarlo.";
    }

    public void BotonContinuarBase()
    {
        if (pasoActual == 0)
        {
            // Apagamos la base y prendemos la explicación con Botty animado
            tarjetaBase.SetActive(false);
            tarjetaExplicacion.SetActive(true);

            tituloTarjetaExplicacion.text = "ANALIZA EL DOCUMENTO";
            textoTarjetaExplicacion.text = "Escanea la imagen y toca el botón flotante de 'Analizar' que aparece sobre el documento para comprobar si es seguro.";

            pasoActual++;
        }
        else
        {
            // Apagamos la explicación y pasamos al escáner
            tarjetaExplicacion.SetActive(false);
            grupoOpciones.SetActive(true);
        }
    }

    // --- FUNCIONES DE ESCANEO (VUFORIA) ---
    public void ActivarBotonAnalizar()
    {
        if (pasoActual > 0)
        {
            botonAnalizar.SetActive(true);
        }
    }

    public void DesactivarBotonAnalizar()
    {
        botonAnalizar.SetActive(false);
    }

    // --- INTERACCIÓN DE ANÁLISIS Y VIRUS ---
    public void IniciarAnalisis()
    {
        botonAnalizar.SetActive(false);
        grupoOpciones.SetActive(false);
        animacionScanner.SetActive(true);

        StartCoroutine(RutinaAnalisis());
    }

    private IEnumerator RutinaAnalisis()
    {
        yield return new WaitForSeconds(7f);

        animacionScanner.SetActive(false);
        MostrarAlertaVirus();
    }

    private void MostrarAlertaVirus()
    {
        tarjetaError.SetActive(true);
        grupoOpciones.SetActive(true);
        tituloTarjetaError.text = "¡PELIGRO!";
        textoTarjetaError.text = "El scanner detectó un virus. Está diseñado para robar tu información.";

        if (textoBotonError != null) textoBotonError.text = "BORRAR";
    }

    public void BotonBorrarPresionado()
    {
        tarjetaError.SetActive(false);
        tarjetaCorrecto.SetActive(true);
        grupoOpciones.SetActive(true);

        tituloTarjetaCorrecto.text = "¡A SALVO!";
        textoTarjetaCorrecto.text = "Eliminaste la amenaza. Recuerda: Nunca descargues archivos de personas que no conoces. ¡Excelente trabajo!";

        if (textoBotonCorrecto != null) textoBotonCorrecto.text = "CONTINUAR";
    }

    // --- FUNCIONES AUXILIARES ---
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

    private void OcultarTodo()
    {
        tarjetaBase.SetActive(false);
        if (tarjetaExplicacion != null) tarjetaExplicacion.SetActive(false); // NUEVO
        tarjetaError.SetActive(false);
        tarjetaCorrecto.SetActive(false);
        tarjetaAyuda.SetActive(false);
        botonAnalizar.SetActive(false);
        animacionScanner.SetActive(false);
        if (grupoOpciones != null) grupoOpciones.SetActive(false);
    }
}