using System;
using System.Collections;
using System.Collections.Generic;
using Data.Enum.Player;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 currentPosition;
    private float moveDistance = 1.25f; // 移動距離
    private float moveDuration = 0.1f; // 移動時間
    private float cubeScale;
    private Sequence moveSequence;

    private void Start()
    {
        playerTransform = transform;
        currentPosition = playerTransform.position;
        cubeScale = playerTransform.localScale.x;
    }

    private void Update()
    {
        if (!Input.anyKeyDown) return;

        Move(GetPlayerMoveDirection());
    }

    /// <summary>
    /// プレイヤーの入力を返す
    /// </summary>
    private PlayerMoveType GetPlayerMoveDirection()
    {
        switch (Input.inputString)
        {
            case "w":
                return PlayerMoveType.FRONT;
            case "s":
                return PlayerMoveType.BACK;
            case "a":
                return PlayerMoveType.LEFT;
            case "d":
                return PlayerMoveType.RIGHT;
            default:
                return PlayerMoveType.IDLE;
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move(PlayerMoveType playerMoveType)
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
            case PlayerMoveType.FRONT:
                nextDirection.z = moveDistance;
                rotateCenterPoint.x = distanceToCenter;
                rotateForward.x = 1;
                break;
            case PlayerMoveType.BACK:
                nextDirection.z = -moveDistance;
                rotateCenterPoint.x = -distanceToCenter;
                rotateForward.x = -1;
                break;
            case PlayerMoveType.LEFT:
                nextDirection.x = -moveDistance;
                rotateCenterPoint.z = distanceToCenter;
                rotateForward.z = 1;
                break;
            case PlayerMoveType.RIGHT:
                nextDirection.x = moveDistance;
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