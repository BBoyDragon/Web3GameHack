using TMPro;
using UnityEngine;

namespace Code.Menu
{
    public class ItemLeaderboardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI username;
        [SerializeField] private TextMeshProUGUI score;
        [SerializeField] private TextMeshProUGUI place;
        
        public string Place
        {
            get => place.text;
            set => place.text = value;
        }
        public string Score
        {
            get => score.text;
            set => score.text = value;
        }
        public string Username
        {
            get => username.text;
            set => username.text = value;
        }
    }
}