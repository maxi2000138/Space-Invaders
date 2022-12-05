using Enemy;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
   public void DestroyEnemy(EnemyBehaviour enemy)
   {
      Destroy(enemy.gameObject);
   }
}
