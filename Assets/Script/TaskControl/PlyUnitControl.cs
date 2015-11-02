#region File Description
//********************************************************************
//    file name		: PlyUnitControl.cs
//    infomation	: 
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
#endregion

/// <summary>
/// 操作可ユニットのコントロール
/// </summary>
public class PlyUnitControl : UnitControl
{
    #region member
    /// <summary>
    /// 持ち上げ中をあらわすフラグ
    /// </summary>
    public bool LiftUp { get; set; }

    /// <summary>
    /// 持ち上げOKか確認デリゲート
    /// </summary>
    public CheckHandler _OnCheckLiftUpOK;

    /// <summary>
    /// ReportReleaseLiftUpデリゲート
    /// </summary>
    public ReportDropHandler _OnReportDragDrop;

    #endregion member

    internal override void DragEnter()
    {
        if (_OnCheckLiftUpOK())
        {
            LiftUp = true;
        }
    }

    internal override void DragDrop()
    {
        if (LiftUp)
        {
            int NewX, NewY;
            _OnReportDragDrop(_Postion.IntX, _Postion.IntY, out NewX, out NewY);

            _Postion.IntX = NewX;
            _Postion.IntY = NewY;

            LiftUp = false;
        }
    }

    protected override void Update()
    {
        if (LiftUp)
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _Postion.SetPos(point.x, point.y);
        }
    }
}