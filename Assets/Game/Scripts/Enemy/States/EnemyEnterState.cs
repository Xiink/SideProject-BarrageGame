using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Game.Scripts.Battle.Misc;
using Game.Scripts.RPG;
using Zenject;

namespace Game.Scripts.Enemy.States
{
    public class EnemyEnterState : IEnemyState
    {
        #region Public Variables

        public float moveSpeed = 5f; // 移動速度
        public float squareSize = 5f; // 正方形邊長

        #endregion

        #region Private Variables

        enum Direction
        {
            Right,
            Up,
            Left,
            Down
        };

        [Inject] private IMover _enemy;

        private Vector3 startPos; // 起始位置
        private Vector3 targetPos; // 目標位置

        private Direction currentDirection = Direction.Right;

        private Queue<string> Actions = new Queue<string>();

        private bool finish = true;

        // public void OnEnter()
        // {
        //     Debug.Log("Enter EnemyEnterState");
        //     // startPos = _enemy.transform.position;
        //     // targetPos = startPos + Vector3.right * squareSize;
        //     //
        //     // Actions.Enqueue("Delay");
        //     // Actions.Enqueue("SquareMove");
        // }


        string _currentAction = string.Empty;

        #endregion

        #region Unity events

        public void Update()
        {
        }

        #endregion

        #region Public Methods

        public void EnterState()
        {
            Debug.Log("Enter EnemyEnterState");
        }

        public void ExitState()
        {
        }

        public void FixedUpdate()
        {
            SquareMove();
        }
        // public async void OnUpdate()
        // {
        //     SquareMove();
        // }
        //
        // public void OnExit()
        // {
        //     // throw new System.NotImplementedException();
        // }

        public bool SquareMove()
        {
            // 根據當前方向和速度進行移動
            float step = moveSpeed * Time.deltaTime;
            bool result = false;

            switch (currentDirection)
            {
                case Direction.Right:
                    _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, targetPos, step);
                    if (Vector3.Distance(_enemy.transform.position, targetPos) < 0.01f)
                    {
                        currentDirection = Direction.Up;
                        targetPos += Vector3.up * squareSize;
                    }

                    break;
                case Direction.Up:
                    _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, targetPos, step);
                    if (Vector3.Distance(_enemy.transform.position, targetPos) < 0.01f)
                    {
                        currentDirection = Direction.Left;
                        targetPos += Vector3.left * squareSize;
                    }

                    break;
                case Direction.Left:
                    _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, targetPos, step);
                    if (Vector3.Distance(_enemy.transform.position, targetPos) < 0.01f)
                    {
                        currentDirection = Direction.Down;
                        targetPos += Vector3.down * squareSize;
                    }

                    break;
                case Direction.Down:
                    _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, targetPos, step);
                    if (Vector3.Distance(_enemy.transform.position, targetPos) < 0.01f)
                    {
                        currentDirection = Direction.Right;
                        targetPos += Vector3.right * squareSize;
                        result = true;
                    }

                    break;
            }

            return result;
        }

        #endregion

        #region Private Methods

        async Task<bool> Delay(int time)
        {
            await Task.Delay(time);
            return true;
        }

        #endregion
    }
}