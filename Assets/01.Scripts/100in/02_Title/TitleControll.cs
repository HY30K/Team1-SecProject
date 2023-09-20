using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class TitleControll : MonoBehaviour
{
    [SerializeField] private List<TextMeshPro> _text = new List<TextMeshPro>();
    [SerializeField] private TextMeshProUGUI _toych;
    [SerializeField] private string NextScene;
    private Animator _ani;
    private Color _cr;
    private int _toychCount = 0;

    private void Awake()
    {
        _ani = GetComponent<Animator>();
    }
    private void OnText()
    {
        if (_toychCount == 0)
        {
            _toychCount++;
        }
        for (int i = 0; i < _text.Count; i++)
        {
            _text[i].fontMaterial.DOFloat(0, ShaderUtilities.ID_FaceDilate, 3);
        }
        Invoke("OnFadeText", 3);
    }

    private void OnFadeText()
    {
        _toych.enabled = true;
        StartCoroutine(TextFadeOut());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(_toychCount == 0)
            {
                _ani.speed = 200;
            }
            else if(_toychCount==1)
            {
                ScenesLoadManager.Instance.FadeOut(() => SceneManager.LoadScene(NextScene));
            }
        }
    }

    private IEnumerator TextFadeOut()
    {
        _cr = _toych.color;
        while (_toych.color.a <= 1)
        {
            _cr.a += Time.deltaTime / 2f;
            _toych.color = _cr;
            yield return null;
        }
        StartCoroutine(TextFadeIn());
    }
    private IEnumerator TextFadeIn()
    {
        _cr = _toych.color;
        while (_toych.color.a >= 0)
        {
            _cr.a -= Time.deltaTime / 2f;
            _toych.color = _cr;
            yield return null;
        }
        StartCoroutine(TextFadeOut());
    }
}
