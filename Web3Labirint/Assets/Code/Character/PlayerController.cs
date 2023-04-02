using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController :IExecute,ICleanup
{
    PlayerData _data;
    MovementController _movementController;
    ChalkController _chalkController;
    Canvas _canvas;
    PlayerView _view;

    public PlayerView View { get => _view;}

    public PlayerController(PlayerData data)
    {
        _data = data;
        _view = GameObject.Instantiate<PlayerView>(_data.View);
        _canvas = GameObject.Instantiate<Canvas>(_data.Canvas);
        View.Init();
        _movementController = new MovementController(View,_data.Speed,_data.Joystick,_canvas);
        _chalkController = new ChalkController(View, _data.ChalkData);
    }

    public void Execute()
    {
        _movementController.Execute();
        _chalkController.Execute();
    }
    public void ActivateUI()
    {
        _canvas.gameObject.SetActive(true);
        _chalkController.ActivateUI();
    }

    public void Cleanup()
    {
    }
}
