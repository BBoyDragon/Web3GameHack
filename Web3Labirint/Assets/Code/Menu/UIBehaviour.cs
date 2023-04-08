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
        [SerializeField] private Button exitShopButton;
        [SerializeField] private Button exitLeaderboardButton;
        [SerializeField] private Button leaderboardButton;
        [SerializeField] private GameObject shopMenu;
        [SerializeField] private GameObject shopItemsContainer;
        [SerializeField] private GameObject enableItemsContainer;
        [SerializeField] private GameObject leaderboardItemsContainer;
        [SerializeField] private GameObject leaderboardMenu;
        [SerializeField] private Slider shopSwitch;
        [SerializeField] private GameObject shopItemsList;
        [SerializeField] private GameObject enableItemsList;

        public Animator Animator => animator;
        public GameObject ShopMenu => shopMenu;
        public GameObject ShopItemsContainer => shopItemsContainer;
        public GameObject EnableItemsContainer => enableItemsContainer;
        public GameObject LeaderboardItemsContainer => leaderboardItemsContainer;
        public GameObject LeaderboardMenu => leaderboardMenu;
        public bool ShopSwitch => Convert.ToBoolean(shopSwitch.value);
        public GameObject ShopItemsList => shopItemsList;
        public GameObject EnableItemsList => enableItemsList;

        public event Action OnStartButtonClick;
        public event Action OnOpenShopButtonClick;
        public event Action OnOpenLeaderboardButtonClick;
        public event Action OnExitShopButtonClick;
        public event Action OnExitLeaderboardButtonClick;
        public event Action OnGameStarted;
        public event Action OnSwitchToggle;

        public void Init()
        {
            startButton.onClick.AddListener(OnStart);
            shopButton.onClick.AddListener(OnShopOpen);
            leaderboardButton.onClick.AddListener(OnLeaderboardOpen);
            exitShopButton.onClick.AddListener(OnShopExit);
            exitLeaderboardButton.onClick.AddListener(OnLeaderboardExit);
            shopSwitch.onValueChanged.AddListener(OnSwitch);
        }
        public void CleanUp()
        {
            startButton.onClick.RemoveAllListeners();
            shopButton.onClick.RemoveAllListeners();
            leaderboardButton.onClick.RemoveAllListeners();
            exitShopButton.onClick.RemoveAllListeners();
            exitLeaderboardButton.onClick.RemoveAllListeners();
            shopSwitch.onValueChanged.RemoveAllListeners();
        }
        private void OnStart()
        {
            OnStartButtonClick?.Invoke();
        }
        private void OnShopOpen()
        {
            OnOpenShopButtonClick?.Invoke();
        }
        private void OnShopExit()
        {
            OnExitShopButtonClick?.Invoke();
        }
        private void OnLeaderboardOpen()
        {
            OnOpenLeaderboardButtonClick?.Invoke();
        }
        private void OnLeaderboardExit()
        {
            OnExitLeaderboardButtonClick?.Invoke();
        }

        private void OnSwitch(float diff)
        {
            OnSwitchToggle?.Invoke();
        }
        public void StartGame()
        {
            OnGameStarted?.Invoke();
        }
    }
}
