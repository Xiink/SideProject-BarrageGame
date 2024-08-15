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
        #region Public Variables

        [LabelText("Step Data id List")] [ValueDropdown("@GetStep.steps"), HideReferenceObjectPicker]
        public List<EnemyStep> StepDataList;

        #endregion

        #region Private Variables

        private enum Direction
        {
            Right,
            Up,
            Left,
            Down
        };

        private bool statTick = false;

        [Inject] private EnemyFlowControl _enemyFlowControl;

        private float moveSpeed = 5f; // 移動速度
        private float squareSize = 5f; // 正方形邊長
        private Direction currentDirection = Direction.Right;

        private Vector3 startPos; // 起始位置
        private Vector3 targetPos; // 目標位置

        private bool _isFinished = true;

        private SynchronizationContext _context;

        #endregion

        #region Public Methods

        public void InitAction()
        {
            currentDirection = Direction.Right;
            _isFinished = false;
        }

        public void InjectDependencies(IFlowControl control)
        {
            _enemyFlowControl = (EnemyFlowControl)control;
        }

        public async Task<bool> onExecute()
        {
            var result = SquareMove();

            return result;
        }

        public bool SquareMove()
        {
            // 根據當前方向和速度進行移動
            float step = moveSpeed * Time.deltaTime;
            try
            {
                switch (currentDirection)
                {
                    case Direction.Right:
                        _enemyFlowControl._enemy.transform.position =
                            Vector3.MoveTowards(_enemyFlowControl._enemy.transform.position, targetPos, step);
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
                            _isFinished = true;
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }

            return _isFinished;
        }

        public override string ToString() => "步驟資料";

        #endregion
    }
}