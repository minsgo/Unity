using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSoundManager_Sad : MonoBehaviour
{
    public AudioClip SadPattern1;
    public AudioClip SadPattern2;
    public AudioClip SadPattern3;

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
            case "SADPATTERN1":
                audioSource.clip = SadPattern1;
                break;
            case "SADPATTERN2":
                audioSource.clip = SadPattern2;
                break;
            case "SADPATTERN3":
                audioSource.clip = SadPattern3;
                break;
        }
        audioSource.Play();
    }
    public void BossStop(string Action)
    {
        switch (Action)
        {
            case "SADPATTERN1":
                audioSource.clip = SadPattern1;
                break;
            case "SADPATTERN2":
                audioSource.clip = SadPattern2;
                break;
            case "SADPATTERN3":
                audioSource.clip = SadPattern3;
                break;
        }
        audioSource.Stop();
    }
}
