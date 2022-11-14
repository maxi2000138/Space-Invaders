using System;
using System.Collections;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;


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

        private bool _shouldShoot;

        private InputAction.CallbackContext ctxer;

        private void Start()
        {
            _input = new OwnInput();
            _input.Player.Enable();
            _input.Player.Fire.started += ctx => StartCoroutine(AutoShoot(ctx));



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
                bullet.transform.rotation = transform.rotation;
            }
            else
            {
                throw new Exception("Error to create bullet!");
            }

        }

        IEnumerator AutoShoot(InputAction.CallbackContext ctx)
        {
            while(true)
            {
                yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
                if (ctx.canceled)
                    yield break;
                Shoot(ctx);
                
            }
        }
    }
    
}
