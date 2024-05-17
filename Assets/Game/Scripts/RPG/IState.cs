namespace Game.Scripts.RPG
{
    public interface IState
    {
        void OnEnter();
        void OnUpdate();
        void OnExit();
    }
}