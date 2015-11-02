#region File Description
//********************************************************************
//    file name		: PanelData.cs
//    infomation	: 
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
using System;
using System.Collections.Generic;
#endregion

/// <summary>
/// パネル状態
/// </summary>
public enum PanelState
{
    Close,
    TurnOver,
    Open,
    TurnDown,
}

/// <summary>
/// 
/// </summary>
public partial class PanelData : FloorObjectData
{
    #region Member
    /// <summary>
    /// パネル状態
    /// </summary>
    public PanelState State { get; set; }

    /// <summary>
    /// 外部タスクへ変更を通知するマルチキャストデリゲート
    /// </summary>
    public ReportPanelChange _OnReportPanelChange;
    
    #endregion

    #region Init
    public PanelData(int index, int x, int y, PanelState _State)
        : base(index, x, y)
    {
        State = _State;
    }

    public override string Name { get { return string.Format("Panel" + "{0}{1}", X.ToString(), Y.ToString()); } }

    protected override int ZPos
    {
        get { return -2; }
    }

    public override void Ready()
    {
        Transform tComponen = UnityEngine.Object.Instantiate(FloorManager.Instance.PanelPrefab, new Vector3(X, Y, ZPos), Quaternion.identity) as Transform;
        tComponen.gameObject.name = Name;
        PanelControl taskCtl = tComponen.gameObject.GetComponent(typeof(PanelControl)) as PanelControl;

        taskCtl._OnCheckTurnOverOK = new CheckHandler(OnCheckTurnOverOK);
        taskCtl._OnTurnOverEnd = new ReportEndHandler(OnTurnOverEnd);
        taskCtl._OnTurnDownEnd = new ReportEndHandler(OnTurnDownEnd);
        base.Ready();
    }
    #endregion Init

    protected override void ActionChangeSequence(SQID value)
    {
    }

    bool OnCheckTurnOverOK()
    {
        if (SequenceManager.NowSQID.Value == SQID.PlyInputWait && State == PanelState.Close)
        {
            State = PanelState.TurnOver;

            if (_OnReportPanelChange != null)
            {
                _OnReportPanelChange(X, Y, PanelState.Open);
            }

            SequenceManager.SendMessage(MessageType.ClickPanelOpenStart, Index);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 捲り終わった通知
    /// </summary>
    void OnTurnOverEnd()
    {
        State = PanelState.Open;

        SequenceManager.SendMessage(MessageType.PanelOpenEnd, Index);
    }

    /// <summary>
    /// 伏せ終わった通知
    /// </summary>
    void OnTurnDownEnd()
    {
        State = PanelState.Close;

        SequenceManager.SendMessage(MessageType.PanelCloseEnd, Index);
    }

    public void ReqTurnOver()
    {
        if (State != PanelState.Close) throw new Exception("Error");

        PanelControl pCtl = TaskControl as PanelControl;
        pCtl.ReqTurnOver();
        State = PanelState.TurnOver;

        // このタイミングで通知
        if (_OnReportPanelChange != null)
        {
            _OnReportPanelChange(X, Y, PanelState.Open);
        }

        SequenceManager.SendMessage(MessageType.PanelOpenStart, Index);
    }

    public void ReqTurnDown()
    {
        if (State != PanelState.Open) throw new Exception("Error");

        PanelControl pCtl = TaskControl as PanelControl;
        pCtl.ReqTurnDown();
        State = PanelState.TurnDown;

        // このタイミングで通知
        if (_OnReportPanelChange != null)
        {
            _OnReportPanelChange(X, Y, PanelState.Close);
        }

        SequenceManager.SendMessage(MessageType.PanelCloseStart, Index);
    }
}

public partial class PanelData : FloorObjectData
{

}