#region File Description
//********************************************************************
//    file name		: TouchHandler.cs
//    infomation	: 
//********************************************************************
#endregion

#region Using Statements
using UnityEngine;
#endregion

/// <summary>
/// Tap検知
/// </summary>
public class TouchHandler : MonoBehaviour
{
    const float m_Distance = 10; // Rayの届く距離

    void Update()
    {
        // タッチされたとき
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collition2d = Physics2D.OverlapPoint(tapPoint);
            if (collition2d)
            {
                RaycastHit2D hitObject = Physics2D.Raycast(tapPoint, -Vector2.up);
                if (hitObject)
                {
                    //Debug.Log("hit object " + hitObject.collider.gameObject.name);
                    GameObject selectedGameObject = hitObject.collider.gameObject;
                    TouchControl target = selectedGameObject.GetComponent(typeof(TouchControl)) as TouchControl;
                    if (target != null)
                    {
                        target.TapDown(ref hitObject);
                    }
                }
            }
        }
        // 指を離したとき
        else if (Input.GetMouseButtonUp(0))
        {
            Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collition2d = Physics2D.OverlapPoint(tapPoint);
            if (collition2d)
            {
                RaycastHit2D hitObject = Physics2D.Raycast(tapPoint, -Vector2.up);
                if (hitObject)
                {
                    //Debug.Log("hit object " + hitObject.collider.gameObject.name);
                    GameObject selectedGameObject = hitObject.collider.gameObject;
                    TouchControl target = selectedGameObject.GetComponent(typeof(TouchControl)) as TouchControl;
                    if (target != null)
                    {
                        target.TapUp(ref hitObject);
                    }
                }
            }
        }
    }

}