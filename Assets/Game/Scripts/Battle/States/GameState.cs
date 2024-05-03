namespace Game.Scripts.Battle.States
{
    public class GameState
    {
        public bool _pause { get; private set; }

        public void SetPauseState(bool state)
        {
            if(state == _pause) return;
            _pause = state;
        }
    }
}