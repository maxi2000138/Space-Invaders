using Infrastructure.Services;
using UnityEngine;

public interface IScreenCharacteristicsService : IService
{
    float Width { get; }
    float Height { get; }
    Vector3 StartPosition { get; }
    Vector3 EndPosition { get; }
    void Construct();
}