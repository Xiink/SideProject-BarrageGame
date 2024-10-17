using UnityEngine;
using Zenject;

namespace Game.Scripts.TestFactory
{
    public class TestFactory
    {
        public enum Difficulty
        {
            Easy,
            Normal,
            Hard
        }

        public class SpawnData
        {
            public Vector3 Position;

            public Quaternion Rotation;

            public SpawnData(Vector3 position, Quaternion rotation)
            {
                Position = position;
                Rotation = rotation;
            }
        }

        public interface IObj
        {
            void OnCreated();
        }

        public class AObj : IObj
        {
            public void OnCreated()
            {
                Debug.Log("AObj Created");
            }
        }

        public class BObj : MonoBehaviour,IObj
        {
            [Inject]
            public void Construct()
            {

            }

            public class Factory : PlaceholderFactory<BObj>
            {

            }

            public void OnCreated()
            {
                Debug.Log("BObj Created");
            }
        }

        public class DifficultyManager
        {
            public Difficulty Difficulty
            {
                get;
                set;
            }
        }

        public class ObjFactory : PlaceholderFactory<SpawnData,IObj>
        {
        }

        public class CustomFactory : IFactory<SpawnData,IObj>
        {
            DiContainer _container;
            DifficultyManager _difficultyManager;

            public CustomFactory(DiContainer container, DifficultyManager difficultyManager)
            {
                _container = container;
                _difficultyManager = difficultyManager;
            }

            public IObj Create(SpawnData param)
            {
                if (_difficultyManager.Difficulty == Difficulty.Hard)
                {
                    return _container.Instantiate<BObj>();
                }

                return _container.Instantiate<AObj>();
            }
        }
    }
}