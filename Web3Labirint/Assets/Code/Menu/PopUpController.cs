using UnityEngine;

namespace Code.Menu
{
    public class PopUpController
    {
        private readonly PopUpView _view;
        private string _url;
        
        public PopUpController(PopUpView view, string url)
        {
            _url = url;
            _view = view;
            _view.Init();
            _view.OnConfirmButtonClick += Confirm;
        }
    
        public void CleanUp()
        {
            _view.CleanUp();
            _view.OnConfirmButtonClick -= Confirm;
        }
    
        public void Confirm()
        {
            Application.OpenURL(_url);
            CleanUp();
            GameObject.Destroy(_view.gameObject);
        }
    }
}