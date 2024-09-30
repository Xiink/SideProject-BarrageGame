using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Battle.Handlers;
using Game.Scripts.Battle.Misc;
using Game.Scripts.Battle.States;
using Game.Scripts.Bullet;
using Game.Scripts.Enemy;
using Game.Scripts.Enemy.Application;
using UnityEngine;
using Zenject;

public class TestCreat : MonoBehaviour
{
    public GameObject myPrefab;

    [Inject] private EnemySpawner _enemySpawner;

    void Start()
    {
        _enemySpawner.CreateNewEnemy();
    }

}
