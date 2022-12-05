using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStartPoint : MonoBehaviour
{
    private void Awake()
    {
        GameObject go = new GameObject();
        go.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 10));
        go.transform.parent = transform;
    }
}
