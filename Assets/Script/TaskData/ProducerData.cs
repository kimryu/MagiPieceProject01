#region File Description
//********************************************************************
//    file name		: ProducerData.cs
//    infomation	: 
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
#endregion

/// <summary>
/// 演出タスク
/// </summary>
class ProducerData : TaskData
{
    public override string Name
    {
        get { throw new System.Exception(); }
    }

    protected override int ZPos
    {
        get { throw new System.Exception(); }
    }

    public ProducerData(int index)
        : base(index)
    {
    }

    protected override void ActionChangeSequence(SQID value)
    {
        if (value == SQID.FloorNameView)
        {
            ViewFloorName();

        }
    }

    public override void ReqDestroy()
    {
        // 破棄不要
        //// GMObjの破棄
        //GameObject.Destroy(GmObj);

        // SQMのイベントを削除
        SequenceManager.NowSQID.action -= ActionChangeSequence;
    }

    void ViewFloorName()
    {
        Vector3 DefPos = new Vector3(0, 0, -2);
        Transform tComponen = UnityEngine.Object.Instantiate(FloorManager.Instance.FloorNameViewPrefab, DefPos, Quaternion.identity) as Transform;
        tComponen.gameObject.name = "FloorNameView";

        FloorNameView _FloorNameView = tComponen.gameObject.GetComponent(typeof(FloorNameView)) as FloorNameView;
        _FloorNameView.FloorNo = 1;
        _FloorNameView.ReqView(new ReportEndHandler(OnViewFloorNameEnd));

        SequenceManager.SendMessage(MessageType.ViewFloorNameStart, Index);
    }

    void OnViewFloorNameEnd()
    {
        SequenceManager.SendMessage(MessageType.ViewFloorNameEnd, Index);
    }
}
