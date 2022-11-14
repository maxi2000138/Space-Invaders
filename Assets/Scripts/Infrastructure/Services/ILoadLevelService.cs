using System;
using Infrastructure.Services;

public interface ILoadLevelService : IService
{
    void LoadLevel(string sceneName, Action onLoadLevel = null);
}
