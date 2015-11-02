#region File Description
//********************************************************************
//    file name		: NotificationObject.cs
//    infomation	: 変数の変更を通知
//********************************************************************
#endregion

#region Using Statements
using System;
#endregion

/// <summary>
/// 変数の変更を通知する
/// </summary>
/// <typeparam name="T">通知する変数の型</typeparam>
[System.Serializable]
public class NotificationObject<T>
{
    public delegate void NotificationAction(T t);

    private T data;

    public NotificationObject(T t)
    {
        Value = t;
    }

    public NotificationObject()
    {
    }

    ~NotificationObject()
    {
        Dispose();
    }

    /// <summary>
    /// Unity用イベント
    /// </summary>
    public UnityEngine.Events.UnityAction<T> action;

    /// <summary>
    /// 値
    /// </summary>
    public T Value
    {
        get
        {
            return data;
        }
        set
        {
            data = value;
            Notice();
        }
    }

    private void Notice()
    {
        if (action != null)
            action(data);
    }

    public void Dispose()
    {
        action = null;
    }
}