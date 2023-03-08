using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorldController : IExecute, IAwake, IInitialization ,IFixedExecute,ILateExecute, ICleanup
{
    public void Awake()
    {
        Debug.Log("Awake action");
    }

    public void Init()
    {
        Debug.Log("Start action");
    }

    public void Execute()
    {
        Debug.Log("Every frame action");
    }

    public void FixedExecute()
    {
        Debug.Log("Fixed time action");
    }

    public void LateExecute()
    {
        Debug.Log("After frame action");
    }

    public void Cleanup()
    {
        Debug.Log("Final action");
    }
}
