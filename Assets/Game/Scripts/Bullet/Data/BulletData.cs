using UnityEngine;
using Zenject;

namespace Game.Scripts.Bullet.Data
{
    [CreateAssetMenu(fileName = "BulletDataContainer",menuName = "BulletDataContainer",order = 1)]
    public class BulletData : ScriptableObjectInstaller
    {
        [SerializeField] private Bullet.Data _data;

        public override void InstallBindings()
        {
            Container.BindInstance(_data);
        }
    }
}