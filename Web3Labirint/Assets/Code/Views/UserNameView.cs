using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserNameView : MonoBehaviour
{
    
    [SerializeField]
    private TMP_Text _text;

    public TMP_Text Text { get => _text;}

    public void SetUsetJWT(string jwtBody)
    {  

        JWT.UserJWT jwt = JWT.JWTParser.Parse(jwtBody);
        Text.text = jwt.username;
        PlayerPrefs.SetString("JwtUserName", jwt.username);
        PlayerPrefs.SetString("Sub", jwt.sub);
        PlayerPrefs.SetString("Wallet", jwt.wallet);     
        PlayerPrefs.SetString("Jwt", jwtBody);
    }
}
