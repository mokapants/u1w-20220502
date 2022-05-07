using System;
using Cysharp.Threading.Tasks;
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
            playerController.OnInputMovingKeyObservable.Subscribe(OnInputMovingKey).AddTo(this);

            // プレイヤーの初期位置を設定
            InitPosition();

            Debug.Log($"CurrentPosition: {Position.x}, {Position.z}");
        }

        /// <summary>
        /// プレイヤーの初期位置を設定
        /// </summary>
        private void InitPosition()
        {
            positionProperty.Value = fieldRepository.PlayerStartPoint;

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

            // 通行不可能なタイルの場合は移動不可
            if (!fieldManager.IsMovableTile(nextPlayerPosX, nextPlayerPosZ))
            {
                return;
            }

            // タイルが存在する場合
            positionProperty.Value = (nextPlayerPosX, nextPlayerPosZ);

            playerAction.Move(playerMoveType);

            Debug.Log($"CurrentPosition: {Position.x}, {Position.z}");
        }
    }
}