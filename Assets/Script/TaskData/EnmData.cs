#region File Description
//********************************************************************
//    file name		: EnmData.cs
//    infomation	: 
//********************************************************************
#endregion

#region Using Statements
using System.Collections.Generic;
using UnityEngine;
using System;
#endregion

/// <summary>
/// 
/// </summary>
public enum EnmType
{
    Enm01,
}

/// <summary>
/// 
/// </summary>
public enum EnmState
{
    Hide,
    Active,
//    Dead,
}



/// <summary>
/// Enm
/// </summary>
public partial class EnmData : FloorObjectData, IHideableData
{
    #region Member
    EnmType Type { get; set; }

    EnmState State { get; set; }
    private int m_i = 0;

    ReportPanelChange pOnReportPanelChange;
    #endregion

    public override string Name 
    { 
        get 
        { 
            return string.Format(Type.ToString() + "_{0}", m_i.ToString()); 
        }
    }

    #region Init
    public EnmData(int index, int x, int y, int EnmIndex, EnmType EnmType, bool IsHide) 
        : base(index, x, y) 
    {
        Type = EnmType;       
        m_i = EnmIndex;

        State = IsHide ? EnmState.Hide : EnmState.Active;

        pOnReportPanelChange = new ReportPanelChange(OnReportPanelChange);
    }

    public override void Ready()
    {
        Transform Prefab = null;

        switch (Type)
        {
            case EnmType.Enm01:
                Prefab = FloorManager.Instance.Enm01Prefab;
                break;
        }

        Transform tComponen = UnityEngine.Object.Instantiate(Prefab, new Vector3(X, Y, ZPos), Quaternion.identity) as Transform;
        tComponen.gameObject.name = Name;

        UnitControl unitCtl = tComponen.gameObject.GetComponent(typeof(UnitControl)) as UnitControl;

        // memo　Stateにより表示状態を変更
        switch (State)
        {
            case EnmState.Hide:
                unitCtl.ReqChangeHide();
                break;
            case EnmState.Active:
                unitCtl.ReqChangeActive();
                break;
        }

        SetPanelEvent(Vector2I.InvalidPos, Vector2);

        base.Ready();
    }

    /// <summary>
    /// Panel変更イベントを登録する
    /// </summary>
    private void SetPanelEvent(Vector2I OldPos, Vector2I NewPos)
    {
        if (OldPos != Vector2I.InvalidPos)
        {
            PanelData panel = GridHelper.GetPanel(OldPos.X, OldPos.Y);
            panel._OnReportPanelChange -= pOnReportPanelChange;
        }
        if (NewPos != Vector2I.InvalidPos)
        {
            PanelData panel = GridHelper.GetPanel(NewPos.X, NewPos.Y);
            panel._OnReportPanelChange += pOnReportPanelChange;
        }
    }
    #endregion Init

    /// <summary>
    /// SQMより移動指示(1Unitのみ)
    /// </summary>
    public static int Telegram_EnmAutoMoveSelectIndex { get; set; }

    protected override void ActionChangeSequence(SQID value)
    {
        if (value == SQID.EnmAutoMove && Telegram_EnmAutoMoveSelectIndex == this.Index)
        {
            Telegram_EnmAutoMoveSelectIndex = -1;

            UnitControl cCtl = TaskControl as UnitControl;

            int NextX, NextY;
            GetMoveablePos(this.Index, out NextX, out NextY);

            cCtl.ReqAutoMoving(NextX, NextY, OnMoveEnd);

            //Panel変更イベントを入れ替え
            SetPanelEvent(Vector2, new Vector2I(NextX, NextY));

            SequenceManager.SendMessage(MessageType.EnmAuroMoveStart, Index);
        }
    }

    #region Handle
    protected virtual void OnMoveEnd()
    {
        this.X = (TaskControl as UnitControl)._Postion.IntX;
        this.Y = (TaskControl as UnitControl)._Postion.IntY;

        SequenceManager.SendMessage(MessageType.EnmAutoMoveEnd, Index);
    }

    /// <summary>
    /// Panel変更通知受信
    /// </summary>
    void OnReportPanelChange(int X, int Y, PanelState PState) 
    {
        if (this.X != X || this.Y != Y) 
            throw new Exception("Error");

        if (State == EnmState.Hide && PState == PanelState.Open)
        {
            State = EnmState.Active;
            UnitControl uCtr = TaskControl as UnitControl;
            uCtr.ReqChangeActive();
        }
        else if (State == EnmState.Active && PState == PanelState.Close)
        {
            State = EnmState.Hide;
            UnitControl uCtr = TaskControl as UnitControl;
            uCtr.ReqChangeHide();
        }
    }
    #endregion Handle
}

public partial class EnmData : FloorObjectData, IHideableData
{
    #region Help
    /// <summary>
    /// 
    /// </summary>
    /// <param name="NextX"></param>
    /// <param name="NextY"></param>
    /// <returns></returns>
    public static int GetNextMoveEnmIndex()
    {
        //EnemyだけをListUp
        List<EnmData> list = new List<EnmData>();
        foreach (TaskData tData in FloorManager.TaskDataList.Values)
            if (tData is EnmData) list.Add(tData as EnmData);

        if (list.Count == 0) return -1;

        //全方向動けないものを除去
        for (int i = 0; i < list.Count; )
        {
            if (GetMoveableFreeSpaceGrid(list[i].Index).Count <= 0)
            {
                list.RemoveAt(i);
                continue;
            }
            i++;
        }

        //Listから乱数でObjectContent決定
        int selIndex = UnityEngine.Random.Range((int)0, (int)list.Count);

        return list[selIndex].Index;
    }

    /// <summary>
    /// Attak可能Enmのリストアップ
    /// </summary>
    /// <param name="NameSymbol"></param>
    /// <returns>List nullは返さない</returns>
    public static List<EnmData> GetAttackAbleEnmData()
    {
        //NameSymbolに引っかかるObjだけをListUp
        List<EnmData> list = new List<EnmData>();
        foreach (TaskData tData in FloorManager.TaskDataList.Values)
            if (tData is EnmData) list.Add(tData as EnmData);
        if (list.Count == 0) return list;

        //対象がいるか確認
        for (int i = 0; i < list.Count; )
        {
            if (PlyData.GetDefencePlyData(list[i].Index).Count <= 0)
            {
                list.RemoveAt(i);
                continue;
            }
            i++;
        }

        return list;
    }

    /// <summary>
    /// 移動可能POSをランダム選択
    /// </summary>
    static void GetMoveablePos(int index, out int NextX, out int NextY)
    {
        NextX = 0;
        NextY = 0;

        //乱数で移動方向を決定
        List<GridData> freeGrids = GetMoveableFreeSpaceGrid(index);

        int selGrid = UnityEngine.Random.Range((int)0, (int)freeGrids.Count);

        NextX = freeGrids[selGrid].X;
        NextY = freeGrids[selGrid].Y;
    }
    #endregion Help

    #region IHideableData メンバ
    public PanelOnOBjectState GetPanelOnOBjectState()
    {
        if (State == EnmState.Hide) return PanelOnOBjectState.自分が隠れている;

        PanelData panel = GridHelper.GetPanel(X, Y);
        if (panel.State == PanelState.Open) return PanelOnOBjectState.空いている上に居る;

        FloorObjectData[] datas = GridHelper.GetOnFloorObject(X, Y);

        foreach (FloorObjectData data in datas)
        {
            if (data is IHideableData == false) continue;
            IHideableData HData = data as IHideableData;

            if (HData.IsHide()) return PanelOnOBjectState.閉じている上に居る_下になにかある;
        }

        return PanelOnOBjectState.閉じている上に居る_下になにもない;
    }

    public bool IsHide()
    {
        if (State == EnmState.Hide) return true;
        else return false;
    }

    #endregion

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
        if (GridHelper.IsOnGrid(X, Y) && GridHelper.IsEnmMoveOK(X, Y) == false) returnList.Add(GridHelper.GetGrid(X, Y));
        X = charData.X + 1; Y = charData.Y;
        if (GridHelper.IsOnGrid(X, Y) && GridHelper.IsEnmMoveOK(X, Y) == false) returnList.Add(GridHelper.GetGrid(X, Y));
        X = charData.X; Y = charData.Y - 1;
        if (GridHelper.IsOnGrid(X, Y) && GridHelper.IsEnmMoveOK(X, Y) == false) returnList.Add(GridHelper.GetGrid(X, Y));
        X = charData.X; Y = charData.Y + 1;
        if (GridHelper.IsOnGrid(X, Y) && GridHelper.IsEnmMoveOK(X, Y) == false) returnList.Add(GridHelper.GetGrid(X, Y));

        return returnList;
    }
}
