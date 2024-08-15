using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Battle.Misc;
using Game.Scripts.Players.Main;
using UnityEngine;
using Zenject;

public class PlayerFacade : MonoBehaviour
{
    [Inject]
    private PlayerCharacter _player;
}
