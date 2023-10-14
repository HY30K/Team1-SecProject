using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class ScenesLoadManager : MonoSingleton<ScenesLoadManager>
{
    private Image _image = null;
    private Color _cr;
    private float _fadeCool = 2; public float FadeCool { get => _fadeCool; set => _fadeCool = value; }
    private void Awake()
    {
        _image = transform.Find("FadeImage").GetComponent<Image>();
    }
    private void Start()
    {
        FadeIn(null);

    }
    public void FadeIn(Action action)
    {
        StartCoroutine(fadeIn(action));
    }
    private IEnumerator fadeIn(Action action)
    {
        _cr = _image.color;
        while (_image.color.a >= 0)
        {
            _cr.a -= Time.deltaTime / _fadeCool;
            _image.color = _cr;
            yield return null;
        }
        action?.Invoke();
    }
    public void FadeOut(Action action)
    {
        StartCoroutine(fadeOut(action));
      
    }
    private IEnumerator fadeOut(Action action)
    {
        _cr = _image.color;
        while (_image.color.a <= 1)
        {
            _cr.a += Time.deltaTime / _fadeCool;
            _image.color = _cr;
            yield return null;
        }
        Debug.Log("페이드아웃데스");
        Debug.Log(_image.color);
        action?.Invoke();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FadeIn(() => { });
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
