using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSoundManager_Rage : MonoBehaviour
{

    public AudioClip RagePattern1;
    public AudioClip RagePattern2;
    public AudioClip RagePattern3;

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
            case "RAGEPATTERN1":
                audioSource.clip = RagePattern1;
                break;
            case "RAGEPATTERN2":
                audioSource.clip = RagePattern2;
                break;
            case "RAGEPATTERN3":
                audioSource.clip = RagePattern3;
                break;
        }
        audioSource.Play();
    }
}
