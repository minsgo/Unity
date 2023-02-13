using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSoundManager_Delight : MonoBehaviour
{
    public AudioClip DelightPattern1;
    public AudioClip DelightPattern2;
    public AudioClip DelightPattern3;

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
    public void BossPlay(string Action)
    {
        switch (Action)
        {
            case "DELIGHTPATTERN1":
                audioSource.clip = DelightPattern1;
                break;
            case "DELIGHTPATTERN2":
                audioSource.clip = DelightPattern2;
                break;
            case "DELIGHTPATTERN3":
                audioSource.clip = DelightPattern3;
                break;
        }
        audioSource.Play();
    }
}
