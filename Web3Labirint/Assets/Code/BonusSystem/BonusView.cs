using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusView : MonoBehaviour
{
    public event Action<BonusView> OnBonusUsed;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerView>())
        {
            OnBonusUsed?.Invoke(this);
        }
    }
}
