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
    public ControladorAudio gestorAudio;
    public AudioClip audioInstrucciones;
    public AudioClip audioAdvertencia;

    void OnEnable()
    {
        // Bloquea el audio si la app acaba de abrirse (los primeros 2 segundos)
        bool permitirAudio = Time.timeSinceLevelLoad > 2f;
        ReiniciarEscaner(permitirAudio);
    }

    public void ReiniciarEscaner(bool reproducirAudio = true)
    {
        if (tituloInstrucciones != null) tituloInstrucciones.text = "ESTE ES EL ESCÁNER";
        if (textoInstrucciones != null) textoInstrucciones.text = "Asegúrate de tener buena luz. Apunta la cámara al código QR y toca CONTINUAR cuando estés listo.";
        if (textoBotonInstrucciones != null) textoBotonInstrucciones.text = "CONTINUAR";

        tarjetaInstrucciones.SetActive(true);
        escanerAnimado.SetActive(false);
        botonCamara.SetActive(false);
        if (tarjetaAviso != null) tarjetaAviso.SetActive(false);

        if (reproducirAudio && gestorAudio != null)
        {
            gestorAudio.ReproducirVoz(audioInstrucciones);
        }
    }

    public void ActivarModoEscaneo()
    {
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