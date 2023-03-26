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
    private Canvas _canvas;
    public Canvas Canvas { get => _canvas; set => _canvas = value; }

    [SerializeField]
    private UserNameView _view;
    public UserNameView View { get => _view; }
    
    [SerializeField]
    private GameObject _text;
    public GameObject Text { get => _text; }

    [SerializeField]
    private string _userName = "abacaba";
    public string UserName { get => _userName; set => _userName = value; }
}
