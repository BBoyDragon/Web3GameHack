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
    public void SetUsetJWT(string jwtBody)
    {  
        JWT.UserJWT jwt = JWT.JWTParser.Parse(jwtBody);
        var text = GameObject.Instantiate(_data.Text, _canvas.transform.position, _canvas.transform.rotation, _canvas.transform);
        text.GetComponent<TextMeshProUGUI>().text = jwt.username;
        Debug.Log(jwt.username);
        var vec = new Vector3(_canvas.transform.position.x, _canvas.transform.position.y, _canvas.transform.position.z);
        text.transform.position = vec;
        _canvas.gameObject.SetActive(true);
    }
}
