using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelService : ILoadLevelService
{
    private readonly ICoroutineRunner _coroutineRunner;

    public LoadLevelService(ICoroutineRunner coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;
    }
    
    public void LoadLevel(string sceneName, Action onLoadLevel = null)
    {
        _coroutineRunner.StartCoroutine(Load(sceneName,onLoadLevel));
    }

    private IEnumerator Load(string sceneName, Action onLoadLevel)
    {
        if (SceneManager.GetActiveScene().name == sceneName)
        {
            onLoadLevel?.Invoke();
            yield break;
        }

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!loadOperation.isDone)
        {
            yield return null;
        }
        
        onLoadLevel?.Invoke();

    }
}