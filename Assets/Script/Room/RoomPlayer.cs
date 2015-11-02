#region File Description
//********************************************************************
//    file name		: RoomPlayer.cs
//    infomation	: Room内のPlayer
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
using System.Collections;
using System;
#endregion

/// <summary>
/// Room内のPlayer
/// </summary>
public class RoomPlayer : MonoBehaviour 
{
    public static string PlayerName { get { return "Nanato"; } }

    #region Member
    public ComponentAccesser _Access;
    public Postion _Postion;

    ReportEndHandler _OnMoveEnd;
    #endregion

    void Awake()
    {
        _Access = new ComponentAccesser(gameObject);
        _Postion = new Postion(gameObject);

        _Postion.Z = CalcZPos();

        // 整数に変更
        _Postion.ToInteger();
    }
	
    float CalcZPos()
    {
        //-1より0.1ずつへらす
        float Z = -1.0f - (0.1f * _Postion.Y);

        return Z;
    }

    float CalcZPos(float Y)
    {
        //-1より0.1ずつへらす
        float Z = -1.0f - (0.1f * Y);

        return Z;
    }

    public virtual void ReqAutoMoving(int NextX, int NextY, ReportEndHandler onMoveEnd)
    {
        _OnMoveEnd = onMoveEnd;

        Hashtable table = new Hashtable();

        table.Add("x", NextX);
        table.Add("y", NextY);
        table.Add("z", CalcZPos((float)NextY));
        table.Add("time", 0.3f);

        table.Add("easetype", iTween.EaseType.spring);

        table.Add("onstart", "StartHandler");       // トゥイーン開始時にStartHandler()を呼ぶ
        table.Add("onstartparams", PlayerName + " Start");        // StartHandler()の引数に渡す値
        table.Add("onupdate", "UpdateHandler");     // トゥイーンを開始してから毎フレームUpdateHandler()を呼ぶ
        table.Add("onupdateparams", PlayerName + " Update");      // UpdateHandler()の引数に渡す値
        table.Add("oncomplete", "CompleteHandler"); // トゥイーン終了時にCompleteHandler()を呼ぶ
        table.Add("oncompleteparams", PlayerName + " Complete");  // CompleteHandler()の引数に渡す値

        iTween.MoveTo(gameObject, table);
    }

    private void StartHandler(string message)
    {
        Debug.Log(message);
    }

    private void UpdateHandler(string message)
    {
        //Debug.Log(message);
    }

    private void CompleteHandler(string message)
    {
        Debug.Log(message);

        if (_OnMoveEnd != null)
            _OnMoveEnd();
    }
}
