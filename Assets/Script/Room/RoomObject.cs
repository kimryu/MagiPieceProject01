#region File Description
//********************************************************************
//    file name		: RoomObject.cs
//    infomation	: Room内のObejct
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
using System.Collections;
using System;
#endregion

/// <summary>
/// Room内のObejct
/// Layerは"RoomObject"
/// </summary>
public class RoomObject : MonoBehaviour 
{
    // RoomManagerの「領域データ数値定義」参照
    public int GridDataNum;

    #region Member
    public ComponentAccesser _Access;
    public Postion _Postion;
    #endregion

    void Awake()
    {
        _Access = new ComponentAccesser(gameObject);
        _Postion = new Postion(gameObject);

        _Postion.Z = CalcZPos();

        // 整数に変更
        _Postion.ToInteger();

    }

    float CalcZPos()
    {
        //-1より0.1ずつへらす
        float Z = -1.0f - (0.1f * _Postion.Y);

        return Z;
    }
}
