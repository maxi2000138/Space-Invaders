using UnityEngine;

namespace Scenes
{
    public class GameFactory : IGameFactory
    {
        private Transform _playerSpawnPoint;
        private readonly GameObject[] _enemiesSpawns;


        public GameFactory()
        {
            _playerSpawnPoint = GameObject.FindGameObjectWithTag(Pathes.PlayerSpawnPointTag).gameObject.transform;
            _enemiesSpawns = GameObject.FindGameObjectsWithTag(Pathes.EnemySpawnPointTag);
        }

        public void InstantiatePlayer() => 
            Instantiate(_playerSpawnPoint.position, Pathes.PlayerPath);

        public void InstantiateHUD() => 
            Instantiate(Pathes.HudPath);

        public void InsantiateEnemies()
        {
            foreach (GameObject spawnPoint in _enemiesSpawns)
            {
                Instantiate(spawnPoint.transform.position, Pathes.EnemyPath);
            }
        }


        private void Instantiate(Vector3 position, string path) => 
            Object.Instantiate(Resources.Load(path), position, Quaternion.identity);

        private void Instantiate(string path) => 
            Object.Instantiate(Resources.Load(path));
    }
}