#region File Description
//********************************************************************
//    file name		: UnitControl.cs
//    infomation	: 
//********************************************************************
#endregion

#region Using Statements
using System.Collections;
using UnityEngine;
#endregion

/// <summary>
/// ユニットのコントロール
/// </summary>
public class UnitControl : TaskControl
{
    #region Member
    private ReportEndHandler _OnMoveEnd;
    #endregion Member

    /// <summary>
    /// 自動移動要求
    /// </summary>
    public virtual void ReqAutoMoving(int NextX, int NextY, ReportEndHandler onMoveEnd)
    {
        _OnMoveEnd = onMoveEnd;

        Hashtable table = new Hashtable();

        table.Add("x", NextX);
        table.Add("y", NextY);
        table.Add("speed", 3.0f);

        table.Add("easetype", iTween.EaseType.linear);

        table.Add("onstart", "StartHandler");       // トゥイーン開始時にStartHandler()を呼ぶ
        table.Add("onstartparams", name + " Start");        // StartHandler()の引数に渡す値
        table.Add("onupdate", "UpdateHandler");     // トゥイーンを開始してから毎フレームUpdateHandler()を呼ぶ
        table.Add("onupdateparams", name + " Update");      // UpdateHandler()の引数に渡す値
        table.Add("oncomplete", "CompleteHandler"); // トゥイーン終了時にCompleteHandler()を呼ぶ
        table.Add("oncompleteparams", name + " Complete");  // CompleteHandler()の引数に渡す値

        iTween.MoveTo(gameObject, table);

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

        if (_OnMoveEnd != null)
            _OnMoveEnd();
    }

    internal void ReqChangeHide()
    {
        _Access.Animator.SetTrigger("OnChangeHide");
    }

    internal void ReqChangeActive()
    {
        _Access.Animator.SetTrigger("OnChangeActive");
    }

    void OnChangeHideEnd()
    {
        _Access.Animator.ResetTrigger("OnChangeHide");
    }

    void OnChangeActiveEnd()
    {
        _Access.Animator.ResetTrigger("OnChangeActive");
    }
}