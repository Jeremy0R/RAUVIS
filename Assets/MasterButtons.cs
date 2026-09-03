using UnityEngine;

public class ControladorMaestro : MonoBehaviour
{
    [Header("Conecta tus Escenarios")]
    public ControladorEscenario1 managerE1;
    public ControladorEscenario2 managerE2;
    // Aquí agregarás el 3, 4, 5 y 6 después

    [Header("Estado Actual")]
    public int escenarioActivo = 1;

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
            // Según tu libreta, ganar el E1 abre el E2 y pide escanear
            escenarioActivo = 2; // Cambiamos el cerebro al nivel 2
            managerE2.tarjetaCorrecto.SetActive(false); // Apagamos la tarjeta de victoria
            // Aquí puedes encender un panel que diga "Escanea el Escenario 2"
        }
        else if (escenarioActivo == 2)
        {
            // Lógica para cuando gane el E2 (pasar al 3)
        }
    }

    // --- BOTÓN DE CARD_AYUDA ---
    public void BotonAyudaEntendido()
    {
        // En ambos escenarios, el botón de ayuda suele regresarte a donde estabas
        if (escenarioActivo == 1) managerE1.MostrarOpciones();
        else if (escenarioActivo == 2) managerE2.BotonIntentarDeNuevo();
    }

    public void CambiarEscenarioActivo(int numeroNivel)
    {
        escenarioActivo = numeroNivel;
    }
}