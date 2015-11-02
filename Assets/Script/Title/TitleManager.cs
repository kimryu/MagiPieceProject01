#region File Description
//********************************************************************
//    file name		: TitleManager.cs
//    infomation	: 
//********************************************************************
#endregion

#region Using Statements
using System;
using UnityEngine;
using System.Collections.Generic;
#endregion

/// <summary>
/// TitleSceneの管理
/// uGUIのEventを使用する
/// </summary>
public class TitleManager : SingletonMonoBehaviourScene<TitleManager>
{
    public const string MyGameObjectName = "TitleGmObj";

    /// <summary>
    /// 最初のフレームのアップデート前
    /// </summary>
    void Start()
    {
        SoundInit();

        // BGM再生開始
        Sound.PlayBgm("TitleBgm");
    }

    /// <summary>
    /// サウンドの読み込み
    /// </summary>
    void SoundInit()
    {
        // サウンドをロード
        // "bgm01"をロード。キーは"bgm"とする
        Sound.LoadBgm("TitleBgm", "game_maoudamashii_7_event44");
        /*
        // "damage"をロード。キーは"damage"とする
        Sound.LoadSe("damage", "damage");
        // "destroy"をロード。キーは"destroy"とする
        Sound.LoadSe("destroy", "destroy");
        */
    }

    /// 破棄
    override protected void OnDestroy()
    {
        // BGM停止
        Sound.StopBgm();

        base.OnDestroy();
    }

    /// <summary>
    /// uGUI Event用
    /// </summary>
    public void OnDebugBtnClick()
    {
        Application.LoadLevel("Opening");
    }
}
