using UnityEngine;
using TMPro;

public class ControladorEscenario1 : MonoBehaviour
{
    // --- Declaración de variables ---

    public GameObject tarjetaBase;
    public TextMeshProUGUI tituloTarjetaBase;
    public TextMeshProUGUI textoTarjetaBase;

    // NUEVO: Variables para la tarjeta con Botty explicando
    public GameObject tarjetaExplicacion;
    public TextMeshProUGUI tituloTarjetaExplicacion;
    public TextMeshProUGUI textoTarjetaExplicacion;

    public GameObject grupoOpciones;

    // Tarjetas de retroalimentación y sus textos dinámicos
    public GameObject tarjetaError;
    public TextMeshProUGUI tituloTarjetaError;
    public TextMeshProUGUI textoTarjetaError;

    public GameObject tarjetaCorrecto;
    public TextMeshProUGUI tituloTarjetaCorrecto;
    public TextMeshProUGUI textoTarjetaCorrecto;

    public GameObject tarjetaAyuda;
    public TextMeshProUGUI tituloTarjetaAyuda;
    public TextMeshProUGUI textoTarjetaAyuda;

    // Elementos extra que tenías declarados
    public GameObject grupoCorreos;
    public GameObject correoMalo;
    public GameObject correoBueno;
    public GameObject pantallaEscaner;

    private int pasoActual = 0;

    void Start()
    {
        OcultarTodo();
    }

    // --- FUNCIONES (MÉTODOS / EVENTOS) ---

    public void IniciarEscenario()
    {
        pasoActual = 0;
        OcultarTodo();

        // Encendemos la tarjeta base con el primer texto
        tarjetaBase.SetActive(true);
        tituloTarjetaBase.text = "LA LLAVE DE TU CASA";
        textoTarjetaBase.text = "¡Hola! Para tener un correo, necesitas una contraseña segura.";
    }

    public void BotonContinuarBase()
    {
        if (pasoActual == 0)
        {
            // Apagamos la base y pasamos a la de explicación con Botty
            tarjetaBase.SetActive(false);
            tarjetaExplicacion.SetActive(true);

            tituloTarjetaExplicacion.text = "LA LLAVE DE TU CASA";
            textoTarjetaExplicacion.text = "Toca la contraseña que creas que es la más difícil de adivinar para un ladrón.";

            pasoActual++;
        }
        else
        {
            // Pasamos a mostrar los botones de las contraseñas
            MostrarOpciones();
        }
    }

    public void MostrarOpciones()
    {
        OcultarTodo();
        grupoOpciones.SetActive(true);
    }

    public void SeleccionarOpcionIncorrecta()
    {
        OcultarTodo();
        tarjetaError.SetActive(true);
        tituloTarjetaError.text = "¡CUIDADO!";
        textoTarjetaError.text = "Esa es muy fácil de adivinar. ¡Intenta mezclar letras mayúsculas, números y símbolos!";
    }

    public void SeleccionarOpcionCorrecta()
    {
        OcultarTodo();
        tarjetaCorrecto.SetActive(true);
        tituloTarjetaCorrecto.text = "¡CORRECTO!";
        textoTarjetaCorrecto.text = "Al mezclar mayúsculas, minúsculas, números y símbolos extraños. ¡Tu correo está protegido!";
    }

    public void MostrarAyuda()
    {
        OcultarTodo();
        tarjetaAyuda.SetActive(true);
        tituloTarjetaAyuda.text = "¡NO TE PREOCUPES!";
        textoTarjetaAyuda.text = "Una contraseña segura debe contener números, letras y símbolos especiales.";
    }

    // NUEVO: Función auxiliar para limpiar la pantalla (unifica el estilo con el E2 y E3)
    private void OcultarTodo()
    {
        tarjetaBase.SetActive(false);
        if (tarjetaExplicacion != null) tarjetaExplicacion.SetActive(false);
        grupoOpciones.SetActive(false);
        tarjetaError.SetActive(false);
        tarjetaCorrecto.SetActive(false);
        tarjetaAyuda.SetActive(false);

        // Limpieza de las variables extra
        if (grupoCorreos != null) grupoCorreos.SetActive(false);
        if (pantallaEscaner != null) pantallaEscaner.SetActive(false);
    }
}