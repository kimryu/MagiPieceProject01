#region File Description
//********************************************************************
//    file name		: RoomClickEvent.cs
//    infomation	: Click時に発生するイベント
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
using System.Collections;
using System;
#endregion

/// <summary>
/// Click時に発生するイベント
/// Layerは"RoomEvent"
/// </summary>
public class RoomClickEvent : MonoBehaviour
{
    public string SrioFileName;
    public EventType Type;//無理ならint型にしてEventTypeにキャストする
    public int SubPosOffsetX;
    public int SubPosOffsetY;

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

    internal Vector2I GetSubPos()
    {
        return new Vector2I(_Postion.IntX + SubPosOffsetX, _Postion.IntY + SubPosOffsetY);
    }
}

