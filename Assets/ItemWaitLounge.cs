using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class ItemWaitLounge : MonoBehaviour
{
    private UIDocument _dot;


    #region itemBox
    private Button itemBox1;
    private Button itemBox2;
    private Button itemBox3;
    private Button itemBox4;
    private Button itemBox5;
    private Button itemBox6;
    private Button itemBox7;
    #endregion

    private string _nullImage = "White 1x1 (UnityEngine.Texture2D)";
    private Camera _cam;
    public Sprite _nullSprite;
    Vector2 DefaultPos;



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

        _cam = Camera.main;
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

        itemBox1.clicked += Dorp;
        itemBox2.clicked += Dorp;
        itemBox3.clicked += Dorp;
        itemBox4.clicked += Dorp;
        itemBox5.clicked += Dorp;
        itemBox6.clicked += Dorp;
        itemBox7.clicked += Dorp;

    }

    public void itemButtonImageSet(Sprite _image)
    {
        if (itemBox1.style.backgroundImage.ToString() == _nullImage)
        {
            itemBox1.style.backgroundImage = _image.texture;
            Debug.Log("논이노");
        }
        else if (itemBox2.style.backgroundImage.ToString() == _nullImage)
        {
            itemBox2.style.backgroundImage = _image.texture;
            Debug.Log("논이노");
        }
        else if (itemBox3.style.backgroundImage.ToString() == _nullImage)
        {
            Debug.Log("논이노");
        }
        else if (itemBox4.style.backgroundImage.ToString() == _nullImage)
        {
            Debug.Log("논이노");
        }
        else if (itemBox5.style.backgroundImage.ToString() == _nullImage)
        {
            Debug.Log("논이노");
        }
        else if (itemBox6.style.backgroundImage.ToString() == _nullImage)
        {
            Debug.Log("논이노");
        }
        else if (itemBox7.style.backgroundImage.ToString() == _nullImage)
        {
            Debug.Log("논이노");
        }
    }
    private void Dorp()
    {
       



        //itemBox1.style.backgroundImage = _nullSprite.texture;
       
        //Debug.Log($"itemBox1.style.backgroundImage {itemBox1.style.backgroundImage.ToString()}" ); 
        //Debug.Log($"itemBox1.style {itemBox1.style}" );
        //Debug.Log($"itemBox1. {itemBox1}" );
        

    }

    private void Update()
    {

    }
}

