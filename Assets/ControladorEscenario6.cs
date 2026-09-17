/* ==============================================================================
 * PROYECTO: RAUVIS (Realidad Aumentada para la detección de Phishing y Estafas)
 * SCRIPT: ControladorEscenario6.cs
 * DESCRIPCIÓN: Gestiona el 6to y último Escenario (Cadenas de Correos). 
 *              Incluye la lógica de decisión final y la pantalla de felicitación 
 *              que concluye el flujo de aprendizaje de la aplicación.
 * ============================================================================== */

using UnityEngine;
using TMPro;

public class ControladorEscenario6 : MonoBehaviour
{
    // --- CONEXIONES GLOBALES ---
    [Header("Conexiones del Sistema")]
    public ControladorNavegacion navegacion; // Para volver al inicio al terminar o cancelar
    public ControladorMaestro maestro;       // Para apagar la AR

    // --- VARIABLES DE INTERFAZ (TARJETAS) ---
    [Header("Tarjetas Principales")]
    public GameObject tarjetaBase;
    public TextMeshProUGUI tituloTarjetaBase;
    public TextMeshProUGUI textoTarjetaBase;

    public GameObject tarjetaExplicacion;
    public TextMeshProUGUI tituloTarjetaExplicacion;
    public TextMeshProUGUI textoTarjetaExplicacion;

    [Header("Interfaz de Interacción")]
    public GameObject grupoOpciones; // Botones globales: Atrás (<) y Ayuda (?)
    public GameObject pantallaCorreoE6; // El correo falso visualizado
    public GameObject grupoBotonesAccion; // Botones AR: "Reenviar" y "Borrar"

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
    public GameObject tarjetaFelicitacion; // Pantalla final tras completar los 6 niveles
    public TextMeshProUGUI tituloTarjetaFelicitacion;
    public TextMeshProUGUI textoTarjetaFelicitacion;

    // Sensores de estado
    private int pasoActual = 0;
    private bool targetDetectado = false;

    void Start()
    {
        OcultarTodo();
    }

    // --- FUNCIONES DE FLUJO (INTRODUCCIÓN) ---
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
            // Apagamos explicación y habilitamos opciones globales
            tarjetaExplicacion.SetActive(false);
            grupoOpciones.SetActive(true);
            pasoActual++;
        }
    }

    // --- FUNCIONES DE ESCANEO (VUFORIA) ---
    public void ActivarCorreoYBotones()
    {
        // Candado anti-parpadeo: Evita encender cosas si el usuario está viendo retroalimentación o el final
        if (pasoActual > 0 && !tarjetaAyuda.activeSelf && !tarjetaError.activeSelf && !tarjetaCorrecto.activeSelf && !tarjetaFelicitacion.activeSelf)
        {
            targetDetectado = true;
            pantallaCorreoE6.SetActive(true);
            grupoBotonesAccion.SetActive(true);
            grupoOpciones.SetActive(true);
        }
        else if (pasoActual > 0)
        {
            targetDetectado = true; // Solo registramos que lo vio
        }
    }

    public void DesactivarCorreoYBotones()
    {
        targetDetectado = false;
        pantallaCorreoE6.SetActive(false);
        grupoBotonesAccion.SetActive(false);
    }

    // --- INTERACCIÓN DE LOS BOTONES DE ACCIÓN (JUEGO) ---

    // Acción Incorrecta
    public void BotonReenviar()
    {
        OcultarElementosInteraccion();

        tarjetaError.SetActive(true);
        tituloTarjetaError.text = "¡CUIDADO!";
        textoTarjetaError.text = "Al reenviarlo, ayudas a los estafadores a engañar a tus seres queridos y llenas sus correos de basura.";
        if (textoBotonError != null) textoBotonError.text = "INTENTAR DE NUEVO";
    }

    // Acción Correcta
    public void BotonBorrar()
    {
        OcultarElementosInteraccion();

        tarjetaCorrecto.SetActive(true);
        tituloTarjetaCorrecto.text = "¡CADENA ROTA!";
        textoTarjetaCorrecto.text = "¡Muy bien! Al borrar el correo, detuviste las mentiras y protegiste a tus contactos. ¡Tu bandeja está limpia y segura!";
        if (textoBotonCorrecto != null) textoBotonCorrecto.text = "CONTINUAR";
    }

    // --- EL GRAN FINAL ---

    // Se ejecuta desde el ControladorMaestro (o desde tarjetaCorrecto) al finalizar todo
    public void MostrarFelicitacion()
    {
        OcultarTodo();
        tarjetaFelicitacion.SetActive(true);
        tituloTarjetaFelicitacion.text = "¡FELICITACIONES!";
        textoTarjetaFelicitacion.text = "La tecnología no tiene por qué ser difícil. Tómate tu tiempo al leer, confía en tu instinto y ¡navega seguro!";
    }

    // ACTUALIZADO: Cierra oficialmente la experiencia de juego
    public void BotonFinalizarJuego()
    {
        OcultarTodo();
        if (maestro != null) maestro.CambiarEscenarioActivo(0); // Apagamos marcadores AR

        // Guardamos explícitamente que ya se completaron todos los niveles (7)
        PlayerPrefs.SetInt("NivelGuardado", 7);
        PlayerPrefs.Save();

        // Volvemos al inicio para ver la tarjeta en estado "¡Misión Cumplida!"
        if (navegacion != null) navegacion.MostrarMenuPrincipal();
    }

    // --- FUNCIONES AUXILIARES DE NAVEGACIÓN ---
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

    // NUEVO: Se ejecuta al tocar el botón Atrás (<) para salir del nivel
    public void RegresarAlMenuPrincipal()
    {
        OcultarTodo();
        if (maestro != null) maestro.CambiarEscenarioActivo(0);
        if (navegacion != null) navegacion.MostrarMenuPrincipal();
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