/* ==============================================================================
 * PROYECTO: RAUVIS (Realidad Aumentada para la detección de Phishing y Estafas)
 * SCRIPT: ControladorAudio.cs
 * DESCRIPCIÓN: Gestor global de audio (Cerebro de Audio). Se encarga de reproducir 
 *              y detener las narraciones de Botty en toda la aplicación, 
 *              respetando la configuración de sonido guardada por el usuario.
 * ============================================================================== */

using UnityEngine;

public class ControladorAudio : MonoBehaviour
{
    [Header("Configuración de Audio")]
    [Tooltip("El componente AudioSource que emitirá la voz de Botty.")]
    public AudioSource bocina;

    /// <summary>
    /// Reproduce un clip de audio si la narración está activada en los ajustes.
    /// Detiene cualquier audio previo para evitar que las voces se encimen.
    /// </summary>
    /// <param name="clip">El archivo de audio (.mp3 o .wav) a reproducir.</param>
    public void ReproducirVoz(AudioClip clip)
    {
        // Si no hay un audio asignado en el inspector, cancelamos la acción para evitar errores
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

    /// <summary>
    /// Detiene inmediatamente la reproducción de la voz de Botty.
    /// Útil cuando el usuario cambia de pantalla o cierra una tarjeta rápidamente.
    /// </summary>
    public void DetenerVoz()
    {
        if (bocina != null)
        {
            bocina.Stop();
        }
    }
}