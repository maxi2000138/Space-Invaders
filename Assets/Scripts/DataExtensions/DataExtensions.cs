using UnityEngine;

public static class DataExtensions
{
    public static string Serialize<T>(this T item) =>
        JsonUtility.ToJson(item);

    public static T Deserialize<T>(this string item) =>
        JsonUtility.FromJson<T>(item);

    public static Vector3Data AsVector3Data(this Vector3 vector) =>
        new Vector3Data(vector.x, vector.y, vector.z);

    public static int AsTypeIdData(this EnemyTypeId id) =>
        (int)id;

}
