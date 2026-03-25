using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] List<string> scenesToLoad;
    private void Awake()
    {
#if !UNITY_EDITOR
        foreach (string _sceneName in scenesToLoad)
        {
            SceneManager.LoadScene(_sceneName,LoadSceneMode.Additive);
        }
#endif
    }
}
