
using UnityEngine;

public class GameController : MonoBehaviour
{
    private ControllerManager _behaviourController;
    private GameInit _gameInit;

    private void Awake()
    {
        _behaviourController = new ControllerManager();
        _gameInit = new GameInit(_behaviourController, this);

        _behaviourController.Awake();

    }

    private void Start()
    {
        _behaviourController.Init();
    }

    private void Update()
    {
        _behaviourController.Execute();
    }

    private void FixedUpdate()
    {
        _behaviourController.FixedExecute();
    }

    private void LateUpdate()
    {
        _behaviourController.LateExecute();
    }

    private void OnDestroy()
    {
        _gameInit.Dispose();
        _behaviourController.Cleanup();
    }
    public void OnDestroyBetwenLevels()
    {
        _behaviourController.Cleanup();
    }
}