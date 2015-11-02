#region File Description
//********************************************************************
//    file name		: CommandUpdateImage.cs
//    infomation	: [ScenarioManager]描写を切り替えるコマンド
//********************************************************************
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using UnityEngine;
#endregion

/// <summary>
/// [ScenarioManager]描写を切り替えるコマンド
/// </summary>
public class CommandUpdateImage : ICommand, IPreCommand 
{
	public string Tag {
		get { return "img"; }
	}

	public void PreCommand (Dictionary<string, string> command)
	{
		var fileName = command["image"];
		TextureresourceManager.Load(fileName);
	}

	public void Command (Dictionary<string, string> command)
	{
		var fileName = command["image"];
		var objectName = command["name"];

		var obj = Array.Find<GameObject>( GameObject.FindGameObjectsWithTag("Layer") ,item => item.name == objectName);
		obj.GetComponent<Layer>().UpdateTexture( TextureresourceManager.Load(fileName) );
	}
}