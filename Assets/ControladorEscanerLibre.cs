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
    public TextMeshProUGUI tituloInstrucciones;
    public TextMeshProUGUI textoInstrucciones;
    public TextMeshProUGUI textoBotonInstrucciones;

    public GameObject escanerAnimado;
    public GameObject botonCamara;

    [Header("Retroalimentación (Botón Rojo)")]
    public GameObject tarjetaAviso;
    public TextMeshProUGUI textoAviso;

    void OnEnable()
    {
        ReiniciarEscaner();
    }

    public void ReiniciarEscaner()
    {
        if (tituloInstrucciones != null) tituloInstrucciones.text = "ESTE ES EL ESCÁNER";
        if (textoInstrucciones != null) textoInstrucciones.text = "Asegúrate de tener buena luz. Apunta la cámara al código QR y toca CONTINUAR cuando estés listo.";
        if (textoBotonInstrucciones != null) textoBotonInstrucciones.text = "CONTINUAR";

        tarjetaInstrucciones.SetActive(true);
        escanerAnimado.SetActive(false);
        botonCamara.SetActive(false);
        if (tarjetaAviso != null) tarjetaAviso.SetActive(false);
    }

    public void ActivarModoEscaneo()
    {
        tarjetaInstrucciones.SetActive(false);
        escanerAnimado.SetActive(true);
        botonCamara.SetActive(true);
    }

    public void BotonRojoPresionado()
    {
        if (tarjetaAviso != null)
        {
            // Apagamos la interfaz del escáner para evitar que se encimen
            escanerAnimado.SetActive(false);
            botonCamara.SetActive(false);

            tarjetaAviso.SetActive(true);
            textoAviso.text = "¡Estás en el Modo Práctica!\nVe a la pestaña LECCIONES para iniciar un escenario oficial.";

            CancelInvoke("OcultarAviso");
            Invoke("OcultarAviso", 4.5f);
        }
    }

    private void OcultarAviso()
    {
        if (tarjetaAviso != null) tarjetaAviso.SetActive(false);

        // Devolvemos la interfaz del escáner a la pantalla
        escanerAnimado.SetActive(true);
        botonCamara.SetActive(true);
    }
}