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
        public MenuController(UiData data)
        {
            _data = data;
            _view = GameObject.Instantiate<UIBehaviour>(_data.MenuView);
            _view.Init();
            _view.OnStartButtonClick += IncreaseSize;
            _view.OnStartButtonClick += IncreaseTransparency;
        }

        public void Cleanup()
        {
            _view.CleanUp();
            _view.OnStartButtonClick -= IncreaseSize;
            _view.OnStartButtonClick -= IncreaseTransparency;
        }

        public void IncreaseSize()
        {
            _view.transform.LeanScale(new Vector3(1.5f, 1.5f, 1.5f), 0.1f).setEaseInOutSine();
        }

        public void IncreaseTransparency()
        {
            _view.Animator.SetTrigger(Transparency);
        }
    }

}
