using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishCintroller:ICleanup
{
    private FinishData _data;
    private FinishView _winView;
    private FinishView _loseView;

    public FinishCintroller(FinishData finishData)
    {
        _data = finishData;
    }

    public void Win()
    {
        _winView = GameObject.Instantiate<FinishView>(_data.WinView);
        _winView.Init();
        _winView.OnRestart += Restart;
       
    }
    public void Lose()
    {
        _loseView = GameObject.Instantiate<FinishView>(_data.LoseView);
        _loseView.Init();
        _loseView.OnRestart += Restart;
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Cleanup()
    {
        if (_winView != null)
        {
            _winView.OnRestart -= Restart;
        }
        if (_loseView != null)
        {
            _loseView.OnRestart -= Restart;
        }
    }
}
