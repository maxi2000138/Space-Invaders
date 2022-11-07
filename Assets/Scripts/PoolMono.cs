using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolMono 
{
    private bool _autoExpand;
    private Transform _container;
    private List<GameObject> _pool;
    private Func<GameObject> _instantiateMethod;

    public PoolMono(Transform container, int size, bool autoExpand, Func<GameObject> instantiateMethod)
    {
        _pool = new List<GameObject>(new GameObject[size]);
        _container = container;
        _autoExpand = autoExpand;
        _instantiateMethod = instantiateMethod;
        
        for(int i = 0; i < size; i++)
        {
            _pool[i] = CreateAndReturnElement(false);
        }
    }
    
    private GameObject CreateAndReturnElement(bool isActiveByDefault)
    {
        GameObject element = _instantiateMethod();
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