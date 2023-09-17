using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
public class MouseCurser : MonoBehaviour
{
    [SerializeField] private UnityEvent _onItemWait;
    [SerializeField] private UnityEvent _offItemWait;

    private Camera _cam;
    private Vector2 _dir = Vector3.zero;
    private SpriteRenderer _spriteRenderer;
    private WeaponStatusSO _weaponStatusSO;
    private UIDocument _dot;
    private VisualElement _curser;
    private void Awake()
    {
        _cam = Camera.main;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //_dot = GetComponent<UIDocument>();
        //_curser = _dot.rootVisualElement.Q<VisualElement>("Mouse");
    }

    private void Update()
    {
        //SetPos();
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(MouseDirSet(), Vector2.zero);
            if (hit.collider != null)
            {
                _onItemWait?.Invoke();
                Debug.Log("µÇ³ª?");
            }
        }
        if (Input.GetMouseButtonDown(1))
        {

        }
    }

    private void SetPos()
    {
        //Vector2 newPosition = RuntimePanelUtils.CameraTransformWorldToPanel(_curser.panel, _cam.ScreenToWorldPoint(Input.mousePosition), _cam);
        //_curser.transform.position = newPosition.WithNewX(newPosition.x - _curser.layout.width / 2);
    }

    private void LateUpdate()
    {
        transform.position = MouseDirSet();

    }

    public void SpritSet(Sprite asdf)
    {
        _spriteRenderer.sprite = asdf;
    }

    public Vector3 MouseDirSet()
    {
        _dir = _cam.ScreenToWorldPoint(Input.mousePosition);
        return _dir;
    }
}
