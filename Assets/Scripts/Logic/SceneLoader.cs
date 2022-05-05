using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Zenject;

namespace Logic
{
  public class SceneLoader
  {
    private CoroutineRunner _coroutineRunner;

    [Inject]
    public void Setup(CoroutineRunner coroutineRunner)
    {
      _coroutineRunner = coroutineRunner;
      Debug.Log("SceneLoader");
    }

    //загрузчик сцены и колбэк

    public void Load(string name, Action onLoaded = null)
    {
      _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
    }

    private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
    {
      if (SceneManager.GetActiveScene().name == nextScene)
      {
        onLoaded?.Invoke();
        yield break;
      }

      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);
      while (!waitNextScene.isDone) 
        yield return null;
      
      onLoaded?.Invoke();
    }
  }
}