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

    private Camera _cam;
    public Sprite q;
    Vector2 DefaultPos;



    private void Awake()
    {
        _dot = GetComponent<UIDocument>();

        itemBox1 = _dot.rootVisualElement.Q<Button>("1");
        itemBox1.clicked += Dorp;



        _cam = Camera.main;
    }

    private void Dorp()
    {
        itemBox1.style.backgroundImage = q.texture;
        if (itemBox1.style.backgroundImage.ToString() == "Null")
        {
            Debug.Log("≥Ì¿Ã≥Î"); 
        }
        Debug.Log($"itemBox1.style.backgroundImage {itemBox1.style.backgroundImage.ToString()}" ); 
        Debug.Log($"itemBox1.style {itemBox1.style}" );
        Debug.Log($"itemBox1. {itemBox1}" );
        


        //itemBox.style.backgroundColor = Color.red;

        //itemBox.style.backgroundImage = q.texture;
        //a = true;

    }

    private void Update()
    {

    }
}

