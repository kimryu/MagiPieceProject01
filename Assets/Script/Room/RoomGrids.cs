#region File Description
//********************************************************************
//    file name		: RoomGrids.cs
//    infomation	: Room内部のGrid管理
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
using System.Collections;
using System;
#endregion


/// <summary>
/// Room内のGridデータ
/// memo: Layerは"RoomGrids"
/// </summary>
public class RoomGrids : MonoBehaviour
{
    #region Member
    ComponentAccesser _Access;
    Postion _Postion;
    #endregion

    #region UNITY_DELEGATE
    void Awake()
    {
        _Access = new ComponentAccesser(gameObject);
        _Postion = new Postion(gameObject);

        _Postion.IntZ = 0;

        // 整数に変更
        _Postion.ToInteger();

        // 非表示にする
        //_Access.Renderer.enabled = false;    
    }
    #endregion UNITY_DELEGATE

    /// <summary>
    /// 領域データ
    /// </summary>
    /// <returns></returns>
    internal int[,] GetGridData()
    {
        //RoomManager.BaseGridNumを使ってね
        return new int[,]
        {
            { 1,1,1,1,1,1,1,1,1,1},
            { 1,1,1,1,1,1,1,1,1,1},
            { 1,1,1,1,1,1,1,1,1,1},
            { 1,1,1,1,1,1,1,1,1,1},
            { 1,1,1,1,1,1,1,1,1,1},
            { 1,1,1,1,1,1,1,1,1,1},
            { 1,1,1,1,1,1,1,1,1,1},
            { 1,1,1,1,1,1,1,1,1,1},
            { 1,1,1,1,1,1,1,1,1,1},
            { 1,1,1,1,1,1,1,1,1,1},
        };
    }
}
