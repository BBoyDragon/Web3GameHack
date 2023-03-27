using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class UserNameController
{
    UserNameData _data;
    Canvas _canvas;
    UserNameView _view;

    public UserNameView View { get => _view;}

    public UserNameController(UserNameData data)
    {
        _data = data;
        _canvas = GameObject.Instantiate<Canvas>(_data.Canvas);
        _view = GameObject.Instantiate<UserNameView>(_data.View);
        _view.setFields(_data, _canvas, _view);
    }
}
