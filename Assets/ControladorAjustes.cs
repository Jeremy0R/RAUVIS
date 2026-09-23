/* ==============================================================================
 * PROYECTO: RAUVIS 
 * SCRIPT: ControladorAjustes.cs
 * DESCRIPCIÓN: Gestiona la vista de Ajustes. Controla la navegación interna, 
 *              la inyección de textos, el reseteo seguro de la memoria y la 
 *              configuración global de AUDIO.
 * ============================================================================== */

using UnityEngine;
using TMPro;

public class ControladorAjustes : MonoBehaviour
{
    [Header("Conexiones Globales")]
    public ControladorNavegacion navegacion;

    [Header("Sub-Paneles de Ajustes")]
    public GameObject panelMenuAjustes;
    public GameObject panelSonido;
    public GameObject panelAcercaDe;
    public GameObject panelConfirmarBorrado;
    public GameObject panelBorradoCompletado;

    [Header("Textos: Tarjeta Sonido")]
    public TextMeshProUGUI tituloSonido;
    public TextMeshProUGUI contenidoSonido;
    public TextMeshProUGUI textoBotonSonido;

    [Header("Textos: Advertencia de Borrado")]
    public TextMeshProUGUI tituloAdvertencia;
    public TextMeshProUGUI contenidoAdvertencia;
    public TextMeshProUGUI textoBotonCancelar;
    public TextMeshProUGUI textoBotonBorrar;

    [Header("Textos: Borrado Completado")]
    public TextMeshProUGUI tituloCompletado;
    public TextMeshProUGUI contenidoCompletado;
    public TextMeshProUGUI textoBotonHecho;

    // --- NUEVAS VARIABLES DE AUDIO ---
    [Header("Conexión de Audio")]
    public ControladorAudio gestorAudio;
    public AudioClip audioPruebaSonido;  // Audio: "Hola, la narración está activada."

    void OnEnable()
    {
        IrAMenuAjustes();
    }

    // --- NAVEGACIÓN INTERNA ---

    // Abre el menú principal de ajustes
    public void IrAMenuAjustes()
    {
        ApagarTodosLosPaneles();
        panelMenuAjustes.SetActive(true);
    }

    // NUEVO: Función para el botón Back del menú principal de Ajustes
    public void RegresarAInicio()
    {
        if (navegacion != null)
        {
            navegacion.IrAInicio();
        }
    }

    public void IrASonido()
    {
        ApagarTodosLosPaneles();
        panelSonido.SetActive(true);

        if (tituloSonido != null) tituloSonido.text = "Narraciones del robot";
        if (contenidoSonido != null) contenidoSonido.text = "El robot leerá en voz alta las instrucciones y textos de la app.";

        // Revisamos cómo está configurado actualmente para poner el texto correcto
        bool activado = PlayerPrefs.GetInt("NarracionActivada", 1) == 1;
        if (textoBotonSonido != null) textoBotonSonido.text = activado ? "DESACTIVAR" : "ACTIVAR";
    }

    public void IrAAcercaDe()
    {
        ApagarTodosLosPaneles();
        panelAcercaDe.SetActive(true);
    }

    public void IrABorrarDatos()
    {
        ApagarTodosLosPaneles();
        panelConfirmarBorrado.SetActive(true);

        if (tituloAdvertencia != null) tituloAdvertencia.text = "¡CUIDADO!";
        if (contenidoAdvertencia != null) contenidoAdvertencia.text = "Se eliminará tu progreso, talleres completados y logros.\nEsta acción no se puede deshacer.";
        if (textoBotonCancelar != null) textoBotonCancelar.text = "CANCELAR";
        if (textoBotonBorrar != null) textoBotonBorrar.text = "BORRAR";
    }

    private void ApagarTodosLosPaneles()
    {
        panelMenuAjustes.SetActive(false);
        panelSonido.SetActive(false);
        panelAcercaDe.SetActive(false);
        panelConfirmarBorrado.SetActive(false);
        panelBorradoCompletado.SetActive(false);
    }

    // --- LÓGICA DE INTERACCIÓN ---

    public void BotonActivarDesactivarSonido()
    {
        // Leemos el estado actual
        bool activado = PlayerPrefs.GetInt("NarracionActivada", 1) == 1;

        // Lo invertimos (Si era 1 lo pasamos a 0, y viceversa)
        bool nuevoEstado = !activado;
        PlayerPrefs.SetInt("NarracionActivada", nuevoEstado ? 1 : 0);
        PlayerPrefs.Save();

        // Actualizamos el botón visualmente
        if (textoBotonSonido != null) textoBotonSonido.text = nuevoEstado ? "DESACTIVAR" : "ACTIVAR";

        // --- NUEVA LÓGICA DE AUDIO: Reproducimos o silenciamos la voz de prueba ---
        if (gestorAudio != null)
        {
            if (nuevoEstado)
            {
                gestorAudio.ReproducirVoz(audioPruebaSonido);
            }
            else
            {
                gestorAudio.DetenerVoz();
            }
        }
    }

    // Reinicia el juego y muestra la tarjeta final
    public void ConfirmarBorradoDeDatos()
    {
        if (navegacion != null)
        {
            navegacion.ReiniciarProgreso();

            // NUEVO: Apagamos la barra de navegación para evitar que el usuario se escape
            if (navegacion.barraNavegacion != null)
            {
                navegacion.barraNavegacion.SetActive(false);
            }
        }

        ApagarTodosLosPaneles();
        panelBorradoCompletado.SetActive(true);

        if (tituloCompletado != null) tituloCompletado.text = "COMPLETADO";
        if (contenidoCompletado != null) contenidoCompletado.text = "La información se borró de manera exitosa.\nPuedes comenzar a explorar los talleres ahora.";
        if (textoBotonHecho != null) textoBotonHecho.text = "HECHO";
    }

    // Finaliza el proceso y vuelve a Inicio
    public void BotonHechoPresionado()
    {
        // Reseteamos el panel de ajustes de fondo para la próxima vez que entre
        IrAMenuAjustes();

        if (navegacion != null)
        {
            // NUEVO: Usamos MostrarMenuPrincipal en lugar de IrAInicio. 
            // Esta función enciende la barra de navegación automáticamente y luego te lleva al Inicio.
            navegacion.MostrarMenuPrincipal();
        }
    }
}