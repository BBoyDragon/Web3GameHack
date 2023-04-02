using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TreasureView : MonoBehaviour
{
    public event Action OnCatchTreasure;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerView>())
        {
            OnCatchTreasure?.Invoke();
        }
    }
}
