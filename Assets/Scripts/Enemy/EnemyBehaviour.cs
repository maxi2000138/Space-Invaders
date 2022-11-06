using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyBehaviour : MonoBehaviour
    {
        public event Action<EnemyBehaviour> OnEnemyDie;
    
        public void Kill()
        {
            OnEnemyDie?.Invoke(this);
        }
    }
    
}
