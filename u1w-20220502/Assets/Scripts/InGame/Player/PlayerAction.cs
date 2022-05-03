using Data.Enum.Player;
using DG.Tweening;
using Repositories.Field;
using UnityEngine;

namespace InGame.Player
{
    /// <summary>
    /// プレイヤーの見た目周りの更新
    /// </summary>
    public class PlayerAction : MonoBehaviour
    {
        [SerializeField] private FieldRepository fieldRepository;
        private Transform playerTransform;
        private Vector3 currentPosition;
        private float moveDuration = 0.1f; // 移動時間
        private float cubeScale;
        private Sequence moveSequence;

        private void Awake()
        {
            playerTransform = transform;
            cubeScale = playerTransform.localScale.x;
        }

        public void InitPosition(int x, int z)
        {
            // 初期位置設定
            var tileHalfDistance = fieldRepository.TileDistance / 2f;
            var offset = new Vector3(tileHalfDistance, 0f, tileHalfDistance);
            currentPosition = offset + new Vector3(x * fieldRepository.TileDistance, 0f, z * fieldRepository.TileDistance);
            playerTransform.position = currentPosition;
        }

        /// <summary>
        /// 移動
        /// </summary>
        public void Move(PlayerMoveType playerMoveType)
        {
            moveSequence.Complete();

            // 移動と回転に必要な値を計算
            // 移動
            var nextDirection = Vector3.zero;
            // 回転
            var rotateCenterPoint = currentPosition;
            rotateCenterPoint.y = 0f;
            var distanceToCenter = cubeScale / 2f;
            var rotateForward = Vector3.zero;
            // 計算
            switch (playerMoveType)
            {
                case PlayerMoveType.Front:
                    nextDirection.z = fieldRepository.TileDistance;
                    rotateCenterPoint.x = distanceToCenter;
                    rotateForward.x = 1;
                    break;
                case PlayerMoveType.Back:
                    nextDirection.z = -fieldRepository.TileDistance;
                    rotateCenterPoint.x = -distanceToCenter;
                    rotateForward.x = -1;
                    break;
                case PlayerMoveType.Left:
                    nextDirection.x = -fieldRepository.TileDistance;
                    rotateCenterPoint.z = distanceToCenter;
                    rotateForward.z = 1;
                    break;
                case PlayerMoveType.Right:
                    nextDirection.x = fieldRepository.TileDistance;
                    rotateCenterPoint.z = -distanceToCenter;
                    rotateForward.z = -1;
                    break;
                default:
                    break;
            }

            currentPosition += nextDirection;

            var prevValue = 0f;
            moveSequence = DOTween.Sequence()
                .Append(
                    playerTransform.DOMove(currentPosition, moveDuration)
                )
                .Join(
                    DOTween.To(
                        () => 0f,
                        value =>
                        {
                            playerTransform.RotateAround(rotateCenterPoint, rotateForward, (value - prevValue) * 90f);
                            prevValue = value;
                        },
                        1f,
                        moveDuration
                    )
                )
                .OnComplete(() => { playerTransform.position = currentPosition; });
        }
    }
}