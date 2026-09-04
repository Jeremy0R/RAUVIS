using UnityEngine;
using TMPro;

public class ControladorEscenario2 : MonoBehaviour
{
    [Header("Tarjetas Principales")]
    public GameObject tarjetaBase;
    public TextMeshProUGUI tituloTarjetaBase;
    public TextMeshProUGUI textoTarjetaBase;

    // NUEVO: Agregamos el grupo de opciones para este escenario
    [Header("Opciones del Escenario")]
    public GameObject grupoOpciones;

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

    private int pasoActual = 0;

    void Start()
    {
        // Limpieza inicial
        OcultarTodo();
    }

    // --- FUNCIONES DE FLUJO ---

    public void IniciarEscenario()
    {
        pasoActual = 0;
        OcultarTodo();

        // Encendemos la tarjeta base y el grupo de opciones (Botón de Ayuda y Regresar)
        tarjetaBase.SetActive(true);
        grupoOpciones.SetActive(false);

        tituloTarjetaBase.text = "EL CARTERO FALSO";
        textoTarjetaBase.text = "Alguien envió un correo del banco, pero los ladrones a veces se disfrazan.";
    }

    public void BotonContinuarBase()
    {
        if (pasoActual == 0)
        {
            textoTarjetaBase.text = "Usa la lupa para buscar los correos. Cuando los veas, toca el que creas que es falso.";
            pasoActual++;
        }
        else
        {
            // Pasamos a la pantalla de la lupa y apagamos todo lo demás
            tarjetaBase.SetActive(false);
            grupoOpciones.SetActive(true);
            pantallaLupa.SetActive(true);
        }
    }

    // --- FUNCIONES DE DECISIÓN ---

    public void SeleccionarCorreoBueno()
    {
        OcultarTodo();
        tarjetaError.SetActive(true);
        grupoOpciones.SetActive(true); // Encendemos opciones por si necesita ayuda

        tituloTarjetaError.text = "¡¡Revisa los detalles!!";
        textoTarjetaError.text = "Ese es un correo oficial. Revisa el otro. Fíjate que usa '@gmail' y trata de asustarte con 'urgencias'.";
    }

    public void SeleccionarCorreoMalo()
    {
        OcultarTodo();
        tarjetaCorrecto.SetActive(true);
        grupoOpciones.SetActive(true);

        tituloTarjetaCorrecto.text = "¡CORRECTO!";
        textoTarjetaCorrecto.text = "Los bancos reales nunca usan '@gmail' ni te envían amenazas urgentes. ¡Esquivaste una trampa!";
    }

    public void BotonIntentarDeNuevo()
    {
        tarjetaError.SetActive(false);
        tarjetaAyuda.SetActive(false);
        grupoOpciones.SetActive(true);
        pantallaLupa.SetActive(true);
    }

    // --- FUNCIONES AUXILIARES ---

    // Esta función la llama el ControladorMaestro
    public void MostrarAyuda()
    {
        OcultarTodo();
        tarjetaAyuda.SetActive(true);
        tituloTarjetaAyuda.text = "NO TE PREOCUPES";
        textoTarjetaAyuda.text = "Uno de los correos quiere robar tus datos, analiza la dirección del usuario de cada uno.";
    }

    private void OcultarTodo()
    {
        tarjetaBase.SetActive(false);
        tarjetaError.SetActive(false);
        tarjetaCorrecto.SetActive(false);
        tarjetaAyuda.SetActive(false);
        pantallaLupa.SetActive(false);
        mailCardMalo.SetActive(false);
        mailCardBueno.SetActive(false);

        // Apagamos el grupo de opciones por defecto
        if (grupoOpciones != null) grupoOpciones.SetActive(false);
    }
}