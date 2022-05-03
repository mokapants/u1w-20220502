using System;
using System.Collections;
using System.Collections.Generic;
using Data.Enum.Player;
using DG.Tweening;
using Repositories.Field;
using UniRx;
using UnityEngine;

/// <summary>
/// プレイヤーの入力を検知
/// </summary>
public class PlayerController : MonoBehaviour
{
    private Subject<PlayerMoveType> onInputMovingKeySubject;

    // イベント
    public IObservable<PlayerMoveType> OnInputMovingKeyObservable => onInputMovingKeySubject;

    private void Awake()
    {
        onInputMovingKeySubject = new Subject<PlayerMoveType>();
    }

    private void Update()
    {
        if (!Input.anyKeyDown) return;

        onInputMovingKeySubject.OnNext(GetPlayerMoveDirection());
    }

    /// <summary>
    /// プレイヤーの入力を返す
    /// </summary>
    private PlayerMoveType GetPlayerMoveDirection()
    {
        switch (Input.inputString)
        {
            case "w":
                return PlayerMoveType.Front;
            case "s":
                return PlayerMoveType.Back;
            case "a":
                return PlayerMoveType.Left;
            case "d":
                return PlayerMoveType.Right;
            default:
                return PlayerMoveType.Idle;
        }
    }
}