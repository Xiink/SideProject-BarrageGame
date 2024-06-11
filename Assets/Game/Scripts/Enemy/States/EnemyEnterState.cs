using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Game.Scripts.Battle.Misc;
using Game.Scripts.RPG;
using Zenject;

namespace Game.Scripts.Enemy.States
{
    public class EnemyEnterState : IState
    {
        [Inject] private IMover _enemy;

        public float moveSpeed = 5f; // 移動速度
        public float squareSize = 5f; // 正方形邊長

        private Vector3 startPos; // 起始位置
        private Vector3 targetPos; // 目標位置

        private Direction currentDirection = Direction.Right;

        private Queue<string> Actions = new Queue<string>();

        enum Direction
        {
            Right,
            Up,
            Left,
            Down
        };

        private bool finish = true;

        public void OnEnter()
        {
            startPos = _enemy.transform.position;
            targetPos = startPos + Vector3.right * squareSize;

            Actions.Enqueue("Delay");
            Actions.Enqueue("SquareMove");
        }


        string _currentAction = string.Empty;
        public async void OnUpdate()
        {
            // if(finish == false) return;
            //
            // if(Actions.Count > 0)
            //     _currentAction = Actions.Dequeue();
            //
            // switch (_currentAction)
            // {
            //     case "Delay":
            //         finish = false;
            //         finish = await Delay(5000);
            //         break;
            //     case "SquareMove":
            //         finish = !SquareMove();
            //         if (finish == false)
            //         {
            //             finish = true;
            //             Actions.Enqueue("Delay");
            //             Actions.Enqueue("SquareMove");
            //         }
            //         break;
            // }
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }

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

        async Task<bool> Delay(int time)
        {
            await Task.Delay(time);
            return true;
        }
    }
}