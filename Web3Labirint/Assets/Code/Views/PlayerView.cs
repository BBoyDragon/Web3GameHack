using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerView : MonoBehaviour
{
    private Rigidbody _rigidbody;
    //public event Action OnTrigger;

    //private void OnTriggerEnter(Collider other)
    //{
    //    OnTrigger?.Invoke();
    //}

    public void Init()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    public Rigidbody Rigidbody { get => _rigidbody; }
}
