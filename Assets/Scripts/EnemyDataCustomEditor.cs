using Infrastructure.Services;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelStaticData))]
public class EnemyDataCustomEditor : Editor
{
    private IGameFactory _gameFactory = AllServices.Container.Single<IGameFactory>();
    private IEnemyStaticDataService _dataService = AllServices.Container.Single<IEnemyStaticDataService>();
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LevelStaticData data = (LevelStaticData)target;

        if (GUILayout.Button("Create enemy"))
        {
            EnemyData enemyData = new EnemyData(Vector3.zero, EnemyTypeId.WhiteEnemy);
            data.Enemies.Add(enemyData);
        }
        

        if (GUILayout.Button("Visualize points"))
        {
            foreach (EnemyData enemyData in data.Enemies)
            {
                GameObject go = new GameObject();
                go.AddComponent<DrawSphereInGizmos>().Construct(enemyData._position,1, GiveColor(enemyData));
                go.transform.position = enemyData._position;
            }
        }

    }
    
    private Color32 GiveColor(EnemyData enemyData)
    {
        return _dataService.GiveEnemy(enemyData._typeId).GizmosColor;
    }
}
