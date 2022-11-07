using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure.Services;
using Player;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [Header("Pool settings")]
    [SerializeField] private Transform _bulletContainer;
    [SerializeField] private int _bulletPoolDefaultSize;
    [SerializeField] private bool _isBulletPoolAutoExpand;

    public PoolMono Pool;
    
    private IGameFactory _gameFactory;


    private void Start()
    {
        _gameFactory = AllServices.Container.Single<IGameFactory>();
        
        Pool = new PoolMono(_bulletContainer, _bulletPoolDefaultSize, _isBulletPoolAutoExpand,
            _gameFactory.InstantiateBullet);
        
    }
    

}
