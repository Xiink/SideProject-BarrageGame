using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Game.Scripts.Battle.Misc;
using Game.Scripts.Enemy.Values;
using Game.Scripts.Values;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Enemy.Steps
{
    [Serializable]
    public class EnemyStepData : DataList
    {
        public override string ToString() => "步驟資料";

        private bool statTick = false;
        private bool result = false;

        [LabelText("Step Data id List")] [ValueDropdown("@GetStep.steps"), HideReferenceObjectPicker]
        public List<EnemyStep> StepDataList;

        [Inject] private EnemyFlowControl _enemyFlowControl;

        private float moveSpeed = 5f; // 移動速度
        private float squareSize = 5f; // 正方形邊長
        private Direction currentDirection = Direction.Right;

        private Vector3 startPos; // 起始位置
        private Vector3 targetPos; // 目標位置

        private SynchronizationContext _context;

        private enum Direction
        {
            Right,
            Up,
            Left,
            Down
        };

        public async Task onExecute()
        {
            Debug.Log("測試");
            Debug.Log(_enemyFlowControl._enemy);

            statTick = true;
            _context = SynchronizationContext.Current;

            await Task.Delay(8000);
            Debug.Log("延遲了八秒");

            // var task = Task.Run(async () =>
            // {
            //     while (true)
            //     {
            //         // _context.Post(_ =>
            //         // {
            //         //     // result = SquareMove();
            //         //     Debug.Log("Test");
            //         // }, null);
            //         result = true;
            //         await Task.Delay(8000);
            //         Debug.Log("延遲了八秒");
            //
            //         if (result == true)
            //         {
            //             Debug.Log("End");
            //             break;
            //         }
            //     }
            // });
            // task.Wait();
        }

        public bool SquareMove()
        {
            // 根據當前方向和速度進行移動
            float step = moveSpeed * Time.deltaTime;
            bool result = false;

            try
            {
                switch (currentDirection)
                {
                    case Direction.Right:
                        _enemyFlowControl._enemy.transform.position =
                            Vector3.MoveTowards(_enemyFlowControl._enemy.transform.position, targetPos, step);
                        Debug.Log(_enemyFlowControl._enemy.transform.position);
                        if (Vector3.Distance(_enemyFlowControl._enemy.transform.position, targetPos) < 0.01f)
                        {
                            currentDirection = Direction.Up;
                            targetPos += Vector3.up * squareSize;
                        }

                        break;
                    case Direction.Up:
                        _enemyFlowControl._enemy.transform.position =
                            Vector3.MoveTowards(_enemyFlowControl._enemy.transform.position, targetPos, step);
                        if (Vector3.Distance(_enemyFlowControl._enemy.transform.position, targetPos) < 0.01f)
                        {
                            currentDirection = Direction.Left;
                            targetPos += Vector3.left * squareSize;
                        }

                        break;
                    case Direction.Left:
                        _enemyFlowControl._enemy.transform.position =
                            Vector3.MoveTowards(_enemyFlowControl._enemy.transform.position, targetPos, step);
                        if (Vector3.Distance(_enemyFlowControl._enemy.transform.position, targetPos) < 0.01f)
                        {
                            currentDirection = Direction.Down;
                            targetPos += Vector3.down * squareSize;
                        }

                        break;
                    case Direction.Down:
                        _enemyFlowControl._enemy.transform.position =
                            Vector3.MoveTowards(_enemyFlowControl._enemy.transform.position, targetPos, step);
                        if (Vector3.Distance(_enemyFlowControl._enemy.transform.position, targetPos) < 0.01f)
                        {
                            currentDirection = Direction.Right;
                            targetPos += Vector3.right * squareSize;
                            result = true;
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }


            return result;
        }

        public void InjectDependencies(IFlowControl control)
        {
            _enemyFlowControl = (EnemyFlowControl)control;
        }
    }
}