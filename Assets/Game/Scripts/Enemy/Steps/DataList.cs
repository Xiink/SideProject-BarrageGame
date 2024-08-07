using System.Collections;
using System.Threading.Tasks;
using Game.Scripts.Battle.Misc;
using Game.Scripts.Enemy.Values;

namespace Game.Scripts.Enemy.Steps
{
    public interface DataList
    {
        Task<bool> onExecute();

        public void InjectDependencies(IFlowControl control);

        public void InitAction();
    }
}