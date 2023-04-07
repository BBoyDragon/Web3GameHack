using UnityEngine;
using UnityEngine.UI;
using System;

namespace Code.Menu
{
    public class UIBehaviour : MonoBehaviour
    {

        [SerializeField] private Animator animator;
        [SerializeField] private Button startButton;
        [SerializeField] private Button shopButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button leaderboardButton;
        [SerializeField] private GameObject shopMenu;
        [SerializeField] private GameObject shopItemsContainer;
        [SerializeField] private GameObject leaderboardMenu;

        public Animator Animator => animator;
        public GameObject ShopMenu => shopMenu;
        public GameObject ShopItemsContainer => shopItemsContainer;
        public GameObject LeaderboardMenu => leaderboardMenu;

        public event Action OnStartButtonClick;
        public event Action OnShopButtonClick;
        public event Action OnLeaderboardButtonClick;
        public event Action OnExitButtonClick;
        public event Action OnGameStarted;

        public void Init()
        {
            startButton.onClick.AddListener(OnStart);
            shopButton.onClick.AddListener(OnShopOpen);
            leaderboardButton.onClick.AddListener(OnLeaderboardOpen);
            exitButton.onClick.AddListener(OnExit);
        }
        public void CleanUp()
        {
            startButton.onClick.RemoveAllListeners();
            shopButton.onClick.RemoveAllListeners();
            leaderboardButton.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveAllListeners();
        }
        private void OnStart()
        {
            OnStartButtonClick?.Invoke();
        }
        private void OnShopOpen()
        {
            OnShopButtonClick?.Invoke();
        }
        private void OnExit()
        {
            OnExitButtonClick?.Invoke();
        }
        private void OnLeaderboardOpen()
        {
            OnLeaderboardButtonClick?.Invoke();
        }
        public void StartGame()
        {
            OnGameStarted?.Invoke();
        }
    }
}
