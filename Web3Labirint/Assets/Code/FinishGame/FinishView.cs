using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishView : MonoBehaviour
{
    [SerializeField]
    private Button _restart;

    public event Action OnRestart;

    public void Init()
    {
        _restart.onClick.AddListener(OnButton);
    }
    public void Clear()
    {
        _restart.onClick.RemoveAllListeners();
    }
    private void OnButton()
    {
        OnRestart.Invoke();
    }
}
