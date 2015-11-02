#region File Description
//********************************************************************
//    file name		: GridHelper.cs
//    infomation	: Gridの判定に関するヘルパ
//********************************************************************
#endregion

#region Using Statements
using System.Collections.Generic;
#endregion

/// <summary>
/// Gridの判定に関するヘルパ
/// </summary>
public static class GridHelper
{
    #region Grid
    public static bool IsOnGrid(int X, int Y)
    {
        foreach (TaskData tData in FloorManager.TaskDataList.Values)
        {
            if (tData is GridData)
            {
                GridData grid = tData as GridData;
                if (grid.X == X && grid.Y == Y)
                    return true;
            }
        }
        return false;
    }

    public static GridData GetGrid(int X, int Y)
    {
        foreach (TaskData tData in FloorManager.TaskDataList.Values)
        {
            if (tData is GridData)
            {
                GridData grid = tData as GridData;
                if (grid.X == X && grid.Y == Y)
                    return grid;
            }
        }
        return null;
    } 

    public static bool IsNextGrid(int X1, int Y1, int X2, int Y2)
    {
        if (X1 == X2 - 1 && Y1 == Y2) return true;
        if (X1 == X2 + 1 && Y1 == Y2) return true;
        if (X1 == X2 && Y1 == Y2 - 1) return true;
        if (X1 == X2 && Y1 == Y2 + 1) return true;
        return false;
    }
    #endregion Grid

    #region Panel
    public static PanelData GetPanel(int X, int Y)
    {
        foreach (TaskData tData in FloorManager.TaskDataList.Values)
        {
            if (tData is PanelData)
            {
                PanelData panel = tData as PanelData;
                if (panel.X == X && panel.Y == Y)
                    return panel;
            }
        }
        return null;
    }
    #endregion Panel

    #region FloorObject
    /// <summary>
    /// Grid上のFloorObjectDataをすべて取得
    /// </summary>
    internal static FloorObjectData[] GetOnFloorObject(int X, int Y)
    {
        List<FloorObjectData> list = new List<FloorObjectData>();
        foreach (TaskData tData in FloorManager.TaskDataList.Values)
        {
            if (tData is FloorObjectData)
            {
                FloorObjectData fData = tData as FloorObjectData;
                if (fData.X == X && fData.Y == Y)
                    list.Add(fData);
            }
        }
        return list.ToArray();
    }

    /// <summary>
    /// Grid上にFloorObjectがいるか(Hide,Active関わらず)
    /// </summary>
    public static bool IsOnFloorObject(int X, int Y, int myIndex)
    {
        foreach (TaskData tData in FloorManager.TaskDataList.Values)
        {
            if (tData.Index == myIndex) continue;

            if (tData is FloorObjectData)
            {
                FloorObjectData cData = tData as FloorObjectData;
                if (cData.X == X && cData.Y == Y)
                    return true;
            }
        }
        return false;
    }

    public static bool IsOnFloorObject(int X, int Y)
    {
        foreach (TaskData tData in FloorManager.TaskDataList.Values)
        {
            if (tData is FloorObjectData)
            {
                FloorObjectData cData = tData as FloorObjectData;
                if (cData.X == X && cData.Y == Y)
                    return true;
            }
        }
        return false;
    }
    #endregion FloorObject

    #region MoveAble
    /// <summary>
    /// EnmDataが移動可能か判定
    /// </summary>
    public static bool IsEnmMoveOK(int X, int Y)
    {
        FloorObjectData[] datas = GetOnFloorObject(X, Y);

        foreach (FloorObjectData data in datas)
        {
            if (data is PlyData)
                return false;
            if (data is EnmData)
            {
                EnmData enm = data as EnmData;
                if (enm.IsHide() == false) return false;
            }
            if (data is SetObjectData)
            {
                SetObjectData setObj = data as SetObjectData;
                if (setObj.IsRideOK() == false) return false;
            }
        }
        return true;
    }

    /// <summary>
    /// PlyDataが移動可能か判定
    /// </summary>
    public static bool IsPlyMoveOK(int X, int Y)
    {
        FloorObjectData[] datas = GetOnFloorObject(X, Y);

        foreach (FloorObjectData data in datas)
        {
            if (data is PlyData)
                return false;
            if (data is EnmData)
                return false;
            if (data is SetObjectData)
            {
                SetObjectData setObj = data as SetObjectData;
                if (setObj.IsRideOK() == false) return false;
            }
        }
        return true;
    }

    public static bool IsPlyDragEntryOK(int X, int Y)
    {
        if (GridHelper.IsOnGrid(X, Y) == false) return false;// Grid外
        if (GridHelper.IsOnFloorObject(X, Y) == true) return  false; // すでに要る場所

        return true;
    }
    #endregion MoveAble
}