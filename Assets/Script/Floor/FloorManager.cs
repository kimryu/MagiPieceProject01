#region File Description
//********************************************************************
//    file name		: FloorManager.cs
//    infomation	: Floor管理
//********************************************************************
#endregion

#region Using Statements
using System;
using UnityEngine;
using System.Collections.Generic;
#endregion

/// <summary>
/// Update順管理
/// Task管理
/// </summary>
public class FloorManager : SingletonMonoBehaviourScene<FloorManager>
{
    public const string GameObjectName = "FloorGmObj";

    #region Inspector
    [SerializeField]
    public Transform GridPrefab;

    [SerializeField]
    public Transform NanatoPrefab;

    [SerializeField]
    public Transform Enm01Prefab;

    [SerializeField]
    public Transform MagiPeace01Prefab;

    [SerializeField]
    public Transform TableMagiPeace01Prefab;

    [SerializeField]
    public Transform PanelPrefab;

    [SerializeField]
    public Transform UIPrefab;

    [SerializeField]
    public Transform FloorNameViewPrefab;
    #endregion Inspector

    #region Member
    /// <summary>
    /// TaskData
    /// </summary>
    static internal Dictionary<int, TaskData> TaskDataList 
    { 
        get { return Instance.m_TaskDataList; } 
    }
    Dictionary<int, TaskData> m_TaskDataList;

    /// <summary>
    /// MyTID
    /// </summary>
    private const int m_MyTID = 0;

    /// <summary>
    /// TID
    /// </summary>
    private int m_NewTID = 0;//１から開始

    /// <summary>
    /// 削除すべきタスクあり
    /// </summary>
    static internal bool Telegram_HaveDestroyTask 
    { 
        get { return Instance.m_HaveDestroyTask; } 
        set { Instance.m_HaveDestroyTask = value; } 
    }
    bool m_HaveDestroyTask;
    #endregion Member

    #region Initialization
    #region UNITY_DELEGATE
    protected override void Awake()
    {
        SequenceManager _SequenceManager = new SequenceManager();
        _SequenceManager.Awake();
        base.Awake();
    }

    /// <summary>
    /// 最初のフレームのアップデート前
    /// </summary>
    void Start()
    {
        // SQM初期化
        SequenceManager.Initialize();

        if (SequenceManager.NowSQID.Value == SQID.新規開始)
        {
            m_TaskDataList = new Dictionary<int, TaskData>();
            m_NewTID = 0;

            int index;

            // 初期化処理
            // !!hoge:Test!!
            for (var y = 0; y < 4; y++)
            {
                for (var x = 0; x < 4; x++)
                {
                    index = GetNewTID();
                    m_TaskDataList.Add(index, new GridData(index, x, y));
                }
            }
            for (var y = 0; y < 4; y++)
            {
                for (var x = 0; x < 4; x++)
                {
                    index = GetNewTID();
                    m_TaskDataList.Add(index, new PanelData(index, x, y, 
                        (x==0 && y==0) ? PanelState.Open : PanelState.Close));//Plyの初期位置だけ空ける
                }
            }

            index = GetNewTID();
            m_TaskDataList.Add(index, new NanatoData(index, 0, 0));

            index = GetNewTID();
            m_TaskDataList.Add(index, new MagiPeaceData(index, 0, 0,MagiPeaceData.MagiPeaceType.MagiPeace01,0));

            index = GetNewTID();
            m_TaskDataList.Add(index, new EnmData(index, 3, 3, 0, EnmType.Enm01, true));//Hide

            index = GetNewTID();
            m_TaskDataList.Add(index, new EnmData(index, 3, 2, 0, EnmType.Enm01, false));//Active

            // 演出家
            index = GetNewTID();
            m_TaskDataList.Add(index, new ProducerData(index));

            index = GetNewTID();
            m_TaskDataList.Add(index, new UIData(index));

            // 各Taskの準備
            foreach(TaskData tData in m_TaskDataList.Values)
                tData.Ready();


            SequenceManager.SendMessage(MessageType.FloorInitEnd, -1);
        }
        else
        {
            // 復元処理
        }
    }
    #endregion UNITY_DELEGATE

    /// <summary>
    /// 新しいTIDを取得する
    /// </summary>
    /// <returns></returns>
    int GetNewTID() { return m_NewTID++; }

    #endregion Initialization

    #region UNITY_DELEGATE
    void Update(){}

    /// <summary>
    /// Update()後にフレームごとに一度呼び出されます
    /// </summary>
    void LateUpdate()
    {
        // TaskDataの整理
        if (Telegram_HaveDestroyTask)
        {
            int[] keycoll = new int[m_TaskDataList.Keys.Count];
            m_TaskDataList.Keys.CopyTo(keycoll, 0);
            foreach (int key in keycoll)
            {
                if (m_TaskDataList[key].IsDestroyOK)
                {
                    m_TaskDataList[key].ReqDestroy();
                    m_TaskDataList.Remove(key);
                }
            }

            Telegram_HaveDestroyTask = false;
        }

        // SQMの更新
        SequenceManager.ManualUpdate();
    }

    override protected void OnDestroy()
    {
        // SQMを消去する
        SequenceManager.Instance.OnDestroy();
    }
    #endregion UNITY_DELEGATE

    #region TaskDataList
    /// <summary>
    /// TypeよりIndexを取得
    /// </summary>
    public int GetIndex(Type type)
    {
        return Instance._GetIndex(type);
    }

    /// <summary>
    /// TypeよりIndexを取得
    /// </summary>
    int _GetIndex(Type type)
    {
        foreach (TaskData data in TaskDataList.Values)
        {
            if (data.GetType() == type)
            {
                return data.Index;
            }
        }

        return -1;
    }
    #endregion TaskDataList
}

