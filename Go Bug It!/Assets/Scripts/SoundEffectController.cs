using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectController : MonoBehaviour
{
    #region references
    public static AudioClip dash, playerdeath, shot, laser, gravity, pause, gameOver, cura, hit, cambioDisparo, explosion;
    private AudioSource audioSrc;
    #endregion

    #region methods
    public void PlaySound(string clip)
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
            case "pause":
                audioSrc.PlayOneShot(pause);
                break;
            case "gameOver":
                audioSrc.PlayOneShot(gameOver);
                break;
            case "cura":
                audioSrc.PlayOneShot(cura);
                break;
            case "hit":
                audioSrc.PlayOneShot(hit);
                break;
            case "cambioDisparo":
                audioSrc.PlayOneShot(cambioDisparo);
                break;
            case "explosion":
                audioSrc.PlayOneShot(explosion);
                break;
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = gameObject.GetComponent<AudioSource>();
        dash = Resources.Load<AudioClip>("dash");
        playerdeath = Resources.Load<AudioClip>("playerdeath");
        shot = Resources.Load<AudioClip>("shot");
        laser = Resources.Load<AudioClip>("laser");
        gravity = Resources.Load<AudioClip>("gravity");
        pause = Resources.Load<AudioClip>("pause");
        gameOver = Resources.Load<AudioClip>("gameOver");
        cura = Resources.Load<AudioClip>("cura");
        hit = Resources.Load<AudioClip>("hit");
        cambioDisparo = Resources.Load<AudioClip>("cambioDisparo");
        explosion = Resources.Load<AudioClip>("explosion");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
