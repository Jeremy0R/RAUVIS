using UnityEngine;
using TMPro; // Librería para controlar la tipografía de alta calidad

public class ControladorEscenario1 : MonoBehaviour
{
    // --- Declaración de variables ---

    public GameObject tarjetaBase;
    public TextMeshProUGUI tituloTarjetaBase; // Nuevo cable para el título
    public TextMeshProUGUI textoTarjetaBase;

    public GameObject grupoOpciones;

    // Tarjetas de retroalimentación y sus textos dinámicos
    public GameObject tarjetaError;
    public TextMeshProUGUI tituloTarjetaError; // Nuevo cable para el título
    public TextMeshProUGUI textoTarjetaError;

    public GameObject tarjetaCorrecto;
    public TextMeshProUGUI tituloTarjetaCorrecto; // Nuevo cable para el título
    public TextMeshProUGUI textoTarjetaCorrecto;

    public GameObject tarjetaAyuda;
    public TextMeshProUGUI tituloTarjetaAyuda; // Nuevo cable para el título
    public TextMeshProUGUI textoTarjetaAyuda;

    public GameObject grupoCorreos;
    public GameObject correoMalo;
    public GameObject correoBueno;
    public GameObject pantallaEscaner;

    private int pasoActual = 0;

    // --- FUNCIONES (MÉTODOS / EVENTOS) ---

    public void IniciarEscenario()
    {
        pasoActual = 0;

        // Apagamos todo por seguridad al iniciar
        grupoOpciones.SetActive(false);
        tarjetaError.SetActive(false);
        tarjetaCorrecto.SetActive(false);
        tarjetaAyuda.SetActive(false);
        grupoCorreos.SetActive(false);

        // Encendemos la tarjeta base con el primer texto
        tarjetaBase.SetActive(true);
        tituloTarjetaBase.text = "LA LLAVE DE TU CASA";
        textoTarjetaBase.text = "¡Hola! Para tener un correo, necesitas una contraseña segura.";
    }

    public void BotonContinuarBase()
    {
        if (pasoActual == 0)
        {
            textoTarjetaBase.text = "Toca la contraseña que creas que es la más difícil de adivinar para un ladrón.";
            pasoActual++;
        }
        else
        {
            MostrarOpciones();
        }
    }

    public void MostrarOpciones()
    {
        tarjetaBase.SetActive(false);
        tarjetaError.SetActive(false);
        tarjetaAyuda.SetActive(false);

        grupoOpciones.SetActive(true);
    }

    public void SeleccionarOpcionIncorrecta()
    {
        grupoOpciones.SetActive(false);
        tarjetaError.SetActive(true);
        tituloTarjetaError.text = "¡CUIDADO!";
        textoTarjetaError.text = "Esa es muy fácil de adivinar. ¡Intenta mezclar letras mayúsculas, números y símbolos!";
    }

    public void SeleccionarOpcionCorrecta()
    {
        grupoOpciones.SetActive(false);
        tarjetaCorrecto.SetActive(true);
        tituloTarjetaCorrecto.text = "¡CORRECTO!";
        textoTarjetaCorrecto.text = "Esta contraseña es muy segura porque mezcla mayúsculas, minúsculas, números y símbolos extraños. ¡Tu correo está protegido!";
    }

    public void MostrarAyuda()
    {
        grupoOpciones.SetActive(false);
        tarjetaAyuda.SetActive(true);
        tituloTarjetaAyuda.text = "¡NO TE PREOCUPES!";
        textoTarjetaAyuda.text = "Una contraseña segura debe contener números, letras y símbolos especiales.";
    }
}