using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class UserNameController:IExecute
{
    UserNameData _data;
    UserNameView _view;
    PlayerView _playerView;
    public UserNameView View { get => _view; }
    public PlayerView PlayerView { get => _playerView; set => _playerView = value; }

    public UserNameController(UserNameData data, PlayerView playerView)
    {
        _data = data;
        _playerView = playerView;
        _view = GameObject.Instantiate<UserNameView>(_data.View);
        _view.Text.text = PlayerPrefs.GetString("JwtUserName");
    }

    public void Execute()
    {
        _view.transform.position = new Vector3(PlayerView.transform.position.x, _view.transform.position.y, PlayerView.transform.position.z);
    }
}
