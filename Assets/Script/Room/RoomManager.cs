#region File Description
//********************************************************************
//    file name		: RoomManager.cs
//    infomation	: Room管理
//********************************************************************
#endregion

#region Using Statements
using System;
using UnityEngine;
using System.Collections.Generic;
#endregion

/// <summary>
/// Room管理
/// </summary>
public class RoomManager : SingletonMonoBehaviourScene<RoomManager>
{
    // 領域データ数値定義
    public const int EmptyGridNum = 1;          // なにもない       移動可能領域
    public const int MoveAbleGridNum = 2;       // Click可能で　    移動可能領域(踏めるもの)
    public const int ClickAbleGridNum = 3;      // Click可能で　    移動不可領域(ClickEventもの)
    public const int ClickEnableGridNum = 4;    // Click不可能で    移動不可領域(踏めないもの)
    public const int NullGridNum = 9;           // Click不可能で    移動不可領域(いけない場所)


    #region Access
    protected RoomPlayer Player
    {
        get
        {
            GameObject GmObj = GameObject.Find(RoomPlayer.PlayerName);
            RoomPlayer obj = GmObj.GetComponent(typeof(RoomPlayer)) as RoomPlayer;
            if (obj == null)
            {
                throw new Exception("Error");
            }
            return obj;
        }
    }
    #endregion Access

    #region Member
    /// <summary>
    /// 領域データ
    /// </summary>
    int[,] Grids { get; set; }
    Vector2I GoalPos { get; set; }
    Vector2I NextPos { get; set; }
    Vector2I NowPos { get; set; }
    List<Vector2I> PathList { get; set; }
    List<EventPart> EventList { get; set; }

    /// <summary>
    /// 移動中
    /// </summary>
    bool NowMoveing = false;
    #endregion Member

    #region init
    private void InitEvent()
    {
        // Event系を登録する
        // Event系はStart時に即非表示にするもの
        // MetaにEventの詳細を登録する

        EventList = new List<EventPart>();

        GameObject[] Objs = GameObject.FindGameObjectsWithTag("RoomEvent");

        foreach (GameObject obj in Objs)
        {
            RoomFootEvent fevent = obj.GetComponent(typeof(RoomFootEvent)) as RoomFootEvent;
            if (fevent != null)
            {
                EventPart EventData = new EventPart();
                EventData.Pos = fevent._Postion.Vector2I;
                EventData.SubPos = EventData.Pos.Clone();

                EventData.SrioFileName = fevent.SrioFileName;
                EventData.Type = fevent.Type;

                EventList.Add(EventData);
            }

            RoomClickEvent cevent = obj.GetComponent(typeof(RoomClickEvent)) as RoomClickEvent;
            if (cevent != null)
            {
                EventPart EventData = new EventPart();
                EventData.Pos = cevent._Postion.Vector2I;
                EventData.SubPos = cevent.GetSubPos();

                EventData.SrioFileName = cevent.SrioFileName;
                EventData.Type = cevent.Type;

                EventList.Add(EventData);
            }
        }
    }

    private void InitGrid()
    {
        // RoomGridよりGrid情報取得
        GameObject GObj = GameObject.FindGameObjectWithTag("RoomGrids");
        RoomGrids rGrids = GObj.GetComponent(typeof(RoomGrids)) as RoomGrids;
        Grids = rGrids.GetGridData();
            
        // Playerの位置を現在位置へ
        NowPos = Player._Postion.Vector2I;

        // ほか必要なやつを探し出し登録していく
        GameObject[] Objs = GameObject.FindGameObjectsWithTag("RoomObject");

        foreach (GameObject obj in Objs)
        {
            RoomObject rObject = obj.GetComponent(typeof(RoomObject)) as RoomObject;
            int gX = rObject._Postion.IntX;
            int gY = rObject._Postion.IntY;

            Grids[gX, gY] = rObject.GridDataNum;
        }
    }
    #endregion init

    /// <summary>
    /// 移動発生のクリックか
    /// </summary>
    void CheckMove()
    {
        Vector2I Pos = GetMouseEnablePos();
        if (Pos != null)
        {
            GoalPos = Pos.Clone();
            Vector2I GoalPos2 = Pos.Clone();

            EventPart findEvent = EventList.Find(i => i.Pos == GoalPos);

            if (findEvent != null)
            {
                if (findEvent.IsClickEvent())
                {
                    GoalPos2 = findEvent.SubPos.Clone();
                }
            }

            // ルート検索
            List<Vector2I> _PathList;

            bool result = A_Star.SearchPath(Grids, NowPos, GoalPos2, out _PathList);

            if (result == false) return;

            if (_PathList.Count <= 0) return;

            PathList = _PathList;

            if (result)// ルートあり、移動する
            {
                NextPos = PathList[0];
                PathList.RemoveAt(0);

                // 移動指示
                Player.ReqAutoMoving(NextPos.X, NextPos.Y, new ReportEndHandler(OnMoveEnd));

                NowMoveing = true;
            }
        }
    }

    /// <summary>
    /// Event発生のクリックか
    /// </summary>
    bool CheckEvent()
    {
        Vector2I Pos = GetMouseEnablePos();
        if (Pos != null)
        {
            // 現在位置をクリック
            EventPart findEvent = null;

            findEvent = EventList.Find(i => (i.Pos == Pos) && (i.SubPos == NowPos));

            if (findEvent != null)
            {
                if (findEvent.IsClickEvent())
                {
                    // ClickEventをクリック
                    ClickEvent(findEvent);
                    return true;
                }
            }
            return false;
        }
        return false;
    }

    /// <summary>
    /// 移動完了イベント
    /// </summary>
    void OnMoveEnd()
    {
        NowPos = NextPos;

        // 到着か
        if (NowPos == GoalPos)
        {
            NowMoveing = false;

            EventPart findEvent = EventList.Find(i => i.Pos == NowPos);

            if (findEvent != null)
            {
                if (findEvent.IsFootEvent())
                {
                    FootEvent(findEvent);
                }
            }
            return;
        }

        // ClickEventのSubPosに到着か
        if (PathList.Count <= 0)
        {
            NowMoveing = false;

            EventPart findEvent = EventList.Find(i => (i.Pos == GoalPos) && (i.SubPos == NowPos));

            if (findEvent != null)
            {
                if (findEvent.IsClickEvent())
                {
                    ClickEvent(findEvent);
                }
            }
            return;
        }

        NextPos = PathList[0];
        PathList.RemoveAt(0);

        // 移動指示
        Player.ReqAutoMoving(NextPos.X, NextPos.Y, new ReportEndHandler(OnMoveEnd));

        NowMoveing = true;
    }

    /// <summary>
    /// マウスの有効座標を取得
    /// </summary>
    /// <returns></returns>
    Vector2I GetMouseEnablePos()
    {
        Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tapPoint = new Vector2(tapPoint.x + 0.5f, tapPoint.y + 0.5f);//Offset
        // 整数座標系に変換
        Vector2I Pos = new Vector2I(tapPoint);

        if (IsGridsOutOfRenge(Pos.X, Pos.Y)) return null;

        if (Grids[Pos.X, Pos.Y] > ClickAbleGridNum)
            return null;

        return Pos;
    }

    private bool IsGridsOutOfRenge(int x, int y)
    {
        if (x < 0 || y < 0) return true;

        if(Grids.GetLength(0) <= x) return true;

        if (Grids.GetLength(1) <= y) return true;

        return false;
    }

    /// <summary>
    /// Clickイベント実行
    /// </summary>
    /// <param name="findEvent"></param>
    void ClickEvent(EventPart findEvent)
    {
        // PrefabよりSrioをインスタンス化する
        // Canvasは非表示にしておく
        // ScenarioManagerにファイル名登録
        // （ScenarioManagerに終了イベントを登録）
        // 非表示のまま実行する(TextなどがあるごとにVisibleに変更する)
        // IF分岐のコマンドを作る(TRUEで別シナリオに飛ぶ)(NIFも作る)
        //=======================
        //MsgBoxも対応する (Messageとイベントを登録)
        Debug.Log("ClickEvent");
    }

    /// <summary>
    /// Footイベント実行
    /// </summary>
    /// <param name="findEvent"></param>
    void FootEvent(EventPart findEvent)
    {
        Debug.Log("FootEvent");

        if (findEvent.Type == EventType.Floor1)
        {
            Application.LoadLevel("Floor");
        }
    }

    #region UNITY_DELEGATE
    /// <summary>
    /// Start
    /// </summary>
    void Start()
    {
        InitGrid();
        InitEvent();
    }

    /// <summary>
    /// Update
    /// </summary>
    void Update()
    {
        // マウス入力で左クリックをした瞬間
        if (NowMoveing == false && Input.GetMouseButtonDown(0))
        {
            bool result = CheckEvent();

            if (result == false) CheckMove();
        }

        // デバッグでキー入力中はGridsなどの情報が表示できるようになりたい
    }
    #endregion UNITY_DELEGATE
}
