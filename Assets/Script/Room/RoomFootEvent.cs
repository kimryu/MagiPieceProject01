#region File Description
//********************************************************************
//    file name		: RoomFootEvent.cs
//    infomation	: 上に乗った場合に発生するイベント
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
using System.Collections;
using System;
#endregion

/// <summary>
/// 上に乗った場合に発生するイベント
/// Layerは"RoomEvent"
/// </summary>
public class RoomFootEvent : MonoBehaviour
{
    #region Inspector
    [SerializeField]
    public string SrioFileName;

    [SerializeField]
    public EventType Type;
    #endregion Inspector

    #region Member
    public ComponentAccesser _Access;
    public Postion _Postion;
    #endregion

    #region UNITY_DELEGATE
    void Awake()
    {
        _Access = new ComponentAccesser(gameObject);
        _Postion = new Postion(gameObject);

        _Postion.IntZ = -9;

        // 整数に変更
        _Postion.ToInteger();

        // 非表示にする
        //_Access.Renderer.enabled = false;
    }
    #endregion UNITY_DELEGATE
}

