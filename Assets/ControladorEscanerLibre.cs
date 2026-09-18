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
    public GameObject tarjetaInstrucciones; // La tarjeta blanca donde Botty explica qué hacer
    public GameObject interfazEscaneo;      // El marco de "SCAN ME", láser y el botón rojo

    [Header("Retroalimentación (Botón Rojo)")]
    public GameObject tarjetaAviso;         // Un pequeño panel para mensajes temporales
    public TextMeshProUGUI textoAviso;

    // OnEnable se ejecuta CADA VEZ que esta vista se enciende desde la barra de navegación
    void OnEnable()
    {
        ReiniciarEscaner();
    }

    public void ReiniciarEscaner()
    {
        tarjetaInstrucciones.SetActive(true);
        interfazEscaneo.SetActive(false);
        if (tarjetaAviso != null) tarjetaAviso.SetActive(false);
    }

    // Se ejecuta al presionar "CONTINUAR" en la tarjeta de Botty
    public void ActivarModoEscaneo()
    {
        tarjetaInstrucciones.SetActive(false);
        interfazEscaneo.SetActive(true);
    }

    // Se ejecuta al presionar el botón rojo de la cámara
    public void BotonRojoPresionado()
    {
        if (tarjetaAviso != null)
        {
            tarjetaAviso.SetActive(true);
            textoAviso.text = "¡Estás en el Modo Práctica!\nVe a la pestaña LECCIONES para iniciar un escenario oficial.";

            // Invoca la función para ocultar el aviso después de 3.5 segundos
            CancelInvoke("OcultarAviso");
            Invoke("OcultarAviso", 3.5f);
        }
    }

    private void OcultarAviso()
    {
        if (tarjetaAviso != null) tarjetaAviso.SetActive(false);
    }
}