#region File Description
//********************************************************************
//    file name		: MessageType.cs
//    infomation	: [SequenceManager]Message種別
//********************************************************************
#endregion

#region Using Statements
#endregion

/// <summary>
/// [SequenceManager]Message種別
/// </summary>
public enum MessageType
{
    FloorInitEnd,
    ViewFloorNameStart,
    ViewFloorNameEnd,
    PlyDDStart,
    PlyDDCancel,
    PlyDDEnd,
    TablePeaceDDStart,
    TablePeaceDDCancel,
    TablePeaceDDEnd,
    EnmNonMove,
    EnmAuroMoveStart,
    EnmAutoMoveEnd,

    ClickPanelOpenStart,
    PanelOpenStart,
    PanelOpenEnd,
    PanelCloseStart,
    PanelCloseEnd,
}