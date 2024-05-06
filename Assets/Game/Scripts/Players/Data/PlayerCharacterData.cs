using System;
using Game.Scripts.Bullet;
using Game.Scripts.Players.Main;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Players.Data
{
    [CreateAssetMenu(fileName = "PlayerCharcterDataContainer", menuName = "PlayerCharacterDataContainer",order = 0)]
    public class PlayerCharacterData : ScriptableObjectInstaller
    {
        [SerializeField]
        private PlayerCharacter.Data _data;

        public override void InstallBindings()
        {
            Container.BindInstance(_data);
        }
    }
}