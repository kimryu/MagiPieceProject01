#region File Description
//********************************************************************
//    file name		: NanatoData.cs
//    infomation	: 
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
#endregion

/// <summary>
/// Nanato
/// </summary>
public class NanatoData : PlyData
{
    public override string Name { get { return "Nanato"; } }

    public NanatoData(int index, int x, int y) : base(index, x, y) { }

    public override void Ready()
    {
        Transform tComponen = UnityEngine.Object.Instantiate(FloorManager.Instance.NanatoPrefab, new Vector3(X, Y, ZPos), Quaternion.identity) as Transform;
        tComponen.gameObject.name = Name;
        PlyUnitControl taskCtl = tComponen.gameObject.GetComponent(typeof(PlyUnitControl)) as PlyUnitControl;
        //taskCtl.Index = this.Index;

        taskCtl._OnCheckLiftUpOK = new CheckHandler(OnCheckLiftUpOK);
        taskCtl._OnReportDragDrop = new ReportDropHandler(OnDragDrop);

        base.Ready();
    }

    protected override void ActionChangeSequence(SQID value)
    {
    }
}