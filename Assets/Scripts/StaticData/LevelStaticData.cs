using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Create Level")]
public class LevelStaticData : ScriptableObject
{
        public int LevelNumber;
        public List<EnemyData> Enemies;
}