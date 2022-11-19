using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;

        private void Awake()
        {
            GameBootstrapper[] bootstrappers = GameObject.FindObjectsOfType<GameBootstrapper>();

            if (bootstrappers.Length > 1)
            {
                Destroy(gameObject);
                return;
            }

            _game = new Game(this);

            DontDestroyOnLoad(this);
        }
    
    
    }    
}
