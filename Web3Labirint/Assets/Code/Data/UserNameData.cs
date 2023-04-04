using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "UserNameData", menuName = "Data/UserNameData")]
public class UserNameData : ScriptableObject
{

    [SerializeField]
    private UserNameView _view;
    public UserNameView View { get => _view; }
}
