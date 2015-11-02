#region File Description
//********************************************************************
//    file name		: MagiPeaceData.cs
//    infomation	: 
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
using System;
#endregion

/// <summary>
/// 
/// </summary>
class MagiPeaceData : PlyData
{
    public enum MagiPeaceType
    {
        MagiPeace01,
    }

    public enum MagiPeaceState
    {
        Table,
        Peace,
    }

    public MagiPeaceType Type { get; set; }
    public MagiPeaceState State { get; set; }
    public int TableNo { get; set; }

    public override string Name
    {
        get { return Type.ToString(); }
    }
    public string TableName
    {
        get { return "Table" + Name; }
    }
    protected override void ActionChangeSequence(SQID value)
    {
    }

    #region Init
    public MagiPeaceData(int index, int x, int y, MagiPeaceType MagiPeaceType, int iTableNo) 
        : base(index, x, y) 
    { 
        Type = MagiPeaceType;
        TableNo = iTableNo;
    }

    Vector3 TablePos
    {
        get
        {
            switch (TableNo)
            {
                case 0:
                    return new Vector3(1, -2, -8);
                case 1:
                    return new Vector3(2, -2, -8);
                case 2:
                    return new Vector3(3, -2, -8);
                default:
                    throw new Exception("Error");
            }
        }
    }

    public override void Ready()
    {
        Transform Prefab = null;

        switch (Type)
        {
            case MagiPeaceType.MagiPeace01:
                Prefab = FloorManager.Instance.TableMagiPeace01Prefab;
                break;
        }

        Transform tComponen = UnityEngine.Object.Instantiate(Prefab, TablePos, Quaternion.identity) as Transform;
        tComponen.gameObject.name = TableName;

        TablePeaceControl unitCtl = tComponen.gameObject.GetComponent(typeof(TablePeaceControl)) as TablePeaceControl;

        unitCtl._OnCheckLiftUpOK += new CheckHandler(OnCheckLiftUpOKTable);
        unitCtl._OnReportDragDrop += new ReportDropHandlerBool(OnDragDropTable);

        base.Ready();
    }

    public void Ready_Peace()
    {
        Transform Prefab = null;

        switch (Type)
        {
            case MagiPeaceType.MagiPeace01:
                Prefab = FloorManager.Instance.MagiPeace01Prefab;
                break;
        }

        Transform tComponen = UnityEngine.Object.Instantiate(Prefab, new Vector3(X, Y, ZPos), Quaternion.identity) as Transform;
        tComponen.gameObject.name = Name;

        PlyUnitControl unitCtl = tComponen.gameObject.GetComponent(typeof(PlyUnitControl)) as PlyUnitControl;
        unitCtl._OnCheckLiftUpOK = new CheckHandler(OnCheckLiftUpOK);
        unitCtl._OnReportDragDrop = new ReportDropHandler(OnDragDrop);

    }
    #endregion Init


    #region Handle_Table
    protected bool OnCheckLiftUpOKTable()
    {
        if (State != MagiPeaceState.Table)
        {
            return false;
        }

        if (SequenceManager.NowSQID.Value != SQID.PlyInputWait)
        {
            return false;
        }

        SequenceManager.SendMessage(MessageType.TablePeaceDDStart, Index);
        return true;
    }

    protected bool OnDragDropTable(int TransferX, int TransferY)
    {
        bool IsOK = true;

        if (GridHelper.IsOnGrid(TransferX, TransferY) == false) IsOK = false;// Grid外
        if (GridHelper.IsPlyDragEntryOK(TransferX, TransferY) == false) IsOK = false;

        // 許可できない位置
        if (IsOK == false)
        {
            SequenceManager.SendMessage(MessageType.TablePeaceDDCancel, Index);
            return false;
        }

        // Data更新
        X = TransferX;
        Y = TransferY;

        State = MagiPeaceState.Peace;

        // Peaceを配置
        Ready_Peace();

        // UI切替要求
        UIData.VisiblePeaceState(TableNo);

        // SQMへ完了通知
        SequenceManager.SendMessage(MessageType.TablePeaceDDEnd, Index);
        return true;
    }
    #endregion Handle_Table
}