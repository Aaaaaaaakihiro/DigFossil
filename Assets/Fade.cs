using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] private float _fadeInTime = 20;//フェードインする時間
    private Image _image;
    private float _red, _green, _blue;
    private void Start()
    {
        _image = this.gameObject.GetComponent<Image>();
        _red = _image.color.r;
        _green = _image.color.g;
        _blue = _image.color.b;
        //コルーチンで使用する待ち時間を計測
        _fadeInTime = 1f * _fadeInTime / 10f;
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn()
    {
        //Colorのアルファを0.1ずつ下げていく
        for(float i = 1f; i >= 0; i -= 0.1f)
        {
            _image.color = new Color(_red, _green, _blue, i);
            //指定秒数待つ
            yield return new WaitForSeconds(_fadeInTime);
        }
    }

    IEnumerator FadeOut()
    {
        for(float i = 0f; i <= 1; i += 0.1f)
        {
            _image.color = new Color(_red, _green, _blue, i);
            yield return new WaitForSeconds(_fadeInTime);
        }
    }
}
