#region File Description
//********************************************************************
//    file name		: IHideableData.cs
//    infomation	: 
//********************************************************************
#endregion

#region Using Statements

#endregion

/// <summary>
/// 
/// </summary>
public enum PanelOnOBjectState
{
    自分が隠れている,
    閉じている上に居る_下になにかある,
    閉じている上に居る_下になにもない,
    空いている上に居る,
}

/// <summary>
/// 
/// </summary>
public interface IHideableData
{
    PanelOnOBjectState GetPanelOnOBjectState();

    bool IsHide();
}
