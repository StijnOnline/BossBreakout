using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MyTriggerEvent : UnityEvent<GameObject, Collider> { }
[System.Serializable]
public class MyTriggerEvent2D : UnityEvent<GameObject, Collider2D> { }

public class TriggerEvent : MonoBehaviour
{
    public MyTriggerEvent triggerEvent = new MyTriggerEvent();
    public MyTriggerEvent2D triggerEvent2D = new MyTriggerEvent2D();

    private void OnTriggerEnter(Collider other) {
        triggerEvent.Invoke(gameObject, other);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        triggerEvent2D.Invoke(gameObject, other);
    }
}