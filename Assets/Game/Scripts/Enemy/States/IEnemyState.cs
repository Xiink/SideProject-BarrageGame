namespace Game.Scripts.Enemy.States
{
    public interface IEnemyState
    {
        void EnterState();

        void ExitState();

        void Update();

        void FixedUpdate();
    }
}