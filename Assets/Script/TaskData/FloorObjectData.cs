#region File Description
//********************************************************************
//    file name		: FloorObjectData.cs
//    infomation	: 
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
using System.Collections.Generic;
using System;
#endregion


/// <summary>
/// Gridに乗るChar
/// </summary>
public abstract partial class FloorObjectData : TaskData
{
    protected override int ZPos
    {
        get { return -1; }
    }

    public int X { get; set; }
    public int Y { get; set; }

    public Vector2I Vector2 { get { return new Vector2I(X, Y); } }

    public FloorObjectData(int index, int x, int y)
        : base(index)
    {
        this.X = x;
        this.Y = y;
    }

}

/// <summary>
/// ヘルプ関数
/// </summary>
public abstract partial class FloorObjectData : TaskData
{
}


