using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;


public class SliderController : MonoBehaviour
{

    public Slider slider;
    public ComponentFunc percentFunction;




    // Use this for initialization
    void Start()
    {
        slider.maxValue = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {

        slider.value = (float)percentFunction.callFunc();

    }

    void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
