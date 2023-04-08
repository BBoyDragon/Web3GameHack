using System;
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

    public event Action OnDie;

    private float _curentAmountOfChalk;

    private float _onePercentofChalk;

    public PlayerView PlayerView { get => _playerView; set => _playerView = value; }

    public ChalkController(PlayerView view,ChalkData data)
    {
        PlayerView = view;
        _data = data;
        _view = GameObject.Instantiate<ChalkView>(_data.View1);
        _onePercentofChalk = _data.MaxAmountOfChalk / 100;
        _curentAmountOfChalk = _data.MaxAmountOfChalk;
    }

    public void Execute()
    {
        RaycastHit hit;
        if (Vector3.Distance(PlayerView.transform.position, lastDistance) >= 0.5)
        {
            if (Physics.Raycast(PlayerView.transform.position, Vector3.down, out hit))
            {
                var go = GameObject.Instantiate(_data.Brush, hit.point + Vector3.up * 0.1f, Quaternion.identity);
                lastDistance = go.transform.position;
                _view.ChalkMeter.fillAmount = (_curentAmountOfChalk / _onePercentofChalk) / 100;
                _curentAmountOfChalk -= _data.ChalkPerSecond * Time.deltaTime;
            }
        }

        if (_curentAmountOfChalk <= 0)
        {
            OnDie.Invoke();
        }
    }

    public void Refresh()
    {
        _curentAmountOfChalk = _data.MaxAmountOfChalk;
        _view.ChalkMeter.fillAmount = (_curentAmountOfChalk / _onePercentofChalk) / 100;
    }
    public void ActivateUI()
    {
        _view.gameObject.SetActive(true);
    }
    public void DeactivateUI()
    {
        _view.gameObject.SetActive(false);
    }
}
