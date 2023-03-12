using System;
using System.Collections.Generic;
using UnityEngine;
internal sealed class GameInit : IDisposable
{
    private MetaData _data;

    private PlayerController _playerController;
    public GameInit(ControllerManager behaviourController, GameController mainController)
    {
        _data = Resources.Load<MetaData>("MetaData");
        _playerController = new PlayerController(_data.PlayerData);
        behaviourController.Add(_playerController);
    }



    public void Dispose()
    {
    }
}