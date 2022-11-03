using UnityEngine;

namespace Scenes
{
    public class GameFactory : IGameFactory
    {
        private Transform _playerSpawnPoint;
        

        public GameFactory()
        {
            _playerSpawnPoint = GameObject.FindGameObjectWithTag(Pathes._playerSpawnPointTag).gameObject.transform;
        }

        public void InstantiatePlayer() => 
            Instantiate(_playerSpawnPoint.position, Pathes._playerPath);

        public void InstantiateHUD() => 
            Instantiate(Pathes._hudPath);


        private void Instantiate(Vector3 position, string path) => 
            Object.Instantiate(Resources.Load(path), position, Quaternion.identity);

        private void Instantiate(string path) => 
            Object.Instantiate(Resources.Load(path));
    }
}