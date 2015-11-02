#region File Description
//********************************************************************
//    file name		: CommandUpdateImage.cs
//    infomation	: [ScenarioManager]コマンドインターフェイス
//********************************************************************
#endregion

#region Using Statements
using System.Collections.Generic;
#endregion

/// <summary>
/// [ScenarioManager]コマンドインターフェイス
/// </summary>
public interface ICommand 
{
	string Tag{get;}
	void Command(Dictionary<string, string> command);
}

/// <summary>
/// [ScenarioManager]事前に呼ばれるコマンド
/// </summary>
public interface IPreCommand 
{
	string Tag{get;}
	void PreCommand(Dictionary<string, string> command);
}