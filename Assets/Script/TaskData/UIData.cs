#region File Description
//********************************************************************
//    file name		: UIData.cs
//    infomation	: 
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
#endregion

/// <summary>
/// 
/// </summary>
class UIData : TaskData
{
    #region Init
    public UIData(int index)
        : base(index)
    {
    }
    #endregion Init

    public override string Name
    {
        get { return "UI"; }
    }

    protected override int ZPos
    {
        get { return -1; }
    }

    protected override void ActionChangeSequence(SQID value)
    {
    }

    public override void Ready()
    {
        Transform tComponen = UnityEngine.Object.Instantiate(FloorManager.Instance.UIPrefab, new Vector3(0, 0, 0), Quaternion.identity) as Transform;
        tComponen.gameObject.name = Name;
        UIControl taskCtl = tComponen.gameObject.GetComponent(typeof(UIControl)) as UIControl;

        //ここでFloorのTaskListをなめて表示する内容を確定する

        base.Ready();
    }

    static public UIData Instance
    {
        get
        {
            foreach (TaskData tData in FloorManager.TaskDataList.Values)
                if (tData is UIData) return tData as UIData;
            return null;
        }
    }

    static internal void VisiblePeaceState(int TableNo)
    {
        Instance._VisiblePeaceState(TableNo);
    }

    void _VisiblePeaceState(int TableNo)
    {
        // とりあえずTextでも表示するか
        throw new System.NotImplementedException();
    }

    static internal void InVisiblePeaceState(int TableNo)
    {
        Instance._InVisiblePeaceState(TableNo);
    }

    void _InVisiblePeaceState(int TableNo)
    {
        // とりあえずTextでも表示するか
        throw new System.NotImplementedException();
    }
}