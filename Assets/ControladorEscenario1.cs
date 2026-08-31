using UnityEngine;
using TMPro; // Librería para controlar la tipografía de alta calidad

public class ControladorEscenario1 : MonoBehaviour
{
    // --- Declaración de variables ---

    public GameObject tarjetaBase;
    public TextMeshProUGUI textoTarjetaBase;

    public GameObject grupoOpciones;

    // Tarjetas de retroalimentación
    public GameObject tarjetaError;
    public GameObject tarjetaCorrecto;
    public GameObject tarjetaAyuda;

    public GameObject grupoCorreos;
    public GameObject correoMalo;
    public GameObject correoBueno;
    public GameObject pantallaEscaner;

    // --- FUNCIONES (MÉTODOS / EVENTOS) ---

    /// <summary>
    /// Se activa cuando Vuforia detecta el marcador. Muestra la tarjeta base inicial.
    /// </summary>
    
    public void IniciarEscenario()
    {
        // Apagamos todo por seguridad al iniciar
        grupoOpciones.SetActive(false);
        tarjetaError.SetActive(false);
        tarjetaCorrecto.SetActive(false);
        tarjetaAyuda.SetActive(false);
        grupoCorreos.SetActive(false);

        // Encendemos la tarjeta base con el primer texto
        tarjetaBase.SetActive(true);
        textoTarjetaBase.text = "¡Hola! Para tener un correo, necesitas una contraseña segura.";
    }

    /// <summary>
    /// Se conecta al primer botón "Continuar". Cambia el texto de la instrucción.
    /// </summary>
    public void AvanzarInstruccion()
    {
        textoTarjetaBase.text = "Toca la contraseña que creas que es la más difícil de adivinar para un ladrón.";
    }

    /// <summary>
    /// Se conecta al segundo botón "Continuar" y al botón "Intentar de nuevo".
    /// </summary>
    public void MostrarOpciones()
    {
        tarjetaBase.SetActive(false);
        tarjetaError.SetActive(false); // Se apaga la tarjeta de error en caso de que se haya mostrado antes.
        tarjetaAyuda.SetActive(false); // Se apaga la tarjeta de ayuda en caso de que se haya mostrado antes.

        grupoOpciones.SetActive(true);
    }

    /// <summary>
    /// Se conecta a los botones de contraseñas débiles (Juan151986 y 12345678).
    /// </summary>
    public void SeleccionarOpcionIncorrecta()
    {
        grupoOpciones.SetActive(false); 
        tarjetaError.SetActive(true);
    }

    /// <summary>
    /// Se conecta al botón de la contraseña segura (Jv@n_5egur0).
    /// </summary>
    public void SeleccionarOpcionCorrecta()
    {
        grupoOpciones.SetActive(false);
        tarjetaCorrecto.SetActive(true);
    }

    /// <summary>
    /// Se conecta al botón de Ayuda para invocar a BOTTY con la pista.
    /// </summary>
    public void MostrarAyuda()
    {
        grupoOpciones.SetActive(false);
        tarjetaAyuda.SetActive(true);
    }
}