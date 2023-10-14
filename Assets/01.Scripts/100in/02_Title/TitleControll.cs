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
    [SerializeField] GameObject StartBtn;
    private Animator _ani;
    private Color _cr;
    private int _toychCount = 0;
    private bool _isToych = false;
    private float _currentTime = 0;
    private void Awake()
    {
        _ani = GetComponent<Animator>();
    }
    public void OnText()
    {
        if (_toychCount == 0)
        {
            _toychCount++;
        }

        _text[0].fontMaterial.DOFloat(0, ShaderUtilities.ID_FaceDilate, 3);
        _text[1].fontMaterial.DOFloat(0, ShaderUtilities.ID_FaceDilate, 5);
        Invoke("OnFadeText", 3);
    }

    private void OnFadeText()
    {
        _toych.enabled = true;
        StartCoroutine(TextFadeOut());
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
    }

    public void ToychEvent()
    {

        if (_toychCount == 0&& _currentTime>1)
        {
            _ani.speed = 500;
        }
        else if (_toychCount == 1 && !_isToych&& _currentTime>1)
        {
            _isToych = true;
            Debug.Log("ÀÌ°Ô ¿ÖµÇ³ó");
            Debug.Log(_isToych);
            ScenesLoadManager.Instance.FadeOut(() => SceneManager.LoadScene(NextScene));
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
