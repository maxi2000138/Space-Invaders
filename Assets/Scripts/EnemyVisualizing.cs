using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EnemyVisualizing : MonoBehaviour
{
    public List<GameObject> _enemiesObjects = new List<GameObject>();

    public List<EnemyData> _enemies;

    public Dictionary<EnemyTypeId, Color32> _colors = new Dictionary<EnemyTypeId, Color32>()
    {
        [EnemyTypeId.WhiteEnemy] = Color.white,
    };

    public float _sphereRadius = 0.3f;

    public bool _isObjectsCreated = false;

    public int _orderNumber = 0;
}
