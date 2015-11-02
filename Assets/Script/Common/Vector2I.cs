#region File Description
//********************************************************************
//    file name		: Vector2I.cs
//    infomation	: 2次元座標(整数型)
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
#endregion

/// <summary>
/// 2次元座標(整数型)
/// </summary>
public class Vector2I
{
    #region Member
    /// <summary>
    /// 
    /// </summary>
    public int X = -1;

    /// <summary>
    /// 
    /// </summary>
    public int Y = -1;
    #endregion Member

    public Vector2I(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public Vector2I(Vector3 Pos)
    {
        this.X = (int)(Pos.x);
        this.Y = (int)(Pos.y);
    }
    public Vector2I(Vector2 Pos)
    {
        this.X = (int)(Pos.x);
        this.Y = (int)(Pos.y);
    }

    #region operator
    public static bool operator == (Vector2I objA, Vector2I objB)
    {
        if ((object)objB == null)
        {
            if ((object)objA == null) return true;
            else return false;
        }

        bool bResult = (objA.X == objB.X && objA.Y == objB.Y);
        return bResult;
    }

    public static bool operator !=(Vector2I objA, Vector2I objB)
    {
        if ((object)objB == null)
        {
            if ((object)objA == null) return false;
            else return true;
        }

        bool bResult = (objA.X != objB.X || objA.Y != objB.Y);
        return bResult;
    }
    #endregion operator

    /// <summary>
    /// 同じオブジェクトをCloneする
    /// </summary>
    /// <returns></returns>
    internal Vector2I Clone()
    {
        return new Vector2I(this.X, this.Y);
    }

    #region override
    public override string ToString()
    {
        return "(" + X.ToString() + "," + Y.ToString() + ")";
    }

    public override bool Equals(object obj)
    {
        Vector2I temp = obj as Vector2I;
        bool bResult = (temp.X == this.X && temp.Y == this.Y);
        return bResult;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    #endregion override

    /// <summary>
    /// 無効POSを返す
    /// </summary>
    public static Vector2I InvalidPos { get { return new Vector2I(-999,-999);} }

    /// <summary>
    /// 無効POSか
    /// </summary>
    /// <returns></returns>
    public bool IsInvalidPos()
    {
        if (X == -999 || Y == -999)
        {
            return true;
        }

        return false;
    }
}
