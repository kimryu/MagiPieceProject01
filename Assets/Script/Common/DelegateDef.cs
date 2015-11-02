#region File Description
//********************************************************************
//    file name		: DelegateDef.cs
//    infomation	: デリゲート管理
//********************************************************************
#endregion

#region Using Statements
using System;
#endregion

#region Info
//********************************************************************
//    命名規約
// 関数名:Action○○ = 変更通知を受けた場合の動作関数
// 関数名:On○○ = delegete対象/metaによる定義対象
// delegate名:Report○○Handler = 通知系イベント
// delegate名:Check○○Handler = 確認系イベント
// メンバー名:_On○○ = delegateメンバー名
// メンバー名:Telegram_○○ = 他Taskからの置き土産 通知等に用いる
// 関数名:Report○○ = 外部Taskから直接Callする通知関数
// 関数名:Req○○ = 外部Taskから直接Callする命令関数
//********************************************************************
#endregion Info

/// <summary>
/// 動作終了通知
/// </summary>
public delegate void ReportEndHandler();

/// <summary>
/// パネル変更通知
/// </summary>
public delegate void ReportPanelChange(int X, int Y, PanelState State);

/// <summary>
/// D&DでDropEntryしたときの通知
/// </summary>
public delegate void ReportDropHandler(int TransferX, int TransferY, out int newX, out int newY);

/// <summary>
/// D&DでDropEntryしたときの通知
/// </summary>
/// <returns>正常に完了したか</returns>
public delegate bool ReportDropHandlerBool(int TransferX, int TransferY);

/// <summary>
/// 上位タスクへチェック
/// </summary>
public delegate bool CheckHandler();