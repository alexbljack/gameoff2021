using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    [SerializeField] float fadeSpeed;
    
    Image _image;

    void Awake()
    {
        _image = GetComponent<Image>();
    }
    
    public IEnumerator FadeIn()
    {
        while (_image.color.a < 1)
        {
            var alpha = _image.color.a + Time.deltaTime * fadeSpeed; 
            SetAlpha(alpha);
            yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        while (_image.color.a > 0)
        {
            var alpha = _image.color.a - Time.deltaTime * fadeSpeed;
            SetAlpha(alpha);
            yield return null;
        }
    }

    public void SetAlpha(float alpha)
    {
        alpha = Mathf.Clamp(alpha, 0, 1);
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, alpha);
    }
}
