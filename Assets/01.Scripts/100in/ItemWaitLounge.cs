using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEditor;

public class ItemWaitLounge : MonoBehaviour
{
    private UIDocument _dot;
    private VisualElement _root;
    [SerializeField] private UnityEvent<Sprite> _SetSprite;
    private Dictionary<string, VisualElement> _itemBox = new Dictionary<string, VisualElement>();
    Vector2 localMousePosition;

    private string _nullImage = "NullSprit(UnityEngine.Sprite)";
    public Sprite _nullSprite;
    private VisualElement _itemWait;
    private static bool OnDrage;

    #region _currentElementSet

    private VisualElement _currentDrageElement;
    private StyleLength _currentTargetTop;
    private StyleLength _currentTargetLeft;
    #endregion


    bool isDragging = false;

    private void Awake()
    {
        _dot = GetComponent<UIDocument>();
        _root = _dot.rootVisualElement;

        for (int i = 1; i < 8; i++)
        {
            _itemBox.Add(i.ToString(), _root.Q<VisualElement>(i.ToString()));
            _itemBox[i.ToString()].RegisterCallback<MouseDownEvent>(DrageDown);
            _itemBox[i.ToString()].RegisterCallback<MouseUpEvent>(DrageUp);
            _itemBox[i.ToString()].RegisterCallback<MouseMoveEvent>(DrageMove);
            _itemBox[i.ToString()].RegisterCallback<MouseOutEvent>(DrageOut);
        }

        _itemWait = _dot.rootVisualElement.Q<VisualElement>("ItemWait");
    }

    private void DrageOut(MouseOutEvent evt)
    {
        if (isDragging)
        {
            localMousePosition = _currentDrageElement.ChangeCoordinatesTo(_currentDrageElement.parent, evt.localMousePosition);
        }
    }

  
    private void DrageMove(MouseMoveEvent evt)
    {
        //a = evt;
        if (isDragging)
        {
            // 올바른 UI 요소에서 이벤트가 발생하는지 확인
            if (_currentDrageElement != null)
            {
                //Debug.Log(evt.localMousePosition);
      
                
                localMousePosition = _currentDrageElement.ChangeCoordinatesTo(_currentDrageElement.parent, evt.localMousePosition);

            }
        }

    }


    private void DrageUp(MouseUpEvent evt)
    {
        _currentDrageElement.style.top = _currentTargetTop;
        _currentDrageElement.style.left = _currentTargetLeft;

        isDragging = false;
        if (isDragging)
        {
            OnDrage = false;
            if (_currentDrageElement != null)
            {
                //_currentDrageElement.style.top = _currentTargetTop;
                //Debug.Log(_currentTargetTop);
                //_currentDrageElement.style.left = _currentTargetLeft;
                //Debug.Log(_currentTargetLeft);

                _currentDrageElement.RemoveFromHierarchy();
                _currentDrageElement = null;
            }
        }
    }

    private void DrageDown(MouseDownEvent evt)
    {
        isDragging = true;
        OnDrage = true;
        _currentDrageElement = evt.target as VisualElement;
        _currentTargetTop = _currentDrageElement.style.top;
        _currentTargetLeft = _currentDrageElement.style.left;
    }


    private void Update()
    {

        if (isDragging)
        {
            //localMousePosition = _currentDrageElement.ChangeCoordinatesTo(_currentDrageElement.parent, a.localMousePosition);
            _currentDrageElement.style.left = localMousePosition.x - (_currentDrageElement.resolvedStyle.width / 2);
            _currentDrageElement.style.top = localMousePosition.y - (_currentDrageElement.resolvedStyle.height / 2);
        }
    }
    //public void itemButtonImageSet( WeaponStatusSO weaponStatusDO)
    //{
    //    if (itemBox1.style.backgroundImage.ToString() == _nullImage)
    //    {
    //        itemSprit1 = weaponStatusDO.WeaponSprite;
    //        itemBox1.style.backgroundImage = itemSprit1.texture;
    //        Debug.Log("논이노");
    //    }
    //    else if (itemBox2.style.backgroundImage.ToString() == _nullImage)
    //    {
    //        itemSprit2 = weaponStatusDO.WeaponSprite;
    //        itemBox2.style.backgroundImage = itemSprit2.texture;
    //        Debug.Log("논이노");
    //    }
    //    else if (itemBox3.style.backgroundImage.ToString() == _nullImage)
    //    {
    //        itemSprit3 = weaponStatusDO.WeaponSprite;
    //        itemBox3.style.backgroundImage = itemSprit3.texture;
    //        Debug.Log("논이노");
    //    }
    //    else if (itemBox4.style.backgroundImage.ToString() == _nullImage)
    //    {
    //        itemSprit4 = weaponStatusDO.WeaponSprite;
    //        itemBox4.style.backgroundImage = itemSprit4.texture;
    //        Debug.Log("논이노");
    //    }
    //    else if (itemBox5.style.backgroundImage.ToString() == _nullImage)
    //    {
    //        itemSprit5 = weaponStatusDO.WeaponSprite;
    //        itemBox5.style.backgroundImage = itemSprit5.texture;
    //        Debug.Log("논이노");
    //    }
    //    else if (itemBox6.style.backgroundImage.ToString() == _nullImage)
    //    {
    //        itemSprit6 = weaponStatusDO.WeaponSprite;
    //        itemBox6.style.backgroundImage = itemSprit6.texture;
    //        Debug.Log("논이노");
    //    }
    //    else if (itemBox7.style.backgroundImage.ToString() == _nullImage)
    //    {
    //        itemSprit7 = weaponStatusDO.WeaponSprite;
    //        itemBox7.style.backgroundImage = itemSprit7.texture;
    //        Debug.Log("논이노");
    //    }
    //}
    //private void Drop1()
    //{
    //    if (itemBox1.style.backgroundImage.ToString() != _nullImage)
    //    {
    //        _SetSprite?.Invoke(itemSprit1);
    //        itemBox1.style.backgroundImage = _nullSprite.texture;
    //        ItemWaitOn();
    //    }
    //}
    //private void Drop2()
    //{
    //    if (itemBox2.style.backgroundImage.ToString() != _nullImage)
    //    {
    //        _SetSprite?.Invoke(itemSprit2);
    //        itemBox2.style.backgroundImage = _nullSprite.texture;
    //        ItemWaitOn();
    //    }
    //}
    //private void Drop3()
    //{
    //    if (itemBox3.style.backgroundImage.ToString() != _nullImage)
    //    {
    //        _SetSprite?.Invoke(itemSprit3);
    //        itemBox3.style.backgroundImage = _nullSprite.texture;
    //        ItemWaitOn();
    //    }
    //}
    //private void Drop4()
    //{
    //    if (itemBox4.style.backgroundImage.ToString() != _nullImage)
    //    {
    //        _SetSprite?.Invoke(itemSprit4);
    //        itemBox4.style.backgroundImage = _nullSprite.texture;
    //        ItemWaitOn();
    //    }
    //}
    //private void Drop5()
    //{
    //    if (itemBox5.style.backgroundImage.ToString() != _nullImage)
    //    {
    //        _SetSprite?.Invoke(itemSprit5);
    //        itemBox5.style.backgroundImage = _nullSprite.texture;
    //        ItemWaitOn();
    //    }
    //}
    //private void Drop6()
    //{
    //    if (itemBox6.style.backgroundImage.ToString() != _nullImage)
    //    {
    //        _SetSprite?.Invoke(itemSprit6);
    //        itemBox6.style.backgroundImage = _nullSprite.texture;
    //        ItemWaitOn();
    //    }

    //}
    //private void Drop7()
    //{
    //    if (itemBox7.style.backgroundImage.ToString() != _nullImage)
    //    {
    //        _SetSprite?.Invoke(itemSprit7);
    //        itemBox7.style.backgroundImage = _nullSprite.texture;
    //        ItemWaitOn();
    //    }
    //}
    //private void Dorp()
    //{
    //    Debug.Log($"itemBox1.style.backgroundImage {itemBox1.style.backgroundImage.ToString()}" ); 
    //    Debug.Log($"itemBox1.style {itemBox1.style}" );
    //    Debug.Log($"itemBox1. {itemBox1}" );
    //}

    //public void ItemWaitOn()
    //{
    //    _itemWait.AddToClassList("on");
    //}
    //public void ItemWaitOff()
    //{
    //    _itemWait.RemoveFromClassList("on");
    //}
}

