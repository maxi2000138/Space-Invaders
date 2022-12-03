using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(LevelStaticData))]
public class EnemyDataCustomEditor : Editor
{
    private EnemyVisualizing _visualizer; 
    private bool _isObjectsDestroy = false;
    private string _index;
    

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (_isObjectsDestroy)
        {
            if (!GUILayout.Button("Visualize"))
            {
                return;
            }

            _isObjectsDestroy = false;
        }
        
        LevelStaticData data = (LevelStaticData)target;
        if (_visualizer == null)
        {
            _visualizer = FindObjectOfType<EnemyVisualizing>();
            if (_visualizer == null)
            {
                GameObject go = new GameObject();
                go.name = "Visualizer";
                _visualizer = go.AddComponent<EnemyVisualizing>();    
            }
        }
        
        _visualizer._enemies = data.Enemies;
        VisualizeAllEnemies(data);

        if (GUILayout.Button("Create enemy"))
        {
            EnemyData enemyData = new EnemyData(Vector3.zero, EnemyTypeId.WhiteEnemy);
            CreateEnemy(enemyData);
            VisualizeAndSetupEnemy(enemyData);
        }
        

        if (GUILayout.Button("Destroy visualizing"))
        {
            if(!_visualizer._isObjectsCreated)
                return;
            
            _isObjectsDestroy = true;
            
            for (int i = 0; i < _visualizer._enemiesObjects.Count; i++)
            {
                DestroyImmediate(_visualizer._enemiesObjects[i]);
            }
            DestroyImmediate(_visualizer.gameObject);

            _visualizer._orderNumber = 0;
        }

        if (GUILayout.Button("Save enemy layout"))
        {
            for (int i = 0; i < _visualizer._enemiesObjects.Count; i++)
            {
                _visualizer._enemies[i]._position = _visualizer._enemiesObjects[i].transform.position;
            }
                Debug.Log("Layout saved!");
        }
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Destroy element: ",GUILayout.Width(EditorGUIUtility.currentViewWidth*0.7f)))
        {
            int index = Int32.Parse(_index);
            DestroyImmediate(_visualizer._enemiesObjects[index]);
            _visualizer._enemiesObjects.RemoveAt(index);
            _visualizer._enemies.RemoveAt(index);
        }

        _index = EditorGUILayout.TextField("",_index,GUILayout.Width(EditorGUIUtility.currentViewWidth*0.2f));
        EditorGUILayout.EndHorizontal();
    }

    private void VisualizeAllEnemies(LevelStaticData data)
    {
            if (_visualizer._isObjectsCreated)
                return;

            _visualizer._isObjectsCreated = true;
            
            foreach (EnemyData enemyData in data.Enemies)
            {
                VisualizeAndSetupEnemy(enemyData);
            }
    }

    private void CreateEnemy(EnemyData enemyData)
    {
        _visualizer._enemies.Add(enemyData);
    }

    private void VisualizeAndSetupEnemy(EnemyData enemyData)
    {
        GameObject go = new GameObject();
        _visualizer._enemiesObjects.Add(go);
        go.transform.position = enemyData._position;
        go.AddComponent<DrawSphereInGizmos>().Construct(go.transform, _visualizer._sphereRadius, GiveColor(enemyData));
        go.name = "Element " + _visualizer._orderNumber++;
    }

    private Color32 GiveColor(EnemyData enemyData)
    {
        return _visualizer._colors[enemyData._typeId];
    }
}