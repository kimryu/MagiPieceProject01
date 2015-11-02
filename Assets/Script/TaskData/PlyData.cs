#region File Description
//********************************************************************
//    file name		: PlyData.cs
//    infomation	: 
//********************************************************************
#endregion

#region Using Statements
using System.Collections.Generic;
using System;
using UnityEngine;
#endregion

/// <summary>
/// 
/// </summary>
public abstract partial class PlyData : FloorObjectData
{
    public PlyData(int index, int x, int y) : base(index, x, y) { }

    #region Handle
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected virtual bool OnCheckLiftUpOK()
    {
        if (SequenceManager.NowSQID.Value != SQID.PlyInputWait)
        {
            return false;
        }

        SequenceManager.SendMessage(MessageType.PlyDDStart, Index);

        return true;
    }

    /// <summary>
    /// Drop
    /// </summary>
    protected void OnDragDrop(int TransferX, int TransferY, out int newX, out int newY)
    {
        bool IsOK = true;

        if (X == TransferX && Y == TransferY) IsOK = false;// 同じ位置へリリース
        if (GridHelper.IsPlyMoveOK(TransferX, TransferY) == false) IsOK = false;

        // 許可できない位置
        if (IsOK == false)
        {
            newX = X;
            newY = Y;
            SequenceManager.SendMessage(MessageType.PlyDDCancel, Index);
            return;
        }

        // 新しい位置へ移動
        newX = TransferX;
        newY = TransferY;

        // Data更新
        X = TransferX;
        Y = TransferY;

        SequenceManager.SendMessage(MessageType.PlyDDEnd, Index);

    }
    #endregion Handle
}

public abstract partial class PlyData : FloorObjectData
{
    /// <summary>
    /// Atk対象のPlyを取得
    /// </summary>
    /// <param name="EnmIndex"></param>
    /// <returns></returns>
    public static List<PlyData> GetDefencePlyData(int EnmIndex)
    {
        EnmData AtkData = FloorManager.TaskDataList[EnmIndex] as EnmData;

        if (AtkData == null) throw new Exception("Error");

        List<PlyData> list = new List<PlyData>();

        foreach (TaskData tData in FloorManager.TaskDataList.Values)
        {
            if (tData is PlyData)
            {
                PlyData plyData = tData as PlyData;
                if (GridHelper.IsNextGrid(plyData.X, plyData.Y, AtkData.X, AtkData.Y))
                    list.Add(plyData);
            }
        }
        return list;
    }

    /// <summary>
    /// 移動可能範囲で空いているGridを返す
    /// </summary>
    public static List<GridData> GetMoveableFreeSpaceGrid(int index)
    {
        List<GridData> returnList = new List<GridData>();

        FloorObjectData charData = FloorManager.TaskDataList[index] as FloorObjectData;

        if (charData == null) throw new Exception("配置のOBjectじゃない");

        int X, Y;
        //4方向
        X = charData.X - 1; Y = charData.Y;
        if (GridHelper.IsOnGrid(X, Y) && GridHelper.IsPlyMoveOK(X, Y) == false) returnList.Add(GridHelper.GetGrid(X, Y));
        X = charData.X + 1; Y = charData.Y;
        if (GridHelper.IsOnGrid(X, Y) && GridHelper.IsPlyMoveOK(X, Y) == false) returnList.Add(GridHelper.GetGrid(X, Y));
        X = charData.X; Y = charData.Y - 1;
        if (GridHelper.IsOnGrid(X, Y) && GridHelper.IsPlyMoveOK(X, Y) == false) returnList.Add(GridHelper.GetGrid(X, Y));
        X = charData.X; Y = charData.Y + 1;
        if (GridHelper.IsOnGrid(X, Y) && GridHelper.IsPlyMoveOK(X, Y) == false) returnList.Add(GridHelper.GetGrid(X, Y));

        return returnList;
    }
}
