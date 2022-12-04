using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Create Enemy")]
public class EnemyStaticData : ScriptableObject
{
    [Space(10)]
    public EnemyTypeId TypeId;
    [Space(10)]
    [Range(0,50)]
    public int HP;
    [Space(10)]
    public GameObject Prefab;
}
