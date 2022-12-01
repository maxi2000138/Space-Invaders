using Enemy;
using Infrastructure;
using Infrastructure.Services;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [Header("Pool settings")]
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private int _enemyPoolDefaultSize;
    [SerializeField] private bool _enemyPoolAutoExpand;  

    public PoolMono Pool;
    
    private IGameFactory _gameFactory;

    private void Start()
    {
        _gameFactory = AllServices.Container.Single<IGameFactory>();
        
        Pool = new PoolMono(_enemyContainer, _enemyPoolDefaultSize, _enemyPoolAutoExpand);

        foreach (GameObject spawn in _gameFactory.EnemiesSpawns)
        {
            if (Pool.CheckAndGetFreeElement(out GameObject enemy))
            {
                enemy.transform.position = spawn.transform.position;
            }
        }
        
                
    }

    private void SetupLevel()
    {   
        
    }
}
