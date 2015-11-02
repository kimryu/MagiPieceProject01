#region File Description
//********************************************************************
//    file name		: SingletonMonoBehaviourForever.cs
//    infomation	: MonoBehaviour��p�V���O���g��(�i�v�ۑ�)
//********************************************************************
#endregion

#region Using Statements
using System;
using UnityEngine;
#endregion

/// <summary>
/// MonoBehaviour��p�V���O���g��(�i�v�ۑ�)
/// 
/// </summary>
/// <typeparam name="T">MonoBehaviour�ΏۃN���X</typeparam>
public abstract class SingletonMonoBehaviourForever<T> : MonoBehaviour where T : SingletonMonoBehaviourForever<T>
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
    #endregion
}