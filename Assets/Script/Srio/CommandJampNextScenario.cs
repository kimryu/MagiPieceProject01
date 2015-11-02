#region File Description
//********************************************************************
//    file name		: CommandJumpNextScenario.cs
//    infomation	: [ScenarioManager]シナリオを切り替えるコマンド
//********************************************************************
#endregion

#region Using Statements
using System.Collections.Generic;
#endregion

/// <summary>
/// [ScenarioManager]シナリオを切り替えるコマンド
/// </summary>
public class CommandJumpNextScenario : ICommand 
{
	public string Tag {
		get {	return "jump"; }
	}
 
	public void Command (Dictionary<string, string> command)
	{
		//var scenario = ScenarioManager.Instance;
		var fileName = command["fileName"];
        ScenarioManager.UpdateLines(fileName);
	}
}