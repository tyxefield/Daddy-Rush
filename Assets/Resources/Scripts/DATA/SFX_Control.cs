using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Control
{
    public static GameObject handler;
    public static bool working;

    public static AudioSource asource;
    public static AudioSource aMusic;

    public static void PlaySound(AudioClip c)
    {
        asource.pitch = Random.Range(0.9f, 1.1f);
        asource.clip = c;
        asource.Play();
        return;
    }

    public static void StopMusic()
    {
        aMusic.Stop();
        asource.Stop();
        return;
    }

    public static void CreateSfxControl()
    {
        if (handler == null)
        {
            handler = new GameObject("SFX_Control");
            aMusic = handler.AddComponent<AudioSource>();
            aMusic.clip = SFX_Data.OST_Game;
            aMusic.loop = true;
            aMusic.playOnAwake = false;
            aMusic.volume = 0.5f;
            aMusic.pitch = 1.1f;
            asource = handler.AddComponent<AudioSource>();
            working = true;
            return;
        }
        else
        {
            handler.SetActive(true);
            working = true;
            return;
        }
    }



    
}
