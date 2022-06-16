using Assets.Scripts.LevelGeneration;
using Assets.Scripts.Player;
using Assets.Scripts.StateMachine;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Camera
{
    public class CameraFollower : MonoBehaviour
    {
        #region settings

        private const float _zOffsetMpy = -60f;

        #endregion

        [Inject]
        private MeshGenerationSettings meshGenerationSettings;

        [Inject]
        private StateMachine<GameState> gameStateMachine;

        [Inject]
        private PlayerUnit player;

        private void Awake()
        {
            gameStateMachine.Subscribe(GameState.LevelConstructed, GameState.ObjectStartMoving, OnObjectStartMoving);
        }

        private void Start()
        {
            transform.position = CalculateStartingPosition();
        }

        private Vector3 CalculateStartingPosition()
        {
            var centerOfMesh = meshGenerationSettings.MiddleCenter();
            var z = -meshGenerationSettings.Size + centerOfMesh.z;
            return new Vector3(centerOfMesh.x, centerOfMesh.y, z);
        }

        private void OnObjectStartMoving()
        {
            var playerPos = player.PlayerObj.transform.position;
            transform.position = new Vector3(playerPos.x, playerPos.y, playerPos.z + _zOffsetMpy);
            transform.parent = player.PlayerObj.transform;
        }
    }
}
