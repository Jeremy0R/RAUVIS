using UnityEngine;

public class ControladorMaestro : MonoBehaviour
{
    [Header("Conecta tus Escenarios")]
    public ControladorBienvenida managerBienvenida;
    public ControladorEscenario1 managerE1;
    public ControladorEscenario2 managerE2;
    public ControladorEscenario3 managerE3;
    public ControladorEscenario4 managerE4;
    public ControladorEscenario5 managerE5;
    public ControladorEscenario6 managerE6; // NUEVO

    [Header("Marcadores Vuforia (Image Targets)")]
    public GameObject[] targetsE1;
    public GameObject[] targetsE2;
    public GameObject[] targetsE3;
    public GameObject[] targetsE4;
    public GameObject[] targetsE5;
    public GameObject[] targetsE6; // NUEVO

    [Header("Interfaz Global")]
    public GameObject pantallaInstruccion;

    [Header("Estado Actual")]
    public int escenarioActivo = 1;

    void Start()
    {
        ActivarTargetsEscenario(escenarioActivo);
        if (pantallaInstruccion != null) pantallaInstruccion.SetActive(false);
    }

    public void OcultarInstruccionGlobal()
    {
        if (pantallaInstruccion != null) pantallaInstruccion.SetActive(false);
    }

    public void BotonBasePresionado()
    {
        if (escenarioActivo == 0) managerBienvenida.AvanzarPaso(); // <-- ESTA LÍNEA NUEVA
        else if (escenarioActivo == 1) managerE1.BotonContinuarBase();
        else if (escenarioActivo == 2) managerE2.BotonContinuarBase();
        else if (escenarioActivo == 3) managerE3.BotonContinuarBase();
        else if (escenarioActivo == 4) managerE4.BotonContinuarBase();
        else if (escenarioActivo == 5) managerE5.BotonContinuarBase();
        else if (escenarioActivo == 6) managerE6.BotonContinuarBase(); // NUEVO
    }

    public void BotonErrorPresionado()
    {
        if (escenarioActivo == 1) managerE1.MostrarOpciones();
        else if (escenarioActivo == 2) managerE2.BotonIntentarDeNuevo();
        else if (escenarioActivo == 3) managerE3.BotonBorrarPresionado();
        else if (escenarioActivo == 4) managerE4.RestaurarDespuesDeError();
        else if (escenarioActivo == 5) managerE5.RestaurarDespuesDeError();
        else if (escenarioActivo == 6) managerE6.RestaurarDespuesDeError(); // NUEVO
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
            managerE6.MostrarFelicitacion(); // Llama a la tarjeta final en lugar de cambiar de nivel
        }
    }

    public void BotonAyudaPresionado()
    {
        if (escenarioActivo == 1) managerE1.MostrarAyuda();
        else if (escenarioActivo == 2) managerE2.MostrarAyuda();
        else if (escenarioActivo == 3) managerE3.MostrarAyuda();
        else if (escenarioActivo == 4) managerE4.MostrarAyuda();
        else if (escenarioActivo == 5) managerE5.MostrarAyuda();
        else if (escenarioActivo == 6) managerE6.MostrarAyuda(); // NUEVO
    }

    public void BotonAyudaEntendido()
    {
        if (escenarioActivo == 1) managerE1.MostrarOpciones();
        else if (escenarioActivo == 2) managerE2.BotonIntentarDeNuevo();
        else if (escenarioActivo == 3) managerE3.OcultarAyuda();
        else if (escenarioActivo == 4) managerE4.OcultarAyuda();
        else if (escenarioActivo == 5) managerE5.OcultarAyuda();
        else if (escenarioActivo == 6) managerE6.OcultarAyuda(); // NUEVO
    }

    public void CambiarEscenarioActivo(int numeroNivel)
    {
        escenarioActivo = numeroNivel;

        PlayerPrefs.SetInt("NivelGuardado", escenarioActivo);
        PlayerPrefs.Save();

        ActivarTargetsEscenario(numeroNivel);
        if (pantallaInstruccion != null) pantallaInstruccion.SetActive(true);
    }

    private void ActivarTargetsEscenario(int nivel)
    {
        foreach (GameObject target in targetsE1) { target.SetActive(false); }
        foreach (GameObject target in targetsE2) { target.SetActive(false); }
        foreach (GameObject target in targetsE3) { target.SetActive(false); }
        foreach (GameObject target in targetsE4) { target.SetActive(false); }
        foreach (GameObject target in targetsE5) { target.SetActive(false); }
        foreach (GameObject target in targetsE6) { target.SetActive(false); } // NUEVO

        if (nivel == 1) foreach (GameObject target in targetsE1) { target.SetActive(true); }
        else if (nivel == 2) foreach (GameObject target in targetsE2) { target.SetActive(true); }
        else if (nivel == 3) foreach (GameObject target in targetsE3) { target.SetActive(true); }
        else if (nivel == 4) foreach (GameObject target in targetsE4) { target.SetActive(true); }
        else if (nivel == 5) foreach (GameObject target in targetsE5) { target.SetActive(true); }
        else if (nivel == 6) foreach (GameObject target in targetsE6) { target.SetActive(true); } // NUEVO
    }
}