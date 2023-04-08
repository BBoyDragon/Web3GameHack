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
    private FinishController _finishController;
    public GameInit(ControllerManager behaviourController, GameController mainController)
    {
        _data = Resources.Load<MetaData>("MetaData");
        _cameraController = new CameraController(_data.CameraData);
        _userNameController = new UserNameController(_data.UserNameData, _data.PlayerData.View);
        _playerController = new PlayerController(_data.PlayerData, _cameraController, _userNameController);
        behaviourController.Add(_playerController);
        _mazeController = new MazeController(_data.MazeData);
        _menuController = new MenuController(_data.UiData, _playerController);
        _menuController.OnStartGame += _playerController.ActivateUI;
        behaviourController.Add(_menuController);
        behaviourController.Add(_cameraController);
        _treasureController = new TreasureController(_data.TreasureData);
        behaviourController.Add(_treasureController);
        _bonusController = new BonusController(_data.BonusData);
        _bonusController.OnBunusApyed += _playerController.ChalkController.Refresh;

        _userNameController.PlayerView = _playerController.View;
        behaviourController.Add(_userNameController);

        _finishController = new FinishController(_data.FinishData);
        _playerController.OnDie += _finishController.Lose;
        _playerController.OnDie += _playerController.DeactivateUI;
        _treasureController.OnWin += _finishController.Win;
        _treasureController.OnWin += _playerController.DeactivateUI;

    }

    public void Dispose()
    {
        _menuController.OnStartGame -= _playerController.ActivateUI;
        _bonusController.OnBunusApyed -= _playerController.ChalkController.Refresh;
        _playerController.OnDie -= _finishController.Lose;
        _treasureController.OnWin -= _finishController.Win;
        _playerController.OnDie -= _playerController.DeactivateUI;
        _treasureController.OnWin -= _playerController.DeactivateUI;
    }
}