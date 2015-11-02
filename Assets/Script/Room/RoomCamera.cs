#region File Description
//********************************************************************
//    file name		: RoomCamera.cs
//    infomation	: Room内のCamera制御
//********************************************************************
#endregion

#region Using Statements
using System;
using UnityEngine;
using System.Collections.Generic;
#endregion

/// <summary>
/// Room内のCamera制御
/// </summary>
class RoomCamera : SingletonMonoBehaviourScene<RoomCamera>
{
    public GameObject player = null;

    Transform _transform;

    #region UNITY_DELEGATE
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤー追尾
        _transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z - 10.0f);
    }
    #endregion UNITY_DELEGATE
}