#region File Description
//********************************************************************
//    file name		: ComponentAccesser.cs
//    infomation	: 標準的なGetComponentへのアクセサー
//********************************************************************
#endregion

#region Using Statements
using System;
using UnityEngine;
#endregion

/// <summary>
/// 標準的なGetComponentへのアクセサー
/// </summary>
public class ComponentAccesser
{
    GameObject _GameObject;

    public ComponentAccesser(GameObject gameObject) { _GameObject = gameObject; }

    public Transform Transform
    {
        get
        {
            Transform obj = _GameObject.GetComponent(typeof(Transform)) as Transform;
            if (obj == null)
            {
                throw new Exception("Error");
            }
            return obj;
        }
    }

    public SpriteRenderer Renderer
    {
        get
        {
            SpriteRenderer obj = _GameObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
            if (obj == null)
            {
                throw new Exception("Error");
            }
            return obj;
        }
    }

    public Rigidbody2D Rigidbody2D
    {
        get
        {
            Rigidbody2D obj = _GameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
            if (obj == null)
            {
                throw new Exception("Error");
            }
            return obj;
        }
    }

    public Animator Animator
    {
        get
        {
            Animator obj = _GameObject.GetComponent(typeof(Animator)) as Animator;
            if (obj == null)
            {
                throw new Exception("Error");
            }
            return obj;
        }
    }
}
