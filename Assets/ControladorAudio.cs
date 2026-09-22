using UnityEngine;

public class ControladorAudio : MonoBehaviour
{
    public AudioSource bocina;

    public void ReproducirVoz(AudioClip clip)
    {
        if (clip == null) return;

        // Revisamos si el usuario tiene activada la narración (1 = Sí, 0 = No)
        // Por defecto, al instalar la app, estará en 1 (Activado)
        if (PlayerPrefs.GetInt("NarracionActivada", 1) == 1)
        {
            bocina.Stop(); // Detiene cualquier audio anterior para que no se encimen
            bocina.clip = clip;
            bocina.Play();
        }
    }

    public void DetenerVoz()
    {
        bocina.Stop();
    }
}