using Assets.Scripts.StateMachine;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player
{
    public class PlayerUnit : MonoBehaviour
    {
        #region events



        #endregion

        [Inject]
        private StateMachine<GameState> gameStateMachine;

        private void Awake()
        {

        }

        private void Start()
        {
            
        }
    }
}
