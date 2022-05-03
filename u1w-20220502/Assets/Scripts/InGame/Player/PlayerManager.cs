using System;
using Data.Enum.Player;
using InGame.Field;
using Repositories.Field;
using UniRx;
using UnityEngine;

namespace InGame.Player
{
    /// <summary>
    /// プレイヤーの制御
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private FieldRepository fieldRepository;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private PlayerAction playerAction;
        [SerializeField] private FieldManager fieldManager;
        private int playerPosX;
        private int playerPosZ;

        private void Start()
        {
            // プレイヤーの移動入力を監視
            playerController.OnInputMovingKeyObservable.Subscribe(OnInputMovingKey);
            
            // プレイヤーの初期位置を設定
            InitPosition();

            Debug.Log($"CurrentPosition: {playerPosX}, {playerPosZ}");
        }

        /// <summary>
        /// プレイヤーの初期位置を設定
        /// </summary>
        private void InitPosition()
        {
            playerPosX = fieldRepository.FieldSize / 2;
            playerPosZ = fieldRepository.FieldSize / 2;
            
            playerAction.InitPosition(playerPosX, playerPosZ);
        }

        /// <summary>
        /// 移動
        /// </summary>
        private void OnInputMovingKey(PlayerMoveType playerMoveType)
        {
            var nextPlayerPosX = playerPosX;
            var nextPlayerPosZ = playerPosZ;
            switch (playerMoveType)
            {
                case PlayerMoveType.Front:
                    nextPlayerPosZ++;
                    break;
                case PlayerMoveType.Back:
                    nextPlayerPosZ--;
                    break;
                case PlayerMoveType.Left:
                    nextPlayerPosX--;
                    break;
                case PlayerMoveType.Right:
                    nextPlayerPosX++;
                    break;
                default:
                    return;
            }

            if (!fieldManager.IsExistsTile(nextPlayerPosX, nextPlayerPosZ)) return;

            playerPosX = nextPlayerPosX;
            playerPosZ = nextPlayerPosZ;

            playerAction.Move(playerMoveType);
            
            Debug.Log($"CurrentPosition: {playerPosX}, {playerPosZ}");
        }
    }
}