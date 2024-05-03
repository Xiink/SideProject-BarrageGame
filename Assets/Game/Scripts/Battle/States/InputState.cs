namespace Game.Scripts.Battle.States
{
    public class InputState
    {
        public bool _pauseKeyDown { get; private set; }

        public void SetPauseKeyDown(bool state)
        {
            _pauseKeyDown = state;
        }
    }
}