using UnityEngine;
using TMPro;

public class ControladorEscenario6 : MonoBehaviour
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
    public GameObject pantallaCorreoE6; // La imagen del correo E6
    public GameObject grupoBotonesAccion; // Botones "Reenviar" y "Borrar"

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

    [Header("Cierre del Juego")]
    public GameObject tarjetaFelicitacion;
    public TextMeshProUGUI tituloTarjetaFelicitacion;
    public TextMeshProUGUI textoTarjetaFelicitacion;

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
        tituloTarjetaBase.text = "LAS CADENAS DE MENTIRAS";
        textoTarjetaBase.text = "Algunos correos prometen milagros si los reenvías a 10 amigos. ¡Son engañosos para saturar tu bandeja!";
    }

    public void BotonContinuarBase()
    {
        if (pasoActual == 0)
        {
            tarjetaBase.SetActive(false);
            tarjetaExplicacion.SetActive(true);

            tituloTarjetaExplicacion.text = "ROMPE LA CADENA";
            textoTarjetaExplicacion.text = "Este correo pide que lo reenvíes. ¿Qué debes hacer?";

            pasoActual++;
        }
        else
        {
            // Apagamos explicación y encendemos botones morados esperando el target
            tarjetaExplicacion.SetActive(false);
            grupoOpciones.SetActive(true);
            pasoActual++;
        }
    }

    // --- FUNCIONES DE ESCANEO (VUFORIA Target E6) ---
    public void ActivarCorreoYBotones()
    {
        // Candado anti-parpadeo
        if (pasoActual > 0 && !tarjetaAyuda.activeSelf && !tarjetaError.activeSelf && !tarjetaCorrecto.activeSelf && !tarjetaFelicitacion.activeSelf)
        {
            targetDetectado = true;
            pantallaCorreoE6.SetActive(true);
            grupoBotonesAccion.SetActive(true);
            grupoOpciones.SetActive(true);
        }
        else if (pasoActual > 0)
        {
            targetDetectado = true;
        }
    }

    public void DesactivarCorreoYBotones()
    {
        targetDetectado = false;
        pantallaCorreoE6.SetActive(false);
        grupoBotonesAccion.SetActive(false);
    }

    // --- INTERACCIÓN DE LOS BOTONES DE ACCIÓN ---
    public void BotonReenviar() // Acción Incorrecta
    {
        OcultarElementosInteraccion();

        tarjetaError.SetActive(true);
        tituloTarjetaError.text = "¡CUIDADO!";
        textoTarjetaError.text = "Al reenviarlo, ayudas a los estafadores a engañar a tus seres queridos y llenas sus correos de basura.";
        if (textoBotonError != null) textoBotonError.text = "INTENTAR DE NUEVO";
    }

    public void BotonBorrar() // Acción Correcta
    {
        OcultarElementosInteraccion();

        tarjetaCorrecto.SetActive(true);
        tituloTarjetaCorrecto.text = "¡CADENA ROTA!";
        textoTarjetaCorrecto.text = "¡Muy bien! Al borrar el correo, detuviste las mentiras y protegiste a tus contactos. ¡Tu bandeja está limpia y segura!";
        if (textoBotonCorrecto != null) textoBotonCorrecto.text = "CONTINUAR";
    }

    // --- EL GRAN FINAL ---
    public void MostrarFelicitacion()
    {
        OcultarTodo();
        tarjetaFelicitacion.SetActive(true);
        tituloTarjetaFelicitacion.text = "¡FELICITACIONES!";
        textoTarjetaFelicitacion.text = "Llegamos al final de nuestro recorrido. Demostraste que la tecnología no tiene porqué ser difícil ni dar miedo. Recuerda: Tómate tu tiempo para leer y confía en tu instinto. ¡Estás totalmente protegido!";
    }

    public void BotonFinalizarJuego()
    {
        // Por ahora limpia la pantalla. Aquí en el futuro cargarás tu Pantalla de Inicio
        OcultarTodo();
        Debug.Log("¡El jugador ha terminado la app! Listo para volver al menú principal.");
    }

    // --- FUNCIONES AUXILIARES ---
    public void MostrarAyuda()
    {
        if (pantallaCorreoE6 != null) pantallaCorreoE6.SetActive(false);
        if (grupoBotonesAccion != null) grupoBotonesAccion.SetActive(false);

        tarjetaAyuda.SetActive(true);
        tituloTarjetaAyuda.text = "¡NO TE PREOCUPES!";
        textoTarjetaAyuda.text = "Selecciona el botón con la acción que consideres correcta para evitar que la estafa siga circulando.";
    }

    public void OcultarAyuda()
    {
        tarjetaAyuda.SetActive(false);
        if (targetDetectado)
        {
            pantallaCorreoE6.SetActive(true);
            grupoBotonesAccion.SetActive(true);
        }
    }

    public void RestaurarDespuesDeError()
    {
        tarjetaError.SetActive(false);
        grupoOpciones.SetActive(true);

        if (targetDetectado)
        {
            pantallaCorreoE6.SetActive(true);
            grupoBotonesAccion.SetActive(true);
        }
    }

    private void OcultarElementosInteraccion()
    {
        pantallaCorreoE6.SetActive(false);
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
        if (tarjetaFelicitacion != null) tarjetaFelicitacion.SetActive(false);
        if (pantallaCorreoE6 != null) pantallaCorreoE6.SetActive(false);
        if (grupoBotonesAccion != null) grupoBotonesAccion.SetActive(false);
        if (grupoOpciones != null) grupoOpciones.SetActive(false);
    }
}