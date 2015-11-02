#region File Description
//********************************************************************
//    file name		: SingletonScene.cs
//    infomation	: シングルトン(手動削除)
//********************************************************************
#endregion

#region Using Statements
using System;
#endregion

/// <summary>
/// Scene切替で消去するエセシングルトン(手動削除)
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SingletonScene<T> where T : SingletonScene<T>
{
    protected static T instance;

    public static T Instance
    {
        get
        {
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
        else if (instance == this)
        {
            return true;
        }

        return false;
    }

    internal void Awake()
	{
		CheckInstance();
	}

    internal void OnDestroy()
    {
        instance = null;
    }
}
