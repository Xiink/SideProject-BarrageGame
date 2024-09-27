using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Battle.Handlers;
using Game.Scripts.Battle.Misc;
using Game.Scripts.Battle.States;
using Game.Scripts.Bullet;
using Game.Scripts.Enemy;
using UnityEngine;
using Zenject;

public class TestCreat : MonoBehaviour
{
    public GameObject myPrefab;

    // [Inject]
    // private Enemy.Factory _factory;

    // Start is called before the first frame update
    void Start()
    {
        // if (_factory == null)
        // {
        //     Debug.LogError("Enemy.Factory not injected!");
        // }
        // else
        // {
        //     var enemy = _factory.Create();
        //     Debug.Log("Enemy created successfully.");
        // }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
