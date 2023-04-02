using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TreasureController :ICleanup
{
    private TreasureData _data;
    private TreasureView _view;

    public TreasureController(TreasureData data)
    {
        _data = data;
        _view = GameObject.Instantiate<TreasureView>(_data.View, new Vector3(Random.Range(5, 55), 1, Random.Range(5, 55)), Quaternion.identity);
        _view.OnCatchTreasure += Win;
    }

    public void Cleanup()
    {
        _view.OnCatchTreasure -= Win;
    }

    public void Win()
    {
        SceneManager.LoadScene(0);
        //End Of the game
    }

}
