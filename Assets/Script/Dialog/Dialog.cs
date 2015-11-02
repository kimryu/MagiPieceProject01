#region File Description
//********************************************************************
//    file name		: Dialog.cs
//    infomation	: モーダル/モーダレスダイアログ
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
#endregion

/// <summary>
/// モーダル/モーダレスダイアログ、ゲーム内時間Pause状態の設定が行えるダイアログクラス
/// 時間停止/入力無効をしたくない例外的なオブジェクト
/// [例外オブジェクト] 
/// カメラ
/// ライト
/// イベントシステム
/// GameObject.Nameに”(non_timescale_object)”が含まれるもの
/// 
/// [制限]
/// Canvasを利用しているのでUGUI 4.6でないとUIとGameの2つに分離できない。つまり旧GUI(?)やOnGUIを使用している場合は対応できない。
/// カメラ、ライト、イベントシステムが一番上の階層にいることが前提。
/// 時間停止をしたくないオブジェクトは、一番上の階層に配置し、かつ名前の中に「(non_timescale_object)」を含めなくてはいけない。
/// なぜかパーティクルは停止しない、これは意図していません。
/// </summary>
public abstract class Dialog : MonoBehaviour
{
    /// <summary>
    /// ポーズ状態
    /// </summary>
    TimePauser _pauser = null;

    /// <summary>
    /// UI状態
    /// </summary>
    private bool _isUIEnable = true;

    /// <summary>
    /// ゲーム内時間Pause状態
    /// </summary>
    private bool _isGameEnable = true;

    /// <summary>
    /// モーダルダイアログ状態の取得または設定します
    /// </summary>
    /// <value></value>
    public bool IsUIEnable
    {
        get { return _isUIEnable; }
        set { _isUIEnable = value; }
    }

    /// <summary>
    /// 起動時のゲーム内時間Pause状態を取得または設定します
    /// </summary>
    /// <value></value>
    public bool IsGameEnable
    {
        get { return _isGameEnable; }
        set { _isGameEnable = value; }
    }

    /// <summary>
    /// 閉じるイベント
    /// </summary>
    public void OnCloseButton()
    {
        Destroy(gameObject);
    }

    #region UNITY_DELEGATE
    /// <summary>
    /// Start
    /// </summary>
    void Start()
    {
        // ダイアログ開始
        _pauser = new TimePauser(gameObject);
        if (IsUIEnable != true)
        {
            _pauser.PauseUI();
        }
        if (IsGameEnable != true)
        {
            _pauser.PauseGame();
        }
    }

    /// <summary>
    /// OnDestory
    /// </summary>
    void OnDisable()
    {
        // ダイアログ終了
        _pauser.Resume();
    }
    #endregion UNITY_DELEGATE

}