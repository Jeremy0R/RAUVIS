using UnityEngine;

public class BotonContinuarGlobal : MonoBehaviour
{
    // You'll need to assign these in the Inspector
    public ControladorEscenario1 managerE1;
    public ControladorEscenario2 managerE2;
    // ... add more as you build scenarios

    // A variable to track the active scenario
    public int escenarioActivo = 1;

    public void EjecutarContinuar()
    {
        if (escenarioActivo == 1)
        {
            managerE1.BotonContinuarBase();
        }
        else if (escenarioActivo == 2)
        {
            managerE2.BotonContinuarBase();
        }
    }
}