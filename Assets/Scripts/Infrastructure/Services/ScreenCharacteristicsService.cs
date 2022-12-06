using UnityEngine;

public class ScreenCharacteristicsService : IScreenCharacteristicsService
{
    public float Width { get; private set; }
    public float Height { get; private set; }
    public Vector3 StartPosition { get; private set; }
    public Vector3 EndPosition { get; private set; }

    public void Construct()
    {
        StartPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 10));
        EndPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 10));
        Width = EndPosition.x - StartPosition.x;
        Height = EndPosition.y - StartPosition.y;
    }
}