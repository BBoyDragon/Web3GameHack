using System;
using System.Collections.Generic;
using UnityEngine;
internal sealed class GameInit : IDisposable
{
    HelloWorldController temp;
    public GameInit(ControllerManager behaviourController, GameController mainController)
    {
        temp = new HelloWorldController();
        behaviourController.Add(temp);
    }



    public void Dispose()
    {
    }
}