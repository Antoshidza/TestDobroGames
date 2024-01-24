using System;
using Source.Common;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public event Action<GameObject> Triggered;

    private void Awake() 
        => this.DIRegisterMultiple();

    private void OnTriggerEnter(Collider other) 
        => Triggered?.Invoke(other.gameObject);
}
