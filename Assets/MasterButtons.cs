using UnityEngine;

public class ControladorMaestro : MonoBehaviour
{
    [Header("Conecta tus Escenarios")]
    public ControladorEscenario1 managerE1;
    public ControladorEscenario2 managerE2;
    public ControladorEscenario3 managerE3;

    [Header("Marcadores Vuforia (Image Targets)")]
    public GameObject[] targetsE1;
    public GameObject[] targetsE2;
    public GameObject[] targetsE3;

    [Header("Interfaz Global")]
    public GameObject pantallaInstruccion;

    [Header("Estado Actual")]
    public int escenarioActivo = 1;

    void Start()
    {
        ActivarTargetsEscenario(1);
        if (pantallaInstruccion != null) pantallaInstruccion.SetActive(true); // Se enciende al abrir la app
    }

    // --- NUEVA FUNCIÓN PARA APAGAR LA INSTRUCCIÓN ---
    public void OcultarInstruccionGlobal()
    {
        if (pantallaInstruccion != null) pantallaInstruccion.SetActive(false);
    }

    // --- BOTÓN DE CARD_BASE ---
    public void BotonBasePresionado()
    {
        if (escenarioActivo == 1) managerE1.BotonContinuarBase();
        else if (escenarioActivo == 2) managerE2.BotonContinuarBase();
        else if (escenarioActivo == 3) managerE3.BotonContinuarBase();
    }

    // --- BOTÓN DE CARD_ERROR ---
    public void BotonErrorPresionado()
    {
        if (escenarioActivo == 1) managerE1.MostrarOpciones();
        else if (escenarioActivo == 2) managerE2.BotonIntentarDeNuevo();
        else if (escenarioActivo == 3) managerE3.BotonBorrarPresionado();
    }

    // --- BOTÓN DE CARD_CORRECTO ---
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
            // Lógica futura para pasar al E4
        }
    }

    // --- BOTÓN PARA ABRIR LA AYUDA (?) ---
    public void BotonAyudaPresionado()
    {
        if (escenarioActivo == 1) managerE1.MostrarAyuda();
        else if (escenarioActivo == 2) managerE2.MostrarAyuda();
        else if (escenarioActivo == 3) managerE3.MostrarAyuda();
    }

    // --- BOTÓN PARA CERRAR LA AYUDA ---
    public void BotonAyudaEntendido()
    {
        if (escenarioActivo == 1) managerE1.MostrarOpciones();
        else if (escenarioActivo == 2) managerE2.BotonIntentarDeNuevo();
        else if (escenarioActivo == 3) managerE3.OcultarAyuda();
    }

    // --- CONTROL DE FLUJO Y VUFORIA ---
    public void CambiarEscenarioActivo(int numeroNivel)
    {
        escenarioActivo = numeroNivel;
        ActivarTargetsEscenario(numeroNivel);

        // Encendemos la instrucción global mientras el usuario busca el nuevo target
        if (pantallaInstruccion != null) pantallaInstruccion.SetActive(true);
    }

    private void ActivarTargetsEscenario(int nivel)
    {
        foreach (GameObject target in targetsE1) { target.SetActive(false); }
        foreach (GameObject target in targetsE2) { target.SetActive(false); }
        foreach (GameObject target in targetsE3) { target.SetActive(false); }

        if (nivel == 1)
        {
            foreach (GameObject target in targetsE1) { target.SetActive(true); }
        }
        else if (nivel == 2)
        {
            foreach (GameObject target in targetsE2) { target.SetActive(true); }
        }
        else if (nivel == 3)
        {
            foreach (GameObject target in targetsE3) { target.SetActive(true); }
        }
    }
}