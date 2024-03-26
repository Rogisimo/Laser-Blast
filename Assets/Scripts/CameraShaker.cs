using UnityEngine;
using System.Collections;

public class CameraShaker : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null; // Wait until next frame
        }

        // Reset to original position
        transform.localPosition = originalPosition;
    }

    public void ShakeCamera(float duration, float magnitude){
        StartCoroutine(Shake(duration,magnitude));
    }
}
