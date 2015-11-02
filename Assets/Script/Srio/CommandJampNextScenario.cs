#region File Description
//********************************************************************
//    file name		: CommandJumpNextScenario.cs
//    infomation	: [ScenarioManager]�V�i���I��؂�ւ���R�}���h
//********************************************************************
#endregion

#region Using Statements
using System.Collections.Generic;
#endregion

/// <summary>
/// [ScenarioManager]�V�i���I��؂�ւ���R�}���h
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