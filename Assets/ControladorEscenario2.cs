using UnityEngine;
using TMPro;

public class ControladorEscenario2 : MonoBehaviour
{
    [Header("Tarjetas Principales")]
    public GameObject tarjetaBase;
    public TextMeshProUGUI tituloTarjetaBase;
    public TextMeshProUGUI textoTarjetaBase;

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
        // Limpieza inicial por seguridad al abrir la app
        OcultarTodo();
    }

    // --- FUNCIONES DE FLUJO ---

    public void IniciarEscenario()
    {
        pasoActual = 0;
        OcultarTodo();

        // Arrancamos con el primer paso de tu libreta
        tarjetaBase.SetActive(true);
        tituloTarjetaBase.text = "EL CARTERO FALSO";
        textoTarjetaBase.text = "Alguien envió un correo del banco, pero los ladrones a veces se disfrazan.";
    }

    public void BotonContinuarBase()
    {
        if (pasoActual == 0)
        {
            // Segundo texto de la libreta en la misma tarjeta
            textoTarjetaBase.text = "Usa la lupa para buscar los correos. Cuando los veas, toca el que creas que es falso.";
            pasoActual++;
        }
        else
        {
            // Pasamos a la pantalla del escáner
            tarjetaBase.SetActive(false);
            pantallaLupa.SetActive(true);
        }
    }

    // --- FUNCIONES DE DECISIÓN (Al tocar los correos) ---

    // El usuario se equivocó al elegir el correo oficial como si fuera el falso
    public void SeleccionarCorreoBueno()
    {
        OcultarTodo();
        tarjetaError.SetActive(true);
        tituloTarjetaError.text = "¡¡Revisa los detalles!!";
        textoTarjetaError.text = "Ese es un correo oficial. Revisa el otro. Fíjate que usa '@gmail' y trata de asustarte con 'urgencias'.";
    }

    // El usuario acertó al identificar el phishing
    public void SeleccionarCorreoMalo()
    {
        OcultarTodo();
        tarjetaCorrecto.SetActive(true);
        tituloTarjetaCorrecto.text = "¡CORRECTO!";
        textoTarjetaCorrecto.text = "Los bancos reales nunca usan '@gmail' ni te envían amenazas urgentes. ¡Esquivaste una trampa!";
    }

    public void BotonIntentarDeNuevo()
    {
        tarjetaError.SetActive(false);
        pantallaLupa.SetActive(true); // Lo regresamos a seguir buscando con la lupa
    }

    // --- FUNCIONES DE NAVEGACIÓN Y AYUDA ---

    public void MostrarAyuda()
    {
        OcultarTodo();
        tarjetaAyuda.SetActive(true);
        tituloTarjetaAyuda.text = "NO TE PREOCUPES";
        textoTarjetaAyuda.text = "Uno de los correos quiere robar tus datos, analiza la dirección del usuario de cada uno.";
    }

    public void RegresarInicio()
    {
        // Función conectada a tu botón (<) - Pendiente de implementar el cambio de escena
        Debug.Log("Regresando al menú de inicio...");
    }

    // Función auxiliar para mantener la pantalla limpia
    private void OcultarTodo()
    {
        tarjetaBase.SetActive(false);
        tarjetaError.SetActive(false);
        tarjetaCorrecto.SetActive(false);
        tarjetaAyuda.SetActive(false);
        pantallaLupa.SetActive(false);
        mailCardMalo.SetActive(false);
        mailCardBueno.SetActive(false);
    }
}