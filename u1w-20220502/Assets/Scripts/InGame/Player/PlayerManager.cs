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
        [SerializeField] private Transform goldPanelTransform;
        private ReactiveProperty<(int x, int z)> positionProperty;
        private ReactiveProperty<int> keyNumberProperty;
        private int goldPanelFace = 1; // 上1 奥から時計回りに2,3,4,5 底6

        // イベント
        public IReadOnlyReactiveProperty<(int x, int z)> PositionProperty => positionProperty;

        // プロパティ
        public (int x, int z) Position => positionProperty.Value;
        public int KeyNumber => keyNumberProperty.Value;
        public bool IsBottomGoldPanel => goldPanelFace == 6;

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
            keyNumberProperty = new ReactiveProperty<int>(0);

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
        /// 鍵を入手
        /// </summary>
        public void OnGotKey()
        {
            keyNumberProperty.Value++;
        }

        /// <summary>
        /// 鍵を使用
        /// </summary>
        public void OnUsedKey()
        {
            keyNumberProperty.Value--;
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
                // 鍵が必要なタイルかつ、鍵を所持している場合以外は移動不可
                if (!(fieldManager.IsNeedKeyTile(nextPlayerPosX, nextPlayerPosZ) && 0 < KeyNumber))
                {
                    return;
                }
            }

            // タイルが存在する場合
            UpdateGoldPanelFaceDirection(playerMoveType);

            positionProperty.Value = (nextPlayerPosX, nextPlayerPosZ);

            playerAction.Move(playerMoveType);

            Debug.Log($"CurrentPosition: {Position.x}, {Position.z}");
        }

        /// <summary>
        /// ゴールドパネルの向きを計算
        /// </summary>
        private void UpdateGoldPanelFaceDirection(PlayerMoveType playerMoveType)
        {
            switch (playerMoveType)
            {
                case PlayerMoveType.Front:
                    switch (goldPanelFace)
                    {
                        case 1:
                            goldPanelFace = 2;
                            return;
                        case 2:
                            goldPanelFace = 6;
                            return;
                        case 3:
                            return;
                        case 4:
                            goldPanelFace = 1;
                            return;
                        case 5:
                            return;
                        case 6:
                            goldPanelFace = 4;
                            return;
                        default:
                            return;
                    }
                case PlayerMoveType.Back:
                    switch (goldPanelFace)
                    {
                        case 1:
                            goldPanelFace = 4;
                            return;
                        case 2:
                            goldPanelFace = 1;
                            return;
                        case 3:
                            return;
                        case 4:
                            goldPanelFace = 6;
                            return;
                        case 5:
                            return;
                        case 6:
                            goldPanelFace = 2;
                            return;
                        default:
                            return;
                    }
                case PlayerMoveType.Left:
                    switch (goldPanelFace)
                    {
                        case 1:
                            goldPanelFace = 5;
                            return;
                        case 2:
                            return;
                        case 3:
                            goldPanelFace = 1;
                            return;
                        case 4:
                            return;
                        case 5:
                            goldPanelFace = 6;
                            return;
                        case 6:
                            goldPanelFace = 3;
                            return;
                        default:
                            return;
                    }
                case PlayerMoveType.Right:
                    switch (goldPanelFace)
                    {
                        case 1:
                            goldPanelFace = 3;
                            return;
                        case 2:
                            return;
                        case 3:
                            goldPanelFace = 6;
                            return;
                        case 4:
                            return;
                        case 5:
                            goldPanelFace = 1;
                            return;
                        case 6:
                            goldPanelFace = 5;
                            return;
                        default:
                            return;
                    }
            }
        }
    }
}