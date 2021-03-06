﻿#region File Description
//********************************************************************
//    file name		: ScenarioManager.cs
//    infomation	: シナリオマネージャー
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
#endregion

/// <summary>
/// シナリオマネージャー
/// </summary>
[RequireComponent(typeof(TextController))]
public class ScenarioManager : SingletonMonoBehaviourForever<ScenarioManager>
{

    /// <summary>
    /// Metaで割り当てておく必要はない
    /// UpdateLinesでenabled = falseになる
    /// enabledにして、UpdateLines(fileName)で起動する
    /// </summary>
	public string LoadFileName;

    private string[] m_scenarios;
    private int m_currentLine = 0;
    private bool m_isCallPreload = false;

    private TextController m_textController;
    private CommandController m_commandController;

    void RequestNextLine()
    {
        var currentText = m_scenarios[m_currentLine];

        m_textController.SetNextLine(CommandProcess(currentText));
        m_currentLine++;
        m_isCallPreload = false;
    }

    static public void UpdateLines(string fileName)
    {
        Instance._UpdateLines(fileName);
    }

    void _UpdateLines(string fileName)
    {
        var scenarioText = Resources.Load<TextAsset>("Scenario/" + fileName);

        if (scenarioText == null)
        {
            Debug.LogError("シナリオファイルが見つかりませんでした");
            Debug.LogError("ScenarioManagerを無効化します");
            enabled = false;
            return;
        }
        m_scenarios = scenarioText.text.Split(new string[] { "@br" }, System.StringSplitOptions.None);
        m_currentLine = 0;

        LoadFileName = fileName;

        Resources.UnloadAsset(scenarioText);
    }

    private string CommandProcess(string line)
    {
        var lineReader = new StringReader(line);
        var lineBuilder = new StringBuilder();
        var text = string.Empty;
        while ((text = lineReader.ReadLine()) != null)
        {
            var commentCharacterCount = text.IndexOf("//");
            if (commentCharacterCount != -1)
            {
                text = text.Substring(0, commentCharacterCount);
            }

            if (!string.IsNullOrEmpty(text))
            {
                if (text[0] == '@' && m_commandController.LoadCommand(text))
                    continue;
                lineBuilder.AppendLine(text);
            }
        }

        return lineBuilder.ToString();
    }

    #region UNITY_CALLBACK

    void Start()
    {
        m_textController = GetComponent<TextController>();
        m_commandController = GetComponent<CommandController>();

        _UpdateLines(LoadFileName);
        RequestNextLine();
    }

    void Update()
    {
        if (m_textController.IsCompleteDisplayText)
        {
            if (m_currentLine < m_scenarios.Length)
            {
                if (!m_isCallPreload)
                {
                    m_commandController.PreloadCommand(m_scenarios[m_currentLine]);
                    m_isCallPreload = true;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    RequestNextLine();
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_textController.ForceCompleteDisplayText();
            }
        }
    }

    #endregion
}