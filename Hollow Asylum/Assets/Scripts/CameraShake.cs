using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private Coroutine shakeCoroutine;

    private IEnumerator Vibrate(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            // Stronger up and down vibration
            transform.localPosition = new Vector3(originalPosition.x, originalPosition.y + Mathf.Sin(elapsed * 30) * magnitude, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        transform.localPosition = originalPosition; // Reset position
    }

    public void TriggerVibrate(float duration, float magnitude)
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine); // Stop any ongoing vibration
        }
        shakeCoroutine = StartCoroutine(Vibrate(duration, magnitude));
    }

    public void StopShake()
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            transform.localPosition = Vector3.zero; // Reset position
            shakeCoroutine = null;
        }
    }
}

