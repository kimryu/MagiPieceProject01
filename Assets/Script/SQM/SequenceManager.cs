#region File Description
//********************************************************************
//    file name		: SequenceManager.cs
//    infomation	: Floor時シーケンス管理
//********************************************************************
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using UnityEngine;
#endregion

/// <summary>
/// Floor時シーケンス管理
/// Taskへメッセージ送信
/// </summary>
public partial class SequenceManager : SingletonScene<SequenceManager>
{
    #region Member

    private List<MessageInfo> MessageList = new List<MessageInfo>();

    /// <summary>
    /// シーケンスID
    /// 変更Action付き
    /// </summary>
    public static NotificationObject<SQID> NowSQID { get { return Instance._NowSQID; } }
    NotificationObject<SQID> _NowSQID;

    /// <summary>
    /// NextシーケンスID
    /// 変更Action付き
    /// </summary>
    public NotificationObject<SQID> NextSQID { get { return Instance._NextSQID; } }
    NotificationObject<SQID> _NextSQID;
    #endregion Member

    #region Initialize
    /// <summary>
    /// 初期化処理
    /// </summary>
    static internal void Initialize()
    {
        Instance._Initialize();
    }

    void _Initialize()
    {
        _NowSQID = new NotificationObject<SQID>(SQID.新規開始);
        _NextSQID = new NotificationObject<SQID>(SQID.新規開始);
        //WaitTaskTID.Clear();
        MessageList.Clear();
    }
    #endregion Initialize

    #region Update
    /// <summary>
    /// 手動Update
    /// </summary>
    static internal void ManualUpdate()
    {
        Instance._ManualUpdate();
    }

    void _ManualUpdate()
    {
        while (MessageList.Count != 0)
        {
            if (NowSQID.Value != NextSQID.Value)
            {
                //危険！
                Debug.LogError("二重変化注意！");
            }

            MessageInfo msg = MessageList[0];
            MessageList.RemoveAt(0);

            DoMessage(msg);
        }

        if (NowSQID.Value != NextSQID.Value)
        {
            DoChangeSequence();
        }
    }


    /// <summary>
    /// 外部タスクのMeeageより次のSQIDを決定する
    /// 状態遷移の管理
    /// </summary>
    /// <param name="msg"></param>
    private void DoMessage(MessageInfo msg)
    {
        bool ChangeSeq = false;
        switch (msg.Type)
        {
            case MessageType.FloorInitEnd:
                if (NowSQID.Value == SQID.新規開始)
                {
                    NextSQID.Value = SQID.FloorNameView;
                    ChangeSeq = true;
                }
                break;

            case MessageType.ViewFloorNameEnd:
                if (NowSQID.Value == SQID.FloorNameView)
                {
                    NextSQID.Value = SQID.PlyInputWait;
                    ChangeSeq = true;
                }
                break;

            case MessageType.PlyDDStart:
                if (NowSQID.Value == SQID.PlyInputWait)
                {
                    NextSQID.Value = SQID.PlyDD;
                    ChangeSeq = true;
                } break;

            case MessageType.PlyDDCancel:
                if (NowSQID.Value == SQID.PlyDD)
                {
                    NextSQID.Value = SQID.PlyInputWait;
                    ChangeSeq = true;
                } break;

            case MessageType.PlyDDEnd:
                if (NowSQID.Value == SQID.PlyDD)
                {
                    NextSQID.Value = SQID.EnmAutoMove;
                    ChangeSeq = true;
                } break;

            case MessageType.TablePeaceDDStart:
                if (NowSQID.Value == SQID.PlyInputWait)
                {
                    NextSQID.Value = SQID.TablePeaceDD;
                    ChangeSeq = true;
                } break;

            case MessageType.TablePeaceDDCancel:
                if (NowSQID.Value == SQID.TablePeaceDD)
                {
                    NextSQID.Value = SQID.PlyInputWait;
                    ChangeSeq = true;
                } break;

            case MessageType.TablePeaceDDEnd:
                if (NowSQID.Value == SQID.TablePeaceDD)
                {
                    NextSQID.Value = SQID.EnmAutoMove;
                    ChangeSeq = true;
                } break;

            case MessageType.EnmNonMove:
                if (NowSQID.Value == SQID.EnmAutoMove)
                {
                    NextSQID.Value = SQID.PlyInputWait;
                    ChangeSeq = true;
                } break;

            case MessageType.EnmAutoMoveEnd:
                if (NowSQID.Value == SQID.EnmAutoMove)
                {
                    NextSQID.Value = SQID.PlyInputWait;
                    ChangeSeq = true;
                } break;

            case MessageType.ClickPanelOpenStart:
                if (NowSQID.Value == SQID.PlyInputWait)
                {
                    NextSQID.Value = SQID.PanelOpenNanato;
                    ChangeSeq = true;
                } break;

            case MessageType.PanelOpenEnd:
                if (NowSQID.Value == SQID.PanelOpenNanato)
                {
                    NextSQID.Value = SQID.EnmAutoMove;
                    ChangeSeq = true;
                } break;

            // 未設計
            case MessageType.PanelOpenStart:
            case MessageType.PanelCloseStart:
            case MessageType.PanelCloseEnd:
                {
                    Debug.Log("Message未実装" + msg.ToString());
                    ChangeSeq = true;
                }
                break;

            // 処理しないもの
            case MessageType.ViewFloorNameStart:
            case MessageType.EnmAuroMoveStart:
                {
                    Debug.Log("Message無視" + msg.ToString());
                    ChangeSeq = true;
                }
                break;
        }

        if (ChangeSeq == false)
        {
            Debug.LogError("DoMessage Error!" + msg.ToString());
        }
    }
    #endregion Update

    #region Sequence制御
    /// <summary>
    /// シーケンスの遷移処理
    /// 各タスクに指示を出す
    /// </summary>
    private void DoChangeSequence()
    {
        switch (NextSQID.Value)
        {
            case SQID.EnmAutoMove:
                int Index = EnmData.GetNextMoveEnmIndex();
                if (Index == -1)
                {
                    //Skip処理
                    SendMessage(MessageType.EnmNonMove, -1);
                    break;
                }

                EnmData.Telegram_EnmAutoMoveSelectIndex = Index;

                break;
            default:
                break;
        }

        Debug.Log(_NowSQID.Value.ToString() + "->" + _NextSQID.Value.ToString());

        // 更新
        // 各タスクにイベントが飛ぶ
        _NowSQID.Value = _NextSQID.Value;
    }
/*
    /// <summary>
    /// 次のSQIDの指定
    /// </summary>
    static internal SQID GetNextSQID()
    {
        return Instance._GetNextSQID();
    }

    /// <summary>
    /// 状況により次のSQIDを決定する
    /// </summary>
    /// <returns></returns>
    SQID _GetNextSQID()
    {
        switch (NowSQID.Value)
        {
            case SQID.新規開始:
                return SQID.Floor名表示;

            case SQID.Floor名表示:
                return SQID.Srio開始;

            case SQID.PlyInput待ち:
                return SQID.EnmAutoMove;

            case SQID.EnmAutoMove:
                return SQID.PlyInput待ち;

            default:
                throw new NotImplementedException();
        }
    }
*/
    #endregion Sequence制御
/*
 * 微妙
    #region 外部通信
    /// <summary>
    /// 各Taskが遷移を停止させるよう通知
    /// </summary>
    /// <param name="Index"></param>
    static internal void ReportWaitTask(int Index)
    {
        Instance._ReportWaitTask(Index);
    }

    void _ReportWaitTask(int Index)
    {
        WaitTaskTID.Add(Index);
    }

    /// <summary>
    /// 各Taskが完了を通知
    /// </summary>
    /// <param name="Index"></param>
    static internal void ReportEnd(int Index)
    {
        Instance._ReportEnd(Index);
    }

    void _ReportEnd(int Index)
    {
        WaitTaskTID.Remove(Index);

        if (WaitTaskTID.Count == 0)
        {
            NextSQID.Value = GetNextSQID();
        }
    }
    #endregion 外部通信
 */
    #region 外部通信
    static internal void SendMessage(MessageType Type, int TaskIndex)
    {
        Instance._SendMessage(Type, TaskIndex);
    }
    void _SendMessage(MessageType Type, int TaskIndex)
    {
        MessageList.Add(new MessageInfo(Type, TaskIndex));
    } 
    #endregion 外部通信
}