using System;
using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class Enemy : MonoBehaviour
    {
        public void Die()
        {
            Destroy(gameObject);
        }
    }
}