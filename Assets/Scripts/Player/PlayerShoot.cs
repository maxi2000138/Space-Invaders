using System;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private BulletPool _bulletPool;
        
        [Header("Shoot characteristics")]
        [SerializeField] private float offset;

        private IGameFactory _gameFactory;
        private OwnInput _input;

        private void Start()
        {
            _input = new OwnInput();
            _input.Player.Enable();
            _input.Player.Fire.performed += Shoot;

            _bulletPool = GameObject.FindObjectOfType<BulletPool>();
        }
    
    

        public void Shoot(InputAction.CallbackContext context)
        {
            Vector3 position = transform.position;
            Vector3 bulletSpawnPosition =
                new Vector3(position.x, position.y + offset, position.z);

            GameObject bullet;
            
            if (_bulletPool.Pool.CheckAndGetFreeElement(out bullet))
            {
                bullet.transform.position = bulletSpawnPosition;
            }
            else
            {
                throw new Exception("Error to create bullet!");
            }

        }
    }
    
}
