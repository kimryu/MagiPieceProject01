#region File Description
//********************************************************************
//    file name		: TouchControl.cs
//    infomation	: 
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
#endregion

/// <summary>
/// Tap管理
/// TaskControlといっしょに配置
/// </summary>
public class TouchControl : MonoBehaviour
{
    private TaskControl Target
    {
        get
        {
            return GetComponent(typeof(TaskControl)) as TaskControl;
        }
    }

    private enum TouchControlState
    {
        Free,
        Click,
        DragEnter,
    }

    private TouchControlState m_State = TouchControlState.Free;
    private float m_TapTime = 0.0f;
    private const float m_CatchTime = 0.2f;//500msec

    public void TapDown(ref RaycastHit2D hit)
    {
        m_State = TouchControlState.Click;

        if (Target != null)
        {
            Target.MouseDown();
        }
    }

    public void TapUp(ref RaycastHit2D hit)
    {
        if (m_State == TouchControlState.Click)
        {
            if (Target != null)
            {
                Target.Click();
            }
        }
        else if (m_State == TouchControlState.DragEnter)
        {
            Debug.Log("DragDrop");
            if (Target != null)
            {
                Target.DragDrop();
            }
        }

        m_State = TouchControlState.Free;
        m_TapTime = 0.0f;
    }

    void Update()
    {
        if (m_State != TouchControlState.Free)
        {
            m_TapTime += Time.deltaTime;
        }

        if (m_State == TouchControlState.Click && m_TapTime >= m_CatchTime)
        {
            Debug.Log("DragEnter");
            m_State = TouchControlState.DragEnter;
            Target.DragEnter();
        }
    }
}