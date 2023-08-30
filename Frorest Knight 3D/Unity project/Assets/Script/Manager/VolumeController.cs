using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{

    public AudioSource bk1;
    public AudioSource bk2;
    public AudioSource bk3;
    public Slider slider;
    public Slider audioSlider;
    public static float a = 0.3f;


    private void Start()
    {
        if (a == 0.3f)
        {
            slider.value = bk1.volume;
        }
        else
        {
            slider.value = a;
        }
        
       // audioSlider.value = Sound.volume;
    
    }

    // Update is called once per frame
    void Update()
    {
        //音乐
        a = slider.value;
        bk1.volume = a;
        bk2.volume = a;
        bk3.volume = a;
        //音效
        //foreach (var anySound in AudioManager.sounds)
        //{
         //   anySound.volume= audioSlider.value;
       // }
    }


}
