using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private Coroutine shakeCoroutine;

    private IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            // Fast up-and-down shake
            float x = Mathf.Sin(Time.time * 30f) * magnitude; // Adjusted frequency for visibility
            float y = Mathf.Sin(Time.time * 30f) * magnitude; // Adjusted frequency for visibility

            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition; // Reset position
    }

    public void TriggerShake(float duration, float magnitude)
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine); // Stop any ongoing shake
        }
        shakeCoroutine = StartCoroutine(Shake(duration, magnitude));
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
