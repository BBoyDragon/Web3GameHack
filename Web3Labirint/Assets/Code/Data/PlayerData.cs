using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerData :ScriptableObject
{
    [SerializeField]
    private int _speed;
    [SerializeField]
    private PlayerView _view;
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Joystick _joystick;
    [SerializeField]
    private Canvas _canvas;

   


    public int Speed { get => _speed;}
    public PlayerView View { get => _view; }
    public Camera Camera { get => _camera; }
    public Joystick Joystick { get => _joystick; }
    public Canvas Canvas { get => _canvas; set => _canvas = value; }
}
