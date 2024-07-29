using Game.Scripts.Enemy.Phases;
using Zenject;

namespace Game.Scripts.Enemy.Main
{
    public class EnemyStateManager : ITickable,IInitializable
    {
        [Inject]
        public EnemyFlowControl _EnemyFlowControl;

        private EnemyPhase _currentPhase;

        public void Tick()
        {
        }

        public void ChangeState(EnemyStates states)
        {

        }

        public void Initialize()
        {

        }
    }
}