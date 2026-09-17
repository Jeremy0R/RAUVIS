/* ==============================================================================
 * PROYECTO: RAUVIS (Realidad Aumentada para la detección de Phishing y Estafas)
 * SCRIPT: ControladorMaestro.cs
 * DESCRIPCIÓN: Es el "Cerebro Central" de la experiencia AR. Controla qué 
 *              escenario (Image Target) está activo, guarda el progreso global 
 *              del usuario y funciona como enrutador para los botones genéricos 
 *              de la interfaz compartida.
 * ============================================================================== */

using UnityEngine;

public class ControladorMaestro : MonoBehaviour
{
    // --- CONEXIONES CON LOS NIVELES ---
    [Header("Conecta tus Escenarios (Managers)")]
    public ControladorBienvenida managerBienvenida;
    public ControladorEscenario1 managerE1;
    public ControladorEscenario2 managerE2;
    public ControladorEscenario3 managerE3;
    public ControladorEscenario4 managerE4;
    public ControladorEscenario5 managerE5;
    public ControladorEscenario6 managerE6;

    // --- CONEXIONES CON VUFORIA ---
    [Header("Marcadores Vuforia (Image Targets)")]
    public GameObject[] targetsE1;
    public GameObject[] targetsE2;
    public GameObject[] targetsE3;
    public GameObject[] targetsE4;
    public GameObject[] targetsE5;
    public GameObject[] targetsE6;

    [Header("Interfaz Global")]
    public GameObject pantallaInstruccion; // Interfaz genérica del escáner

    [Header("Estado Actual")]
    public int escenarioActivo = 1;

    void Start()
    {
        // Al arrancar, activa solo la cámara del nivel correspondiente y apaga la UI genérica
        ActivarTargetsEscenario(escenarioActivo);
        if (pantallaInstruccion != null) pantallaInstruccion.SetActive(false);
    }

    public void OcultarInstruccionGlobal()
    {
        if (pantallaInstruccion != null) pantallaInstruccion.SetActive(false);
    }

    // ==============================================================================
    // ENRUTADOR DE BOTONES GLOBALES
    // Estos métodos reciben el clic del Canvas y lo mandan al escenario activo.
    // ==============================================================================

    public void BotonBasePresionado()
    {
        if (escenarioActivo == 0) managerBienvenida.AvanzarPaso();
        else if (escenarioActivo == 1) managerE1.BotonContinuarBase();
        else if (escenarioActivo == 2) managerE2.BotonContinuarBase();
        else if (escenarioActivo == 3) managerE3.BotonContinuarBase();
        else if (escenarioActivo == 4) managerE4.BotonContinuarBase();
        else if (escenarioActivo == 5) managerE5.BotonContinuarBase();
        else if (escenarioActivo == 6) managerE6.BotonContinuarBase();
    }

    public void BotonErrorPresionado()
    {
        if (escenarioActivo == 1) managerE1.MostrarOpciones();
        else if (escenarioActivo == 2) managerE2.BotonIntentarDeNuevo();
        else if (escenarioActivo == 3) managerE3.BotonBorrarPresionado();
        else if (escenarioActivo == 4) managerE4.RestaurarDespuesDeError();
        else if (escenarioActivo == 5) managerE5.RestaurarDespuesDeError();
        else if (escenarioActivo == 6) managerE6.RestaurarDespuesDeError();
    }

    public void BotonCorrectoPresionado()
    {
        if (escenarioActivo == 1)
        {
            CambiarEscenarioActivo(2);
            managerE1.tarjetaCorrecto.SetActive(false);
        }
        else if (escenarioActivo == 2)
        {
            CambiarEscenarioActivo(3);
            managerE2.tarjetaCorrecto.SetActive(false);
            managerE2.grupoOpciones.SetActive(false);
        }
        else if (escenarioActivo == 3)
        {
            CambiarEscenarioActivo(4);
            managerE3.tarjetaCorrecto.SetActive(false);
            managerE3.grupoOpciones.SetActive(false);
        }
        else if (escenarioActivo == 4)
        {
            CambiarEscenarioActivo(5);
            managerE4.tarjetaCorrecto.SetActive(false);
            managerE4.grupoOpciones.SetActive(false);
        }
        else if (escenarioActivo == 5)
        {
            CambiarEscenarioActivo(6);
            managerE5.tarjetaCorrecto.SetActive(false);
            managerE5.grupoOpciones.SetActive(false);
            if (managerE5.pantallaLupa != null) managerE5.pantallaLupa.SetActive(false);
        }
        else if (escenarioActivo == 6)
        {
            // EL GRAN FINAL
            managerE6.tarjetaCorrecto.SetActive(false);
            managerE6.grupoOpciones.SetActive(false);
            managerE6.MostrarFelicitacion();
        }
    }

    public void BotonAyudaPresionado()
    {
        if (escenarioActivo == 1) managerE1.MostrarAyuda();
        else if (escenarioActivo == 2) managerE2.MostrarAyuda();
        else if (escenarioActivo == 3) managerE3.MostrarAyuda();
        else if (escenarioActivo == 4) managerE4.MostrarAyuda();
        else if (escenarioActivo == 5) managerE5.MostrarAyuda();
        else if (escenarioActivo == 6) managerE6.MostrarAyuda();
    }

    public void BotonAyudaEntendido()
    {
        if (escenarioActivo == 1) managerE1.MostrarOpciones();
        else if (escenarioActivo == 2) managerE2.BotonIntentarDeNuevo();
        else if (escenarioActivo == 3) managerE3.OcultarAyuda();
        else if (escenarioActivo == 4) managerE4.OcultarAyuda();
        else if (escenarioActivo == 5) managerE5.OcultarAyuda();
        else if (escenarioActivo == 6) managerE6.OcultarAyuda();
    }

    // NUEVO: Enrutador para el botón de Atrás (<)
    public void BotonAtrasPresionado()
    {
        if (escenarioActivo == 1) managerE1.RegresarAlMenuPrincipal();
        else if (escenarioActivo == 2) managerE2.RegresarAlMenuPrincipal();
        else if (escenarioActivo == 3) managerE3.RegresarAlMenuPrincipal();
        else if (escenarioActivo == 4) managerE4.RegresarAlMenuPrincipal();
        else if (escenarioActivo == 5) managerE5.RegresarAlMenuPrincipal();
        else if (escenarioActivo == 6) managerE6.RegresarAlMenuPrincipal();
    }

    // ==============================================================================
    // GESTIÓN DE NIVELES Y PERSISTENCIA (GUARDADO)
    // ==============================================================================

    public void CambiarEscenarioActivo(int numeroNivel)
    {
        escenarioActivo = numeroNivel;

        // SISTEMA ANTIRROBO DE PROGRESO:
        // Solo guardamos si el nuevo nivel alcanzado es mayor al histórico.
        // Así, si alguien va en el Nivel 6 y repite el 1, no pierde su progreso.
        int nivelHistorico = PlayerPrefs.GetInt("NivelGuardado", 1);
        if (numeroNivel > nivelHistorico)
        {
            PlayerPrefs.SetInt("NivelGuardado", numeroNivel);
            PlayerPrefs.Save();
        }

        ActivarTargetsEscenario(numeroNivel);
        if (pantallaInstruccion != null) pantallaInstruccion.SetActive(true);
    }

    // Apaga todas las cámaras/marcadores y solo enciende los del nivel actual
    private void ActivarTargetsEscenario(int nivel)
    {
        foreach (GameObject target in targetsE1) { target.SetActive(false); }
        foreach (GameObject target in targetsE2) { target.SetActive(false); }
        foreach (GameObject target in targetsE3) { target.SetActive(false); }
        foreach (GameObject target in targetsE4) { target.SetActive(false); }
        foreach (GameObject target in targetsE5) { target.SetActive(false); }
        foreach (GameObject target in targetsE6) { target.SetActive(false); }

        if (nivel == 1) foreach (GameObject target in targetsE1) { target.SetActive(true); }
        else if (nivel == 2) foreach (GameObject target in targetsE2) { target.SetActive(true); }
        else if (nivel == 3) foreach (GameObject target in targetsE3) { target.SetActive(true); }
        else if (nivel == 4) foreach (GameObject target in targetsE4) { target.SetActive(true); }
        else if (nivel == 5) foreach (GameObject target in targetsE5) { target.SetActive(true); }
        else if (nivel == 6) foreach (GameObject target in targetsE6) { target.SetActive(true); }
    }
}