using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector2 originPos = transform.localPosition;
        float timer = .0f;
        float xPos, yPos;

        while(timer < duration)
        {
            xPos = magnitude * Random.Range(-1f, 1f);
            yPos = magnitude * Random.Range(-1f, 1f);

            transform.localPosition = new Vector2(xPos, yPos);

            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = originPos;
    }
}
