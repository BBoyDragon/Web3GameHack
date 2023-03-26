using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserNameView : MonoBehaviour
{
    [SerializeField]
    private UserNameData _userNameData;
    public UserNameData UserNameData { get => _userNameData; }

    public void SetUserName(string userName)
    {
        _userNameData.UserName = userName;
    }
}
