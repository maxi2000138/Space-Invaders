using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tools : MonoBehaviour
{
    [MenuItem("Tools/Clear all PlayerPrefs")]
    public static void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
