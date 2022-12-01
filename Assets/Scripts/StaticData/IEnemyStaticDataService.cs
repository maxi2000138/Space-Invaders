using Infrastructure.Services;

public interface IEnemyStaticDataService : IService
{
    void LoadEnemies();
    EnemyStaticData GiveEnemy(EnemyTypeId id);
}