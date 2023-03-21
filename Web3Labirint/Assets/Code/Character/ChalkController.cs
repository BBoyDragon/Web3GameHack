using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChalkController :IExecute
{
    private PlayerView _playerView;
    private ChalkData _data;
    private ChalkView _view;
    Vector3 lastDistance;

    private float _curentAmountOfChalk;

    private float _onePercentofChalk;
    public ChalkController(PlayerView view,ChalkData data)
    {
        _playerView = view;
        _data = data;
        _view = GameObject.Instantiate<ChalkView>(_data.View1);
        _onePercentofChalk = _data.MaxAmountOfChalk / 100;
        _curentAmountOfChalk = 100*_onePercentofChalk;
    }

    public void Execute()
    {

        RaycastHit hit;
        if (Vector3.Distance(_playerView.transform.position, lastDistance) >= 0.5)
        {
            if (Physics.Raycast(_playerView.transform.position, Vector3.down, out hit))
            {
                var go = GameObject.Instantiate(_data.Brush, hit.point + Vector3.up * 0.1f, Quaternion.identity);
                lastDistance = go.transform.position;
                _view.ChalkMeter.fillAmount = (_curentAmountOfChalk / _onePercentofChalk) / 100;
                _curentAmountOfChalk -= _data.ChalkPerSecond * Time.deltaTime;
            }
        }

        if (_curentAmountOfChalk <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
