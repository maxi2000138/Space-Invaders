using System;
using System.Collections;
using Infrastructure.Services;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;


namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {
        [Header("Shoot characteristics")]
        [SerializeField] private float _offset;
        [SerializeField] private float _minBulletSpawnSpeed;
        [SerializeField] private float _maxBulletSpawnSpeed;
        
        [Header("Components")]
        private IGameFactory _gameFactory;
        private OwnInput _input;
        private bool _shouldShoot;

        private InputAction.CallbackContext ctxer;
        private IEnumerator ShootCoroutine;

        private void Start()
        {
            _input = new OwnInput();
            _input.Player.Enable();
            _input.Player.Fire.started += ctx =>
            {
                ShootCoroutine = AutoShoot(ctx);
                StartCoroutine(ShootCoroutine);
            };
            _input.Player.Fire.canceled += ctx => StopCoroutine(ShootCoroutine);

        }

        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
    
    

        public void Shoot(InputAction.CallbackContext context)
        {
            Vector3 position = transform.position;
            Vector3 bulletSpawnPosition =
                new Vector3(position.x, position.y + _offset, position.z);

            GameObject bullet = _gameFactory.InstantiateBullet();
            bullet.transform.position = bulletSpawnPosition;
            bullet.transform.rotation = transform.rotation;
        }

        IEnumerator AutoShoot(InputAction.CallbackContext ctx)
        {
            while(true)
            {
                yield return new WaitForSeconds(Random.Range(_minBulletSpawnSpeed, _maxBulletSpawnSpeed));
                Shoot(ctx);
                
            }
        }
    }
    
}
