using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeenManager : MonoBehaviour
{
    public static SeenManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void GoSeen(string name)
    {
        //ScenesLoadManager.Instance.FadeOut(() => SceneManager.LoadScene(name));
        //SceneManager.LoadScene(name);
    }
}