#region File Description
//********************************************************************
//    file name		: SingletonMonoBehaviourScene.cs
//    infomation	: MonoBehaviour専用シングルトン(シーン切替時に破棄)
//********************************************************************
#endregion

#region Using Statements
using System;
using UnityEngine;
#endregion

/// <summary>
/// Scene切替で消去するエセシングルトン
/// OnDestroy()で消去
/// </summary>
/// <typeparam name="T">MonoBehaviour対象クラス</typeparam>
public abstract class SingletonMonoBehaviourScene<T> : MonoBehaviour where T : SingletonMonoBehaviourScene<T>
{
    protected static readonly string[] findTags =
	{
		"GameController",
	};

    protected static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {

                Type type = typeof(T);

                foreach (var tag in findTags)
                {
                    GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);

                    for (int j = 0; j < objs.Length; j++)
                    {
                        instance = (T)objs[j].GetComponent(type);
                        if (instance != null)
                            return instance;
                    }
                }

                Debug.LogWarning(string.Format("{0} is not found", type.Name));
            }

            return instance;
        }
    }

    protected bool CheckInstance()
    {
        if (instance == null)
        {
            instance = (T)this;
            return true;
        }
        else if (Instance == this)
        {
            return true;
        }

        Destroy(this);
        return false;
    }

    #region UNITY_DELEGATE
    virtual protected void Awake()
    {
        CheckInstance();
    }

    virtual protected void OnDestroy()
    {
        if (instance == null)
        {
            instance = null;
        }
    }
    #endregion
}