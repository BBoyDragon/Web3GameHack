using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserNameView : MonoBehaviour
{
    UserNameData _data;
    Canvas _canvas;

    public void SetFields(UserNameData data, Canvas canvas)
    {
        _data = data;
        _canvas = canvas;
    } 
    public void SetUserName(string userName) // this method is called form JS and userName string puts here
    {
        var text = GameObject.Instantiate(_data.Text, _canvas.transform.position, _canvas.transform.rotation, _canvas.transform);
        text.GetComponent<TextMeshProUGUI>().text = userName + "\nThis account have no assets";
        var vec = new Vector3(_canvas.transform.position.x/2.0f, _canvas.transform.position.y*3/2.0f, _canvas.transform.position.z);
        text.transform.position = vec;
    }
}
