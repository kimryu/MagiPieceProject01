#region File Description
//********************************************************************
//    file name		: PanelControl.cs
//    infomation	: 
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
#endregion

/// <summary>
/// Panelのコントロール
/// めくり、伏せなどの動作をコントロールする
/// </summary>
class PanelControl : TaskControl
{
    /// <summary>
    /// ＊＊＊OKか確認デリゲート
    /// </summary>
    public CheckHandler _OnCheckTurnOverOK;
    public ReportEndHandler _OnTurnOverEnd;
    public ReportEndHandler _OnTurnDownEnd;

    internal override void Click() 
    {
        if (_OnCheckTurnOverOK())
        {
            ReqTurnOver();
        }
    }

    internal void ReqTurnDown()
    {
        _Access.Animator.SetTrigger("OnTurnDown");
    }

    internal void ReqTurnOver()
    {
        _Access.Animator.SetTrigger("OnTurnOver");
    }

    void OnTurnDownEnd()
    {
        _Access.Animator.ResetTrigger("OnTurnDown");

        _OnTurnOverEnd();
    }

    void OnTurnOverEnd()
    {
        _Access.Animator.ResetTrigger("OnTurnOver");

        _OnTurnDownEnd();
    }
}