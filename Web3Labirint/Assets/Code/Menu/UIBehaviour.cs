using UnityEngine;
using UnityEngine.UI;
using System;

namespace Code.Menu
{
    public class UIBehaviour : MonoBehaviour
    {

        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private Button _startButton;

        public Animator Animator { get => _animator;}

        public event Action OnStartButtonClick;
        public event Action OnGameStarted;

        public void Init()
        {
            _startButton.onClick.AddListener(OnStart);
        }
        public void CleanUp()
        {
            _startButton.onClick.RemoveAllListeners();
        }
        private void OnStart()
        {
            OnStartButtonClick?.Invoke();
        }

        public void StartGame()
        {
            OnGameStarted?.Invoke();
        }
    }
}
