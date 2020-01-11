using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float shakeTime;
    public float baseShakeSpeed;
    public float shakeDistance;
    private Vector3 basePosition;

    private void Awake() {
        basePosition = transform.position;
        //Shake(1);
    }

    public void Shake(float intensity) {
        StartCoroutine(shake(intensity));
    }

    public IEnumerator shake(float intensity) {
        float timer = .0f;
        while(timer < shakeTime * intensity) {
            timer += Time.deltaTime;

            transform.position = basePosition + new Vector3(Mathf.Sin(timer * baseShakeSpeed * intensity) * shakeDistance * intensity, .0f, .0f);

            yield return 0;
        }

        transform.position = basePosition;
    }
}
