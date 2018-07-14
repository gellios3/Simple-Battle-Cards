namespace Services
{
    public class GameStateService
    {
        public GameType GameType { get; private set; }

        public void InitMultiplayer()
        {
            GameType = GameType.Multiplayer;
        }

        public void InitSingleGame()
        {
            GameType = GameType.Single;
        }
    }

    public enum GameType
    {
        Single,
        Multiplayer
    }
}