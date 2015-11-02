#region File Description
//********************************************************************
//    file name		: TaskData.cs
//    infomation	: 
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
#endregion

/// <summary>
/// TaskControlの情報管理
/// ステータスも扱い始めるなら「ScriptableObject」の運用も考える
/// </summary>
public abstract class TaskData
{
    #region member
    /// <summary>
    /// GmObjName
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// TaskIndex(TID)
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// TaskDataの破棄OK
    /// </summary>
    public bool IsDestroyOK
    {
        get { return m_IsDestroyOK; }

        set
        {
            m_IsDestroyOK = value;

            if (m_IsDestroyOK)
                FloorManager.Telegram_HaveDestroyTask = true;
        }
    }

    bool m_IsDestroyOK = false;

    /// <summary>
    /// 本TaskのZ座標
    /// </summary>
    protected abstract int ZPos { get; }
    #endregion member

    #region Access
    public virtual GameObject GmObj { get { return GameObject.Find(Name); } }

    public virtual TaskControl TaskControl { get { return GameObject.Find(Name).GetComponent(typeof(TaskControl)) as TaskControl; } }
    #endregion 

    #region Init
    public TaskData(int index)
    {
        Index = index;
    }

    public virtual void Ready()
    {
        // SQMへイベントを登録
        SequenceManager.NowSQID.action += ActionChangeSequence;
    }
    #endregion Init

    #region Destroy
    /// <summary>
    /// FloorManagerからの削除要求
    /// </summary>
    public virtual void ReqDestroy()
    {
        // GMObjの破棄
        GameObject.Destroy(GmObj);

        // SQMのイベントを削除
        SequenceManager.NowSQID.action -= ActionChangeSequence;
    }
    #endregion 

    #region Sequence制御
    /// <summary>
    /// SQID変更検知
    /// </summary>
    /// <param name="value"></param>
    protected abstract void ActionChangeSequence(SQID value);

    ///// <summary>
    ///// 上位タスクへ待機要求
    ///// </summary>
    //protected virtual void OnSetWait()
    //{
    //    // SQMへ待ちを通知
    //    SequenceManager.ReportWaitTask(Index);
    //}

    ///// <summary>
    ///// 上位タスクへ完了を通知
    ///// </summary>
    //protected virtual void OnReportEnd()
    //{
    //    // SQMに完了を通知
    //    SequenceManager.ReportEnd(Index);
    //}
    #endregion Sequence制御
}
