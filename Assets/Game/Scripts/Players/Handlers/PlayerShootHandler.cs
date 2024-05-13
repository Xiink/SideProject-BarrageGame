using Game.Scripts.Battle.Misc;
using Game.Scripts.Bullet;
using Game.Scripts.Players.Main;
using Zenject;
using UnityEngine;

namespace Game.Scripts.Players.Handlers
{
    public class PlayerShootHandler : ITickable
    {
        private readonly Bullet.Bullet.Factory _factory;

        [Inject] private IMover _player;
        [Inject] private PlayerInputState _playerInputState;

        public PlayerShootHandler(Bullet.Bullet.Factory factory)
        {
            _factory = factory;
        }

        public void Tick()
        {
            if(_playerInputState._shootMouseDown == false) return;
            Fire();
        }

        private void Fire()
        {
            var bullet = _factory.Create(BulletTypes.FromPlayer);

            bullet.transform.position = (Vector3)_player.GetPosition() - _player.transform.right;
            bullet.transform.rotation = _player.rigidbody.rotation;
        }
    }
}