using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserNameView : MonoBehaviour
{
    UserNameData _data;
    Canvas _canvas;
    UserNameView _view;

    public void setFields(UserNameData data, Canvas canvas, UserNameView view)
    {
        _data = data;
        _canvas = canvas;
        _view = view;
    } 
    public void SetUserName(string userName)
    {
        var text = GameObject.Instantiate(_data.Text, _canvas.transform.position, _canvas.transform.rotation, _canvas.transform);
        text.GetComponent<TextMeshProUGUI>().text = userName;
    }
}
