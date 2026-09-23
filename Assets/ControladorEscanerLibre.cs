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
    public TextMeshProUGUI tituloAviso;
    public TextMeshProUGUI textoAviso;

    [Header("Conexión de Audio")]
    public ControladorAudio gestorAudio; // NUEVO
    public AudioClip audioInstrucciones; // Audio: "Este es el escáner..."
    public AudioClip audioAdvertencia;   // Audio: "¡Estás en modo práctica!..."

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

        // NUEVO: Reproducimos el audio al abrir la pestaña del Escáner
        if (gestorAudio != null) gestorAudio.ReproducirVoz(audioInstrucciones);
    }

    public void ActivarModoEscaneo()
    {
        // NUEVO: Silenciamos por si Botty seguía hablando
        if (gestorAudio != null) gestorAudio.DetenerVoz();

        tarjetaInstrucciones.SetActive(false);
        escanerAnimado.SetActive(true);
        botonCamara.SetActive(true);
    }

    public void BotonRojoPresionado()
    {
        if (tarjetaAviso != null)
        {
            escanerAnimado.SetActive(false);
            botonCamara.SetActive(false);

            tarjetaAviso.SetActive(true);

            if (tituloAviso != null) tituloAviso.text = "¡MODO PRÁCTICA!";
            if (textoAviso != null) textoAviso.text = "Ve a la pestaña LECCIONES para iniciar un escenario oficial.";

            // NUEVO: Reproducimos el audio de advertencia
            if (gestorAudio != null) gestorAudio.ReproducirVoz(audioAdvertencia);

            CancelInvoke("OcultarAviso");
            Invoke("OcultarAviso", 5.5f);
        }
    }

    private void OcultarAviso()
    {
        if (tarjetaAviso != null) tarjetaAviso.SetActive(false);

        escanerAnimado.SetActive(true);
        botonCamara.SetActive(true);
    }
}