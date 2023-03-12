using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController :IExecute,ICleanup
{
    PlayerData _data;
    MovementController _movement;
    Canvas _canvas;
    PlayerView _view;

    public PlayerController(PlayerData data)
    {
        _data = data;
        _view = GameObject.Instantiate<PlayerView>(_data.View);
        _canvas = GameObject.Instantiate<Canvas>(_data.Canvas);
        _view.Init();
        _movement = new MovementController(_view,_data.Speed,_data.Joystick,_canvas);
    }

    public void Execute()
    {
        _movement.Execute();
    }
    public void Cleanup()
    {
    }
}
