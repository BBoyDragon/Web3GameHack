using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserNameView : MonoBehaviour
{
    
    [SerializeField]
    private TMP_Text _text;
    public void SetUsetJWT(string jwtBody)
    {  
        JWT.UserJWT jwt = JWT.JWTParser.Parse(jwtBody);
        _text.text = jwt.username;
    }
}
