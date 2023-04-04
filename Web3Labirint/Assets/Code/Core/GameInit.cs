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
    private TreasureController _treasureController;
    private BonusController _bonusController;
    private UserNameController _userNameController;
    public GameInit(ControllerManager behaviourController, GameController mainController)
    {
        _data = Resources.Load<MetaData>("MetaData");
        _playerController = new PlayerController(_data.PlayerData);
        behaviourController.Add(_playerController);
        _mazeController = new MazeController(_data.MazeData);
        _menuController = new MenuController(_data.UiData);
        _menuController.OnStartGame += _playerController.ActivateUI;
        behaviourController.Add(_menuController);
        _cameraController = new CameraController(_data.CameraData);
        _cameraController.SetTarget(_playerController.View.gameObject);
        behaviourController.Add(_cameraController);
        _treasureController = new TreasureController(_data.TreasureData);
        behaviourController.Add(_treasureController);
        _bonusController = new BonusController(_data.BonusData);
        _bonusController.OnBunusApyed += _playerController.ChalkController.Refresh;
        _userNameController = new UserNameController(_data.UserNameData);

    }



    public void Dispose()
    {
        _menuController.OnStartGame -= _playerController.ActivateUI;
        _bonusController.OnBunusApyed += _playerController.ChalkController.Refresh;
    }
}