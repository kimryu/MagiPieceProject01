#region File Description
//********************************************************************
//    file name		: SystemModalDialog.cs
//    infomation	: システムモーダルダイアログ
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
using System.Collections;
#endregion

/// <summary>
/// システムモーダルダイアログ
/// 自分自身以外のダイアログ入力禁止 + ゲーム内時間の停止を行います
/// </summary>
public class SystemModalDialog : Dialog
{
    #region UNITY_DELEGATE
    /// <summary>
    /// Awake
    /// </summary>
    void Awake()
    {
        IsUIEnable = false;
        IsGameEnable = false;
    }
    #endregion UNITY_DELEGATE
}