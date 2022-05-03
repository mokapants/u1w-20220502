using System;
using Data.Enum.Field;
using Data.ValueObjects.Field;
using InGame.Player;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace InGame.Field
{
    public class TileObject : MonoBehaviour
    {
        private FieldManager fieldManager;
        private PlayerManager playerManager;
        [SerializeField] private Text pointText;
        [SerializeField] private GameObject normalObj;
        [SerializeField] private GameObject paintedPlusPointObj;
        [SerializeField] private GameObject paintedMinusPointObj;
        [SerializeField] private GameObject paintedGoldPointObj;
        private int x;
        private int z;

        [Inject]
        public void Constructor(
            FieldManager fieldManager,
            PlayerManager playerManager
        )
        {
            this.fieldManager = fieldManager;
            this.playerManager = playerManager;
        }

        private void Start()
        {
            // プレイヤーの移動を監視
            playerManager.PositionProperty.Subscribe(OnMovedPlayer).AddTo(this);
            // タイル情報の変更を監視
            fieldManager.TileObservable[x, z].Subscribe(OnUpdateTile).AddTo(this);

            // ポイントを設定
            SetPointText(fieldManager.GetTile(x, z).Point);
        }

        /// <summary>
        /// データを初期化
        /// </summary>
        public void Init(int x, int z)
        {
            this.x = x;
            this.z = z;
        }

        /// <summary>
        /// プレイヤーが動いた時
        /// </summary>
        private void OnMovedPlayer((int x, int z) position)
        {
            if (position.x != x || position.z != z) return;

            // プレイヤーがこのタイル上にいる
            var tile = fieldManager.GetTile(x, z);
            tile.TileState = playerManager.IsBottomGoldPanel ? TileState.GoldPainted : TileState.Painted;

            fieldManager.SetTile(x, z, tile);
        }

        /// <summary>
        /// タイルの情報が更新された時
        /// </summary>
        private void OnUpdateTile(Tile tile)
        {
            SetPointText(tile.Point);
            SetTileObject(tile.TileState, tile.Point);
        }

        /// <summary>
        /// ポイントのテキストを設定
        /// </summary>
        private void SetPointText(int point)
        {
            pointText.text = point.ToString();
        }

        /// <summary>
        /// タイルの見た目を更新
        /// </summary>
        private void SetTileObject(TileState tileState, int point)
        {
            switch (tileState)
            {
                case TileState.None:
                    normalObj.SetActive(true);
                    paintedPlusPointObj.SetActive(false);
                    paintedMinusPointObj.SetActive(false);
                    paintedGoldPointObj.SetActive(false);
                    break;
                case TileState.Painted:
                    normalObj.SetActive(false);
                    paintedPlusPointObj.SetActive(0 <= point);
                    paintedMinusPointObj.SetActive(point < 0);
                    paintedGoldPointObj.SetActive(false);
                    break;
                case TileState.GoldPainted:
                    normalObj.SetActive(false);
                    paintedPlusPointObj.SetActive(false);
                    paintedMinusPointObj.SetActive(false);
                    paintedGoldPointObj.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }
}