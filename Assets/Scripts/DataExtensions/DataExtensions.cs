using UnityEngine;

public static class DataExtensions
{
    public static string Serialize<T>(this T item) =>
        JsonUtility.ToJson(item);

    public static T Deserialize<T>(this string item) =>
        JsonUtility.FromJson<T>(item);

}
