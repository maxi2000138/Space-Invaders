using System;
using System.Collections;
using Enemy;
using UnityEngine;

namespace Bullet
{
    public class BulletDestroyer : MonoBehaviour
    {
        private const float _frequencyToCheckDestroyDistance = 2f;
        private const float _destroyOffset = -1f;
        
        private void OnEnable()
        {
            StartCoroutine(CheckDistanceToDestroy());
        }

        private void OnDisable()
        {
            StopCoroutine(CheckDistanceToDestroy());
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent(out EnemyBehaviour enemy))
            {
                enemy.Kill();
                DestroyBullet();
            }
        }


        private IEnumerator CheckDistanceToDestroy()
        {
            while (true)
            {
                yield return new WaitForSeconds(1 / _frequencyToCheckDestroyDistance);
                if (transform.position.y + _destroyOffset > PlayingFieldBorders.UpBoundY)
                {
                    DestroyBullet();
                }
            }
        }

        private void DestroyBullet() =>
            gameObject.SetActive(false);
    }
}