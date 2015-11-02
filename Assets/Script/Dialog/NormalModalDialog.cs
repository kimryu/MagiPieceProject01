#region File Description
//********************************************************************
//    file name		: NormalModalDialog.cs
//    infomation	: モーダルダイアログ
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
using System.Collections;
#endregion

/// <summary>
/// モーダルダイアログ
/// 自分自身以外のダイアログ入力禁止を行います
/// </summary>
public class NormalModalDialog : Dialog
{
    #region UNITY_DELEGATE
    /// <summary>
    /// Awake
    /// </summary>
    void Awake()
    {
        IsUIEnable = false;
        IsGameEnable = true;
    }
    #endregion UNITY_DELEGATE
}