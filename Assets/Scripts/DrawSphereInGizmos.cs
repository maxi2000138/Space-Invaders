using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawSphereInGizmos : MonoBehaviour
{
    private Transform _objectTransform;
    private float _radius;
    private Color32 _color;

    public void Construct(Transform objectTransform, float radius, Color32 color)
    {
        _objectTransform = objectTransform;
        _radius = radius;
        _color = color;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = _color;  
        Gizmos.DrawSphere(_objectTransform.position,_radius);
    }
}
