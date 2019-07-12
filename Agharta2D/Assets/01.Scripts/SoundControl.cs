using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//유은호
public class SoundControl : MonoBehaviour
{
    public AudioSource glassBreak;

    public void SoundGlassBreak()
    {
        if (!glassBreak.isPlaying)//소리재생
        {
            glassBreak.Play();
        }
        else//소리가 이미 재생되어있다면
        {
            glassBreak.Stop();
            glassBreak.Play();
        }
    }
}