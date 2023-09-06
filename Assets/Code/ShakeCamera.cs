using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public IEnumerator shake(float duration, float magnitude)
    {
        Vector2 OriginalPosition = transform.localPosition;
        float TimeElapsed = 0;
        while(TimeElapsed < duration)
        {
            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;

            transform.localPosition = new Vector3(x, y);

            TimeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = OriginalPosition;
    }
}
