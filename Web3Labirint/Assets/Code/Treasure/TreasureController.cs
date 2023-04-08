using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TreasureController :ICleanup
{
    private TreasureData _data;
    private TreasureView _view;
    public event Action OnWin;
    public TreasureController(TreasureData data)
    {
        _data = data;
        _view = GameObject.Instantiate<TreasureView>(_data.View, new Vector3(UnityEngine.Random.Range(0, 20)*3-0.5f, 1, UnityEngine.Random.Range(0, 20) * 3 +2.5f), Quaternion.identity);
        _view.OnCatchTreasure += Win;
    }

    public void Cleanup()
    {
        _view.OnCatchTreasure -= Win;
    }

    public void Win()
    {
        OnWin.Invoke();
    }
}
