using Enemy;
using UnityEngine;


namespace Bullet
{
    public class BulletBehaviour : MonoBehaviour
    {
        [Header("Bullet components")]
        [SerializeField] private Rigidbody2D _bulletRigidbody;
        [Header("Bullet characteristics")]
        [SerializeField]private float _bulletSpeed = 1f;

        private void FixedUpdate()
        {  
            _bulletRigidbody.MovePosition(transform.position + (Vector3.up *_bulletSpeed));
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent(out EnemyBehaviour enemy))
            {
                enemy.Kill();
                Destroy(this.gameObject);
            }
        }

        void CheckDistanceToBounce()
        {
            
        }
        
        
    }
}

