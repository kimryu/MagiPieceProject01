#region File Description
//********************************************************************
//    file name		: TaskControl.cs
//    infomation	: 
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
#endregion

/// <summary>
/// GmObjの管理
/// </summary>
public class TaskControl : MonoBehaviour
{
    #region Member
    public ComponentAccesser _Access;
    public Postion _Postion;
    #endregion

    //#region Member
    //public int Index { get; set; }
    //#endregion

    void Awake()
    {
        _Access = new ComponentAccesser(gameObject);
        _Postion = new Postion(gameObject);
    }

    internal virtual void MouseDown(){}
    internal virtual void Click(){}
    internal virtual void DragEnter(){}
    internal virtual void DragDrop(){}

    protected virtual void Update(){}
}
