using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Code.Menu
{
    public class MenuController:ICleanup
    {
        private static readonly int Transparency = Animator.StringToHash("Transparency");
        private UiData _data;
        private UIBehaviour _view;

        public event Action OnStartGame;
        public MenuController(UiData data)
        {
            _data = data;
            _view = GameObject.Instantiate<UIBehaviour>(_data.MenuView);
            _view.Init();
            _view.OnStartButtonClick += IncreaseSize;
            _view.OnStartButtonClick += IncreaseTransparency;
            _view.OnGameStarted += StartGame;
        }

        public void Cleanup()
        {
            _view.CleanUp();
            _view.OnStartButtonClick -= IncreaseSize;
            _view.OnStartButtonClick -= IncreaseTransparency;
            _view.OnGameStarted -= StartGame;
        }

        private void IncreaseSize()
        {
            _view.transform.LeanScale(new Vector3(1.5f, 1.5f, 1.5f), 0.1f).setEaseInOutSine();
        }

        private void IncreaseTransparency()
        {
            _view.Animator.SetTrigger(Transparency);
        }
        private void StartGame()
        {
            OnStartGame?.Invoke();
            TogleSetActive();
        }
        private void TogleSetActive()
        {
            if (_view.enabled)
            {
                _view.gameObject.SetActive(false);
            }
            else
            {
                _view.gameObject.SetActive(true);
            }
        }

    }

}
