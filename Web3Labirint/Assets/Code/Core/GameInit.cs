using System;
using System.Collections.Generic;
using UnityEngine;
using MazeUtils.Spawn;
using Code.Menu;

internal sealed class GameInit : IDisposable
{
    private MetaData _data;
    private MenuController _menuController;

    private PlayerController _playerController;
    private MazeController _mazeController;
    private CameraController _cameraController;
    public GameInit(ControllerManager behaviourController, GameController mainController)
    {
        _data = Resources.Load<MetaData>("MetaData");
        _playerController = new PlayerController(_data.PlayerData);
        behaviourController.Add(_playerController);
        _mazeController = new MazeController(_data.MazeData);
        //behaviourController.Add(_playerController);
        //_menuController = new MenuController(_data.UiData);
        behaviourController.Add(_menuController);
        _cameraController = new CameraController(_data.CameraData);
        _cameraController.SetTarget(_playerController.View.gameObject);
        behaviourController.Add(_cameraController);
    }



    public void Dispose()
    {
    }
}