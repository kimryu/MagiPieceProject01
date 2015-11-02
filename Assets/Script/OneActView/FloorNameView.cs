#region File Description
//********************************************************************
//    file name		: FloorNameView.cs
//    infomation	: フロア名表示
//********************************************************************
#endregion

#region Using Statements
using System;
using UnityEngine;
#endregion

/// <summary>
/// フロア名表示
/// </summary>
class FloorNameView : OneActView
{
    internal int FloorNo { get; set; }

    /// <summary>
    /// 実行要求
    /// </summary>
    public override void ReqView(ReportEndHandler onViewEnd)
    {
        // FloorNoに合わせて表示するScriptを切り替える
        base.ReqView(onViewEnd);
    }
}

