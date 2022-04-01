using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectController : MonoBehaviour
{
    #region references
    public static AudioClip dash, playerdeath, shot, laser, gravity;
    static AudioSource audioSrc;
    #endregion

    #region methods
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "dash":
                audioSrc.PlayOneShot(dash);
                break;
            case "playerdeath":
                audioSrc.PlayOneShot(playerdeath);
                break;
            case "shot":
                audioSrc.PlayOneShot(shot);
                break;
            case "laser":
                audioSrc.PlayOneShot(laser);
                break;
            case "gravity":
                audioSrc.PlayOneShot(gravity);
                break;
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        dash = Resources.Load<AudioClip>("dash");
        playerdeath = Resources.Load<AudioClip>("playerdeath");
        shot = Resources.Load<AudioClip>("shot");
        laser = Resources.Load<AudioClip>("laser");
        gravity = Resources.Load<AudioClip>("gravity");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
