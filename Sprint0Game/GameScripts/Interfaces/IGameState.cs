namespace Sprint0Game.GameScripts.Interfaces
{
    public interface IGameState
    {
        void PauseGame();
        void ResumeGame();
        void ToMainMenu();
        void ToItemMenu();
        void ToGameOver();
    }
}
