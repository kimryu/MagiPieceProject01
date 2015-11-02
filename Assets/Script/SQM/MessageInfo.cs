#region File Description
//********************************************************************
//    file name		: MessageInfo.cs
//    infomation	: [SequenceManager]Message
//********************************************************************
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using UnityEngine;
#endregion

/// <summary>
/// [SequenceManager]Message
/// </summary>
class MessageInfo
{
    public MessageType Type;
    public int TaskIndex;

    public MessageInfo(MessageType _Type, int _TaskIndex)
    {
        this.Type = _Type;
        this.TaskIndex = _TaskIndex;
    }

    public override string ToString()
    {
        return Type.ToString() + "(" + TaskIndex.ToString() + ")";
    }
}
