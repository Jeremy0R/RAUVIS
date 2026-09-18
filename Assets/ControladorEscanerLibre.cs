/* ==============================================================================
 * PROYECTO: RAUVIS 
 * SCRIPT: ControladorEscanerLibre.cs
 * DESCRIPCIÓN: Gestiona la vista "Escáner" del menú principal (Modo Práctica).
 *              Controla la transición de instrucciones y da retroalimentación 
 *              al presionar el botón de captura simulado.
 * ============================================================================== */

using UnityEngine;
using TMPro;

public class ControladorEscanerLibre : MonoBehaviour
{
    [Header("Interfaz Principal")]
    public GameObject tarjetaInstrucciones; // Arrastra aquí tu Card_Scan
    public GameObject escanerAnimado;       // Arrastra aquí tu Escaner_animado
    public GameObject botonCamara;          // Arrastra aquí tu Button_Camara

    [Header("Retroalimentación (Botón Rojo)")]
    public GameObject tarjetaAviso;         // Arrastra tu nueva Tarjeta_Aviso
    public TextMeshProUGUI textoAviso;      // Arrastra el texto dentro de la Tarjeta_Aviso

    // Se ejecuta automáticamente cada vez que el usuario abre la pestaña Escáner
    void OnEnable()
    {
        ReiniciarEscaner();
    }

    public void ReiniciarEscaner()
    {
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

            // Cancela cualquier temporizador previo y oculta el aviso en 3.5 segundos
            CancelInvoke("OcultarAviso");
            Invoke("OcultarAviso", 3.5f);
        }
    }

    private void OcultarAviso()
    {
        if (tarjetaAviso != null) tarjetaAviso.SetActive(false);
    }
}