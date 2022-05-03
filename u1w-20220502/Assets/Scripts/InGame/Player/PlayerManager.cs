using System;
using Data.Enum.Player;
using InGame.Field;
using Repositories.Field;
using UniRx;
using UnityEngine;
using VContainer;

namespace InGame.Player
{
    /// <summary>
    /// プレイヤーの制御
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {
        private FieldRepository fieldRepository;
        private PlayerController playerController;
        private PlayerAction playerAction;
        private FieldManager fieldManager;
        private ReactiveProperty<(int x, int z)> positionProperty;

        // イベント
        public IReadOnlyReactiveProperty<(int x, int z)> PositionProperty => positionProperty;

        // プロパティ
        public (int x, int z) Position => positionProperty.Value;

        [Inject]
        public void Constructor(
            FieldRepository fieldRepository,
            PlayerController playerController,
            PlayerAction playerAction,
            FieldManager fieldManager
        )
        {
            this.fieldRepository = fieldRepository;
            this.playerController = playerController;
            this.playerAction = playerAction;
            this.fieldManager = fieldManager;
        }

        private void Start()
        {
            positionProperty = new ReactiveProperty<(int x, int z)>();

            // プレイヤーの移動入力を監視
            playerController.OnInputMovingKeyObservable.Subscribe(OnInputMovingKey);

            // プレイヤーの初期位置を設定
            InitPosition();

            Debug.Log($"CurrentPosition: {Position.x}, {Position.z}");
        }

        /// <summary>
        /// プレイヤーの初期位置を設定
        /// </summary>
        private void InitPosition()
        {
            positionProperty.Value = (fieldRepository.FieldSize / 2, fieldRepository.FieldSize / 2);

            playerAction.InitPosition(Position.x, Position.z);
        }

        /// <summary>
        /// 移動
        /// </summary>
        private void OnInputMovingKey(PlayerMoveType playerMoveType)
        {
            var nextPlayerPosX = Position.x;
            var nextPlayerPosZ = Position.z;
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

            positionProperty.Value = (nextPlayerPosX, nextPlayerPosZ);

            playerAction.Move(playerMoveType);

            Debug.Log($"CurrentPosition: {Position.x}, {Position.z}");
        }
    }
}