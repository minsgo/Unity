using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{

    public GameObject[] story_1 = new GameObject[5];
    public AudioClip EffectSound;
    AudioSource audioSource;
    int key_count = 0;

    // Start is called before the first frame update
    void Start()
    {
        story_1[0].SetActive(false);
        story_1[1].SetActive(false);
        story_1[2].SetActive(false);
        story_1[3].SetActive(false);
        story_1[4].SetActive(false);
        this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Next_story();
    } 

    void Effect()
    {
        audioSource.clip = EffectSound;
        audioSource.Play();
    }

    public void Next_story()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            key_count++;

            switch (key_count)
            {
                case 1:
                    story_1[0].SetActive(true);
                    Effect();
                    break;
                case 2:
                    story_1[1].SetActive(true);
                    Effect();
                    break;
                case 3:
                    story_1[2].SetActive(true);
                    Effect();
                    break;
                case 4:
                    story_1[3].SetActive(true);
                    Effect();
                    break;
                case 5:
                    story_1[4].SetActive(true);
                    Effect();
                    break;
                default:
                    SceneManager.LoadScene("StageSelect");
                    break;
            }
           
        }
    }
}
