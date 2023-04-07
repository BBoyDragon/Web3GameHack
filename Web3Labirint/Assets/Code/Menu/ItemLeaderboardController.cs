namespace Code.Menu
{
    public class ItemLeaderboardController
    {
        private readonly ItemLeaderboardView _view;

        public ItemLeaderboardController(ItemLeaderboardView view, string place, string score, string username)
        {
            _view = view;
            _view.Place = place;
            _view.Score = score;
            _view.Username = username;
        }
    }
}