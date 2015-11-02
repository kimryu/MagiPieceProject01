#region File Description
//********************************************************************
//    file name		: Postion.cs
//    infomation	: 座標管理
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
#endregion

/// <summary>
/// 座標管理
/// </summary>
public class Postion
{
    ComponentAccesser Access;

    public Postion(GameObject gameObject) { Access = new ComponentAccesser(gameObject); }

    /// <summary>
    /// 
    /// </summary>
    public float X
    {
        get
        {
            return Access.Transform.position.x;
        }
        set
        {
            Vector3 now = Access.Transform.position;
            Access.Transform.position = new Vector3(value, now.y, now.z);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public float Y
    {
        get
        {
            return Access.Transform.position.y;
        }
        set
        {
            Vector3 now = Access.Transform.position;
            Access.Transform.position = new Vector3(now.x, value, now.z);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public float Z
    {
        get
        {
            return Access.Transform.position.z;
        }
        set
        {
            Vector3 now = Access.Transform.position;
            Access.Transform.position = new Vector3(now.x, now.y, value);
        }
    }

    /// <summary>
    /// 描写上のX座標
    /// </summary>
    public int IntX
    {
        get
        {
            float fX = Access.Transform.position.x;
            int iX = (int)fX; //超えない整数？

            if (fX >= ((float)iX + 0.5f)) return (iX + 1);
            else return iX;
        }
        set
        {
            Vector3 now = Access.Transform.position;
            Access.Transform.position = new Vector3((float)value, now.y, now.z);
        }
    }
    /// <summary>
    /// 描写上のY座標
    /// </summary>
    public int IntY
    {
        get
        {
            float fY = Access.Transform.position.y;
            int iY = (int)fY; //超えない整数？

            if (fY >= ((float)iY + 0.5f)) return (iY + 1);
            else return iY;
        }
        set
        {
            Vector3 now = Access.Transform.position;
            Access.Transform.position = new Vector3(now.x, (float)value, now.z);
        }
    }
    /// <summary>
    /// 描写上のZ座標
    /// </summary>
    public int IntZ
    {
        set
        {
            Vector3 now = Access.Transform.position;
            Access.Transform.position = new Vector3(now.x, now.y, (float)value);
        }
    }

    /// <summary>
    /// X,Y軸を整数値に変更
    /// 
    /// </summary>
    public void ToInteger()
    {
        SetPos((float)(IntX), (float)(IntY));
    }

    /// <summary>
    /// X,Y座標の設定
    /// </summary>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    public void SetPos(float X, float Y)
    {
        Vector3 now = Access.Transform.position;
        Access.Transform.position = new Vector3(X, Y, now.z);
    }

    /// <summary>
    /// Vector2Iの取得
    /// </summary>
    public Vector2I Vector2I
    {
        get { return new Vector2I(IntX, IntY); }
    }

    /// <summary>
    /// Vector3の取得
    /// </summary>
    public Vector3 Vector3
    {
        get { return Access.Transform.position; }
    }
}