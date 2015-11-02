#region File Description
//********************************************************************
//    file name		: SetObjectData.cs
//    infomation	: 
//********************************************************************
#endregion

#region Using Statements
using System.Collections.Generic;
#endregion

/// <summary>
/// 
/// </summary>
public partial class SetObjectData : FloorObjectData, IHideableData
{
    public SetObjectData(int index, int x, int y)
        : base(index, x, y)
    {
    }

    public override string Name
    {
        get { throw new System.NotImplementedException(); }
    }

    protected override void ActionChangeSequence(SQID value)
    {
        throw new System.NotImplementedException();
    }

    #region IHideableData メンバ

    public PanelOnOBjectState GetPanelOnOBjectState()
    {
        throw new System.NotImplementedException();
    }

    public bool IsHide()
    {
        throw new System.NotImplementedException();
    }

    #endregion

    internal bool IsRideOK()
    {
        throw new System.NotImplementedException();
    }
}

public partial class SetObjectData : FloorObjectData, IHideableData
{

}