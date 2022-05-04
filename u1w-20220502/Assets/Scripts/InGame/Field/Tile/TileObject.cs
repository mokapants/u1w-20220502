using System;
using Data.Enum.Field;
using Data.ValueObjects.Field;
using InGame.Player;
using Repositories.Core;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace InGame.Field
{
    public class TileObject : MonoBehaviour
    {
        protected GameRepository gameRepository;
        protected PlayerManager playerManager;
        [SerializeField] private bool isWalkable;
        [SerializeField] private bool isNeedKey;
        [SerializeField] private bool isGettableKey;
        protected Tile tile;

        // プロパティ
        public Tile Tile => tile;

        [Inject]
        public void Constructor(
            GameRepository gameRepository,
            PlayerManager playerManager
        )
        {
            this.gameRepository = gameRepository;
            this.playerManager = playerManager;
        }

        private void Start()
        {
            // プレイヤーの移動を監視
            playerManager.PositionProperty.Subscribe(OnMovedPlayer).AddTo(this);
        }

        /// <summary>
        /// データを初期化
        /// </summary>
        public void Init(int x, int z, TileType tileType)
        {
            tile = new Tile(x, z, tileType);
            tile.isWalkable = isWalkable;
            tile.isNeedKey = isNeedKey;
            tile.isGettableKey = isGettableKey;
        }

        /// <summary>
        /// プレイヤーが動いた時
        /// </summary>
        private void OnMovedPlayer((int x, int z) position)
        {
            if (position.x != tile.X || position.z != tile.Z) return;

            // プレイヤーがこのタイル上にいる
            OnPlayerStepOnTheTile();
        }

        /// <summary>
        /// プレイヤーがこのタイルを踏んでいる時
        /// </summary>
        protected virtual void OnPlayerStepOnTheTile()
        {
            
        }
    }
}