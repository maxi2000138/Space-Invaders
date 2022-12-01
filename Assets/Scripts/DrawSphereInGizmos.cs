using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawSphereInGizmos : MonoBehaviour
{
    public Vector3 _center;
    public float _radius;
    public Color32 _color;

    public void Construct(Vector3 center, float radius, Color32 color)
    {
        _center = center;
        _radius = radius;
        _color = color;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawSphere(_center,_radius);
    }
}
