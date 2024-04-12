
using FluentAssertions;
using Game.Scripts.Battle.Misc;
using Zenject;

namespace Game.Scripts.Battle.Main
{
    public class BattleInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<TimeProvider>().AsSingle();
        }
    }
}


