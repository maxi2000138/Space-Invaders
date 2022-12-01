using System;
using UnityEngine;

[Serializable]
public class EnemyData
{
    public Vector3 _position;
    public EnemyTypeId _typeId;

    public EnemyData(Vector3 position, EnemyTypeId typeId)
    {
        _position = position;
        _typeId = typeId;
    }
}