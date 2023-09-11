using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
public class ItemWaitLounge : MonoBehaviour
{
    private UIDocument _dot;
    [SerializeField] private UnityEvent<Sprite> _SetSprite;

    #region itemBox
    private Button itemBox1;
    private Button itemBox2;
    private Button itemBox3;
    private Button itemBox4;
    private Button itemBox5;
    private Button itemBox6;
    private Button itemBox7;

    private Sprite itemSprit1;
    private Sprite itemSprit2;
    private Sprite itemSprit3;
    private Sprite itemSprit4;
    private Sprite itemSprit5;
    private Sprite itemSprit6;
    private Sprite itemSprit7;
    #endregion

    private string _nullImage = "White 1x1 (UnityEngine.Texture2D)";
    public Sprite _nullSprite;



    private void Awake()
    {
        _dot = GetComponent<UIDocument>();

        itemBox1 = _dot.rootVisualElement.Q<Button>("1");
        itemBox2 = _dot.rootVisualElement.Q<Button>("2");
        itemBox3 = _dot.rootVisualElement.Q<Button>("3");
        itemBox4 = _dot.rootVisualElement.Q<Button>("4");
        itemBox5 = _dot.rootVisualElement.Q<Button>("5");
        itemBox6 = _dot.rootVisualElement.Q<Button>("6");
        itemBox7 = _dot.rootVisualElement.Q<Button>("7");

        Init();

    }

    private void Init()
    {
        itemBox1.style.backgroundImage = _nullSprite.texture;
        itemBox2.style.backgroundImage = _nullSprite.texture;
        itemBox3.style.backgroundImage = _nullSprite.texture;
        itemBox4.style.backgroundImage = _nullSprite.texture;
        itemBox5.style.backgroundImage = _nullSprite.texture;
        itemBox6.style.backgroundImage = _nullSprite.texture;
        itemBox7.style.backgroundImage = _nullSprite.texture;

        itemBox1.clicked += Drop1;
        itemBox2.clicked += Drop2;
        itemBox3.clicked += Drop3;
        itemBox4.clicked += Drop4;
        itemBox5.clicked += Drop5;
        itemBox6.clicked += Drop6;
        itemBox7.clicked += Drop7;

    }

    public void itemButtonImageSet(Sprite _image)
    {
        if (itemBox1.style.backgroundImage.ToString() == _nullImage)
        {
            itemSprit1 = _image;
            itemBox1.style.backgroundImage = _image.texture;
            Debug.Log("논이노");
        }
        else if (itemBox2.style.backgroundImage.ToString() == _nullImage)
        {
            itemSprit2 = _image;
            itemBox2.style.backgroundImage = _image.texture;
            Debug.Log("논이노");
        }
        else if (itemBox3.style.backgroundImage.ToString() == _nullImage)
        {
            itemSprit3 = _image;
            itemBox3.style.backgroundImage = _image.texture;
            Debug.Log("논이노");
        }
        else if (itemBox4.style.backgroundImage.ToString() == _nullImage)
        {
            itemSprit4 = _image;
            itemBox4.style.backgroundImage = _image.texture;
            Debug.Log("논이노");
        }
        else if (itemBox5.style.backgroundImage.ToString() == _nullImage)
        {
            itemSprit5 = _image;
            itemBox5.style.backgroundImage = _image.texture;
            Debug.Log("논이노");
        }
        else if (itemBox6.style.backgroundImage.ToString() == _nullImage)
        {
            itemSprit6 = _image;
            itemBox6.style.backgroundImage = _image.texture;
            Debug.Log("논이노");
        }
        else if (itemBox7.style.backgroundImage.ToString() == _nullImage)
        {
            itemSprit7 = _image;
            itemBox7.style.backgroundImage = _image.texture;
            Debug.Log("논이노");
        }
    }
    private void Drop1()
    {
        if (itemBox1.style.backgroundImage.ToString() != _nullImage)
        {
            _SetSprite?.Invoke(itemSprit1);
            itemBox1.style.backgroundImage = _nullSprite.texture;
        }
    }
    private void Drop2()
    {
        if (itemBox2.style.backgroundImage.ToString() != _nullImage)
        {
            _SetSprite?.Invoke(itemSprit2);
            itemBox2.style.backgroundImage = _nullSprite.texture;
        }
    }
    private void Drop3()
    {
        if (itemBox3.style.backgroundImage.ToString() != _nullImage)
        {
            _SetSprite?.Invoke(itemSprit3);
            itemBox3.style.backgroundImage = _nullSprite.texture;
        }
    }
    private void Drop4()
    {
        if (itemBox4.style.backgroundImage.ToString() != _nullImage)
        {
            _SetSprite?.Invoke(itemSprit4);
            itemBox4.style.backgroundImage = _nullSprite.texture;
        }
    }
    private void Drop5()
    {
        if (itemBox5.style.backgroundImage.ToString() != _nullImage)
        {
            _SetSprite?.Invoke(itemSprit5);
            itemBox5.style.backgroundImage = _nullSprite.texture;
        }
    }
    private void Drop6()
    {
        if (itemBox6.style.backgroundImage.ToString() != _nullImage)
        {
            _SetSprite?.Invoke(itemSprit6);
            itemBox6.style.backgroundImage = _nullSprite.texture;
        }

    }
    private void Drop7()
    {
        if (itemBox7.style.backgroundImage.ToString() != _nullImage)
        {
            _SetSprite?.Invoke(itemSprit7);
            itemBox7.style.backgroundImage = _nullSprite.texture;
        }
    }
    private void Dorp()
    {
        //Debug.Log($"itemBox1.style.backgroundImage {itemBox1.style.backgroundImage.ToString()}" ); 
        //Debug.Log($"itemBox1.style {itemBox1.style}" );
        //Debug.Log($"itemBox1. {itemBox1}" );
    }

    private void Update()
    {

    }
}

