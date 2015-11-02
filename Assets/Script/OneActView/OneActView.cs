#region File Description
//********************************************************************
//    file name		: OneActView.cs
//    infomation	: 1動作し自動で消えるGameObjectを管理
//********************************************************************
#endregion

#region Using Statements
using System;
using UnityEngine;
#endregion

/// <summary>
/// 1動作し自動で消えるGameObjectを管理
/// </summary>
public class OneActView : MonoBehaviour
{
    #region Member
    public ComponentAccesser _Access;
    public Postion _Postion;

    ReportEndHandler _OnViewEnd;
    #endregion

    /// <summary>
    /// 実行要求
    /// </summary>
    public virtual void ReqView(ReportEndHandler onViewEnd)
    {
        _OnViewEnd = onViewEnd;
        ReqView();
    }

    /// <summary>
    /// 実行要求
    /// </summary>
    public void ReqView()
    {
        _Access.Renderer.enabled = true;
        _Access.Animator.enabled = true;
        _Access.Animator.SetTrigger("OnTrigger");
    }

    /// <summary>
    /// Animator用完了通知
    /// </summary>
    void OnAnimEnd()
    {
        _Access.Animator.ResetTrigger("OnTrigger");
        _Access.Animator.enabled = false;
        _Access.Renderer.enabled = false;

        if (_OnViewEnd != null)
        {
            _OnViewEnd();
        }

        // 自らを破棄
        GameObject.Destroy(this.gameObject);
    }

    #region UNITY_DELEGATE
    void Awake()
    {
        _Access = new ComponentAccesser(gameObject);
        _Postion = new Postion(gameObject);
    }
    #endregion UNITY_DELEGATE
}

