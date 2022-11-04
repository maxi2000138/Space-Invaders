using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [Header("Bullet components")]
    [SerializeField] private Rigidbody2D _rb;
    [Header("Bullet characteristics")]
    [SerializeField]private float _bulletSpeed = 1f;

    private void FixedUpdate()
    {
        //transform.Translate(Vector3.up * _bulletSpeed);   
        _rb.MovePosition(transform.position + (Vector3.up *_bulletSpeed));
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.Deleter();
            Destroy(this.gameObject);
        }
    }
}
