using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip audioJump;
    public AudioClip rageWeak;
    public AudioClip rageStrong;
    public AudioClip sadWeak;
    public AudioClip sadStrong;
    public AudioClip delightWeak;
    public AudioClip delightStrong;
    public AudioClip voidWeak;
    public AudioClip voidStrong;
    public AudioClip damaged;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SoundPlay(string Action)
    {
        switch(Action)
        {
            case "JUMP":
                audioSource.clip = audioJump;
                break;
            case "RAGEWEAK":
                audioSource.clip = rageWeak;
                break;
            case "RAGESTRONG":
                audioSource.clip = rageStrong;
                break;
            case "SADWEAK":
                audioSource.clip = sadWeak;
                break;
            case "SADSTRONG":
                audioSource.clip = sadStrong;
                break;
            case "DELIGHTWEAK":
                audioSource.clip = delightWeak;
                break;
            case "DELIGHTSTRONG":
                audioSource.clip = delightStrong;
                break;
            case "VOIDWEAK":
                audioSource.clip = voidWeak;
                break;
            case "VOIDSTRONG":
                audioSource.clip = voidStrong;
                break;
            case "DAMAGED":
                audioSource.clip = damaged;
                break;

        }
        audioSource.Play();
    }

}
