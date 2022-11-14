using System;
using UnityEngine;


public static class PlayingFieldBorders
{
    public static readonly float LeftBoundX;
    public static readonly float RightBoundX;
    public static readonly float UpBoundY;
    public static readonly float DownBoundY;

    static PlayingFieldBorders()
    {
        Camera camera = Camera.main;
        if (camera == null)
        {
            throw new Exception("Camera null!\nCan't find bounds");
        }
        
        else
        {
            LeftBoundX = camera.ScreenToWorldPoint(Vector3.zero).x;
            RightBoundX = camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
            UpBoundY = camera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
            DownBoundY = camera.ScreenToWorldPoint(Vector3.zero).y;
        }
    }
}