using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Menu
{
    public class PopUpView : MonoBehaviour
    {
        [SerializeField] private Button confirmButton;
        [SerializeField] private TMP_Text text;

        public string Text { get => text.text; set => text.text = value; }
        public string ButtonText { get => confirmButton.GetComponentInChildren<TMP_Text>().text; set => confirmButton.GetComponentInChildren<TMP_Text>().text = value; }

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
