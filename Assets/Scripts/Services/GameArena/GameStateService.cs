namespace Services.GameArena
{
    public class GameStateService
    {
        public GameType GameType { get; private set; }

        public string PlayerName { get; private set; }

        public void InitPlayerName(string name)
        {
            PlayerName = name;
        }

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