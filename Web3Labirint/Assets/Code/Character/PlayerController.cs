using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController :IExecute,ICleanup
{
    PlayerData _data;
    CameraController _cameraController;
    UserNameController _userNameController;
    MovementController _movementController;
    ChalkController _chalkController;
    Canvas _canvas;
    PlayerView _view;

    private bool _isDead=false;
    public event Action OnDie;

    public ChalkController ChalkController { get => _chalkController; }
    public PlayerView View { get => _view; set => _view = value; }

    public PlayerController(PlayerData data, CameraController cameraController, UserNameController userNameController)
    {
        _cameraController = cameraController;
        _userNameController = userNameController;
        _data = data;
        _view = GameObject.Instantiate<PlayerView>(_data.View);
        _canvas = GameObject.Instantiate<Canvas>(_data.Canvas);
        View.Init();
        _cameraController.SetTarget(_view.gameObject);
        _movementController = new MovementController(View,_data.Speed,_data.Joystick,_canvas);
        _chalkController = new ChalkController(View, _data.ChalkData);
        _chalkController.OnDie += Die;
    }

    public void Execute()
    {
        if (!_isDead)
        {
            _movementController.Execute();
            ChalkController.Execute();
        }
    }
    public void ActivateUI()
    {
        _canvas.gameObject.SetActive(true);
        ChalkController.ActivateUI();
    }
    public void DeactivateUI()
    {
        _canvas.gameObject.SetActive(false);
        ChalkController.DeactivateUI();
    }
    private void Die()
    {
        _isDead = true;
        OnDie.Invoke();
    }

    public void ResetView(PlayerView view)
    {
        UnityEngine.Object.Destroy(_view.gameObject);
        view.Init();
        _view = view;
        _cameraController.SetTarget(view.gameObject);
        _userNameController.PlayerView = view;
        _movementController.View = view;
        _chalkController.PlayerView = view;
    }

    public void Cleanup()
    {
        _chalkController.OnDie -= Die;
    }
}
