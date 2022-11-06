using Infrastructure.Services;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {
        [Header("Shoot characteristics")]
        [SerializeField]private float offset;

        private IGameFactory _gameFactory;
        private OwnInput _input;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            _input = new OwnInput();
            _input.Player.Enable();
            _input.Player.Fire.performed += Shoot;
        }
    
    

        public void Shoot(InputAction.CallbackContext context)
        {
            Vector3 bulletSpawnPosition =
                new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        
            _gameFactory.InstantiateBullet(bulletSpawnPosition);
        }
    }
    
}
