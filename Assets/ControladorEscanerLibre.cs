/* ==============================================================================

* PROYECTO: RAUVIS

* SCRIPT: ControladorEscanerLibre.cs

* DESCRIPCIÓN: Gestiona la vista "Escáner" del menú principal (Modo Práctica).

* Controla la transición de instrucciones y da retroalimentación

* al presionar el botón de captura simulado.

* ============================================================================== */
using UnityEngine;
using TMPro;

public class ControladorEscanerLibre : MonoBehaviour
{
    [Header("Interfaz Principal")]
    public GameObject tarjetaInstrucciones;
    public TextMeshProUGUI tituloInstrucciones;  // NUEVO
    public TextMeshProUGUI textoInstrucciones;   // NUEVO
    public TextMeshProUGUI textoBotonInstrucciones; // NUEVO

    public GameObject escanerAnimado;
    public GameObject botonCamara;

    [Header("Retroalimentación (Botón Rojo)")]
    public GameObject tarjetaAviso;
    public TextMeshProUGUI textoAviso;

    // Se ejecuta automáticamente cada vez que el usuario abre la pestaña Escáner
    void OnEnable()
    {
        ReiniciarEscaner();
    }

    public void ReiniciarEscaner()
    {
        // 1. Asignamos los textos de la tarjeta de Botty
        if (tituloInstrucciones != null) tituloInstrucciones.text = "ESTE ES EL ESCÁNER";
        if (textoInstrucciones != null) textoInstrucciones.text = "Asegúrate de tener buena luz. Apunta la cámara al código QR y toca CONTINUAR cuando estés listo.";
        if (textoBotonInstrucciones != null) textoBotonInstrucciones.text = "CONTINUAR";

        // 2. Mostramos la tarjeta y escondemos la cámara temporalmente
        tarjetaInstrucciones.SetActive(true);
        escanerAnimado.SetActive(false);
        botonCamara.SetActive(false);
        if (tarjetaAviso != null) tarjetaAviso.SetActive(false);
    }

    // Vincula esta función al botón CONTINUAR de tu tarjeta
    public void ActivarModoEscaneo()
    {
        tarjetaInstrucciones.SetActive(false);
        escanerAnimado.SetActive(true);
        botonCamara.SetActive(true);
    }

    // Vincula esta función al botón rojo (Button_Camara)
    public void BotonRojoPresionado()
    {
        if (tarjetaAviso != null)
        {
            tarjetaAviso.SetActive(true);
            textoAviso.text = "¡Estás en el Modo Práctica!\nVe a la pestaña LECCIONES para iniciar un escenario oficial.";

            CancelInvoke("OcultarAviso");
            Invoke("OcultarAviso", 3.5f);
        }
    }

    private void OcultarAviso()
    {
        if (tarjetaAviso != null) tarjetaAviso.SetActive(false);
    }
}