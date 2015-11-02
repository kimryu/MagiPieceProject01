#region File Description
//********************************************************************
//    file name		: TextureresourceManager.cs
//    infomation	: [ScenarioManager]テキスチャをAssertから動的にロード
//********************************************************************
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using UnityEngine;
#endregion

/// <summary>
/// [ScenarioManager]テキスチャをAssertから動的にロード
/// </summary>
public class TextureresourceManager : SingletonMonoBehaviourForever<TextureresourceManager>
{
	public int Max = 5;
	private List<Texture2D> m_textureList = new List<Texture2D>();

    // サウンド再生のためのゲームオブジェクト
    GameObject _object = null;

    public static void Mark(string textureName)
	{
		var tex = Instance.m_textureList.Find( item => item.name == textureName );
		if( tex != null )
		{
			Instance.m_textureList.Remove(tex);
			Instance.m_textureList.Add(tex);
		}
	}
	
	public static Texture Load(string textureName)
	{
		var tex = Instance.m_textureList.Find( item => item.name == textureName );
		if( tex == null ){
			tex = Instance.m_textureList[0];
			var res = Resources.Load<TextAsset>("Image/" + textureName);
			tex.LoadImage(res.bytes);

			tex.name = textureName;
			Resources.UnloadAsset(res);
		}
		
		Instance.m_textureList.Remove(tex);
		Instance.m_textureList.Add(tex);
		
		return tex;
	}

    #region UNITY_DELEGATE
    protected override void Awake()
    {
        // シングルトン処理
        base.Awake();

        if (_object == null)
        {
            // GameObjectがなければ探す
            _object = GameObject.Find("(non_timescale_object)TextureresourceManager");

            if (_object == null) Debug.LogException(new Exception("GameObject.Find Error"));

            // 破棄しないようにする
            GameObject.DontDestroyOnLoad(_object);            
        }
    }

    void OnEnable()
	{
		for(int i=0; i<Instance.Max; i++)
		{
			var tex2D = new Texture2D(1,1, TextureFormat.ARGB32, false);
			tex2D.Apply(false, true);
			Instance.m_textureList.Add(tex2D);
		}
	}
	
	void OnDisable()
	{
		foreach( var tex in m_textureList )
		{
			Destroy (tex);
		}
		m_textureList.Clear();
	}
	
	#endregion
}