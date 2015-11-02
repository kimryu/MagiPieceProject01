#region File Description
//********************************************************************
//    file name		: TablePeaceControl.cs
//    infomation	: 
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
using System.Collections;
#endregion

/// <summary>
/// テーブルに置かれたピースのコントロール
/// </summary>
class TablePeaceControl : TaskControl
{
    public enum TablePeaceState
    {
        OnTable,
        LiftUp,
        ComeBack,
        ViewState,
    }

    #region member
    public TablePeaceState State
    {
        get { return m_State; }
        set
        {
            m_State = value;
            switch (m_State)
            {
                case TablePeaceState.OnTable:
                case TablePeaceState.ComeBack:
                case TablePeaceState.LiftUp:
                    // Peace表示
                    _Access.Renderer.enabled = true;
                    break;

                case TablePeaceState.ViewState:
                    // State表示
                    _Access.Renderer.enabled = false;
                    break;
            }
        }
    }
    TablePeaceState m_State;

    Vector3 DefPos;

    /// <summary>
    /// 持ち上げOKか確認デリゲート
    /// </summary>
    public CheckHandler _OnCheckLiftUpOK;

    /// <summary>
    /// ReportReleaseLiftUpデリゲート
    /// </summary>
    public ReportDropHandlerBool _OnReportDragDrop;

    #endregion member

    internal override void DragEnter()
    {
        if (State != TablePeaceState.OnTable) 
            return;

        if (_OnCheckLiftUpOK())
        {
            State = TablePeaceState.LiftUp;
        }
    }

    internal override void DragDrop()
    {
        if (State == TablePeaceState.LiftUp)
        {
            bool result = _OnReportDragDrop(_Postion.IntX, _Postion.IntY);

            if (result == false)
            {
                //Comeback
                State = TablePeaceState.ComeBack;

                // 自動移動開始
                Hashtable table = new Hashtable();

                table.Add("x", DefPos.x);
                table.Add("y", DefPos.y);
                table.Add("speed", 5.0f);

                table.Add("easetype", iTween.EaseType.linear);

                table.Add("onstart", "StartHandler");       // トゥイーン開始時にStartHandler()を呼ぶ
                table.Add("onstartparams", name + " ComeBack Start");        // StartHandler()の引数に渡す値
                table.Add("onupdate", "UpdateHandler");     // トゥイーンを開始してから毎フレームUpdateHandler()を呼ぶ
                table.Add("onupdateparams", name + " ComeBack Update");      // UpdateHandler()の引数に渡す値
                table.Add("oncomplete", "CompleteHandler"); // トゥイーン終了時にCompleteHandler()を呼ぶ
                table.Add("oncompleteparams", name + " ComeBack Complete");  // CompleteHandler()の引数に渡す値

                iTween.MoveTo(gameObject, table);

            }
            else
            {
                //表示切替
                State = TablePeaceState.ViewState;
                _Postion.X = DefPos.x;
                _Postion.Y = DefPos.y;
                _Postion.Z = DefPos.z;
            }
        }
    }

    private void StartHandler(string message)
    {
        Debug.Log(message);
    }

    private void UpdateHandler(string message)
    {
        //Debug.Log(message);
    }

    private void CompleteHandler(string message)
    {
        Debug.Log(message);

        State = TablePeaceState.OnTable;
    }


    void Start()
    {
        // デフォルト位置記憶
        DefPos = _Postion.Vector3;
    }

    protected override void Update()
    {
        if (State == TablePeaceState.LiftUp)
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _Postion.SetPos(point.x, point.y);
        }
    }
}
