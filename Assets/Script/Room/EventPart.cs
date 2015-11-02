#region File Description
//********************************************************************
//    file name		: EventPart.cs
//    infomation	: 
//********************************************************************
#endregion

#region Using Statements
using System;
#endregion

/// <summary>
/// RoomないのEvent識別
/// </summary>
public enum EventType
{
    NULL,
    /// <summary>
    /// Floor1へ移動
    /// </summary>
    Floor1,
}

/// <summary>
/// RoomManager用Event管理
/// 「RoomEvent」タグの付いたオブジェクトを取得しこちらに変換する
/// </summary>
public class EventPart
{
    public Vector2I Pos { get; set; }

    public Vector2I SubPos { get; set; }

    public string SrioFileName { get; set; }

    public EventType Type { get; set; }

    public bool IsClickEvent()
    {
        return Pos != SubPos;
    }
    public bool IsFootEvent()
    {
        return Pos == SubPos;
    }
}