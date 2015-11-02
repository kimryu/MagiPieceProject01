#region File Description
//********************************************************************
//    file name		: GridData.cs
//    infomation	: 
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
#endregion

/// <summary>
/// Grid
/// </summary>
public class GridData : TaskData
{
    protected override int ZPos
    {
        get { return 0; }
    }

    protected override void ActionChangeSequence(SQID value)
    {
    }

    public override string Name { get { return string.Format("Grid" + "{0}{1}", X.ToString(), Y.ToString()); } }

    public int X { get; set; }
    public int Y { get; set; }

    public GridData(int index, int x, int y)
        : base(index)
    {
        this.X = x;
        this.Y = y;
    }

    public override void Ready()
    {
        Transform tComponen = UnityEngine.Object.Instantiate(FloorManager.Instance.GridPrefab, new Vector3(X, Y, ZPos), Quaternion.identity) as Transform;
        tComponen.gameObject.name = Name;
        TaskControl taskCtl = tComponen.gameObject.GetComponent(typeof(TaskControl)) as TaskControl;
        //taskCtl.Index = this.Index;

        base.Ready();
    }
}