using System;
using System.Collections.Generic;
using Infrastructure.Services;
using UnityEngine;
using Object = System.Object;

public class PoolMono
{
    private bool _autoExpand;
    private Transform _container;
    private List<GameObject> _pool;
    private EnemyStaticData _enemyStaticData;

    public PoolMono(Transform container, int size, bool autoExpand) {
        _pool = new List<GameObject>(new GameObject[size]);
        _container = container;
        _autoExpand = autoExpand;
        
        for(int i = 0; i < size; i++)
        {
            _pool[i] = CreateAndReturnElement(false);
        }
    }
    
    private GameObject CreateAndReturnElement(bool isActiveByDefault)
    {
        GameObject element = new GameObject();
        element.transform.SetParent(_container);
        element.SetActive(isActiveByDefault);
        return element;
    }

    public void TurnOffElement(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
    

    public bool CheckAndGetFreeElement(out GameObject element)
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (!_pool[i].activeSelf)
            {
                element = _pool[i];
                element.SetActive(true);
                return true;
            }
        }

        if (_autoExpand)
        {
            element = CreateAndReturnElement(true);
            _pool.Add(element);
            return true;
        }

        element = null;
        return false;
    }
}