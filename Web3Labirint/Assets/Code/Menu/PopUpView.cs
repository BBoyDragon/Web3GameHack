using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Menu
{
    public class PopUpView : MonoBehaviour
    {
        [SerializeField] private Button confirmButton;

        public event Action OnConfirmButtonClick;
        
        public void Init()
        {
            confirmButton.onClick.AddListener(OnConfirm);
        }
        
        public void CleanUp()
        {
            confirmButton.onClick.RemoveAllListeners();
        }

        private void OnConfirm()
        {
            OnConfirmButtonClick?.Invoke();
        }
    }
}
