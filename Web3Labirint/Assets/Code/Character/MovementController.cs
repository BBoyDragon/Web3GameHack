using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController :IExecute
{
    PlayerView _view;
    float _speed;
    float _rotationSpeed=10;
    Camera _camera;
    Joystick _joystick;
    Canvas _canvas;
    Vector3 _direction;
    Vector2 _rotation;
    public MovementController(PlayerView view,float speed,Joystick joystick,Canvas canvas)
    {
        _speed = speed;
        _view = view;
        _canvas = canvas;
        _camera = Camera.main;
        _joystick = GameObject.Instantiate<Joystick>(joystick,_canvas.transform);
    }

    public void Execute()
    {

        _view.Rigidbody.velocity = new Vector3(
                _joystick.Direction.x * _speed,
                _view.Rigidbody.velocity.y,
                _joystick.Direction.y * _speed);
            Rotate(_joystick.Direction);



        //if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        //{
        //    _direction=new Vector3(Input.GetAxis("Horizontal") * _speed, 0,
        //            Input.GetAxis("Vertical") * _speed);
        //    _view.Rigidbody.velocity = _direction;
        //}

        ////var point1 = (_camera.ScreenPointToRay(Input.mousePosition).GetPoint(15) -
        ////             _view.transform.position).normalized;
        //_rotation = new Vector2(_view.transform.position.x + _direction.x, _view.transform.position.z + _direction.z);//.normalized;
        //Rotate(_rotation);

    }
    private void Rotate(Vector2 dir)
    {
        if (dir.Equals(Vector2.zero)) return;


        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.y));
        Vector3 r = Quaternion.Lerp(_view.transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime).eulerAngles;
        _view.transform.rotation = Quaternion.Euler(0f, r.y, 0f);

        //_view.transform.rotation =Quaternion.Euler(0f,Quaternion.LookRotation( _view.transform.position +  new Vector3(dir.normalized.x, 0f, dir.normalized.y)).eulerAngles.y,0f);
    }
}
