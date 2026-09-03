using UnityEngine;

public class ControladorMaestro : MonoBehaviour
{
    [Header("Conecta tus Escenarios")]
    public ControladorEscenario1 managerE1;
    public ControladorEscenario2 managerE2;

    [Header("Marcadores Vuforia (Image Targets)")]
    public GameObject[] targetsE1; // Aquí pondremos los del nivel 1
    public GameObject[] targetsE2; // Aquí pondremos los del nivel 2

    [Header("Estado Actual")]
    public int escenarioActivo = 1;

    void Start()
    {
        // Al iniciar la app, forzamos a que solo los targets del E1 existan para la cámara
        ActivarTargetsEscenario(1);
    }

    // --- BOTÓN DE CARD_BASE ---
    public void BotonBasePresionado()
    {
        if (escenarioActivo == 1) managerE1.BotonContinuarBase();
        else if (escenarioActivo == 2) managerE2.BotonContinuarBase();
    }

    // --- BOTÓN DE CARD_ERROR ---
    public void BotonErrorPresionado()
    {
        if (escenarioActivo == 1) managerE1.MostrarOpciones();
        else if (escenarioActivo == 2) managerE2.BotonIntentarDeNuevo();
    }

    // --- BOTÓN DE CARD_CORRECTO ---
    public void BotonCorrectoPresionado()
    {
        if (escenarioActivo == 1)
        {
            CambiarEscenarioActivo(2); // ¡Pasamos al Escenario 2!
            managerE2.tarjetaCorrecto.SetActive(false); // Apagamos la de victoria
        }
        else if (escenarioActivo == 2)
        {
            // Lógica futura para pasar al Escenario 3
        }
    }

    // --- BOTÓN DE CARD_AYUDA ---
    public void BotonAyudaEntendido()
    {
        if (escenarioActivo == 1) managerE1.MostrarOpciones();
        else if (escenarioActivo == 2) managerE2.BotonIntentarDeNuevo();
    }

    // --- CONTROL DE FLUJO Y VUFORIA ---
    public void CambiarEscenarioActivo(int numeroNivel)
    {
        escenarioActivo = numeroNivel;
        ActivarTargetsEscenario(numeroNivel); // Apaga los targets viejos y prende los nuevos
    }

    private void ActivarTargetsEscenario(int nivel)
    {
        // 1. Apagamos absolutamente TODOS los targets
        foreach (GameObject target in targetsE1) { target.SetActive(false); }
        foreach (GameObject target in targetsE2) { target.SetActive(false); }

        // 2. Encendemos SOLO los que corresponden al nivel actual
        if (nivel == 1)
        {
            foreach (GameObject target in targetsE1) { target.SetActive(true); }
        }
        else if (nivel == 2)
        {
            foreach (GameObject target in targetsE2) { target.SetActive(true); }
        }
    }
}