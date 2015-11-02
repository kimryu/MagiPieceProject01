#region File Description
//********************************************************************
//    file name		: CommandChangeScene.cs
//    infomation	: [ScenarioManager]シーンを切り替えるコマンド
//********************************************************************
#endregion

#region Using Statements
using System.Collections.Generic;
using UnityEngine;
#endregion

/// <summary>
/// [ScenarioManager]シーンを切り替えるコマンド
/// </summary>
public class CommandChangeScene : ICommand
{
    public string Tag
    {
        get { return "scene"; }
    }


    public void Command(Dictionary<string, string> command)
    {
        var sceneName = command["name"];

        Application.LoadLevel(sceneName);
    }
}

