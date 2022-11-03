using UnityEngine;

public interface IGameFactory : IService
{
    void InstantiatePlayer();
    void InstantiateHUD();
}