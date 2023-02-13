using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneChanger : MonoBehaviour
{
    public GameObject button;
    public GameObject button1;

    public GameObject Rage_button;
    public GameObject Sad_button;
    public GameObject Delight_button;
    public GameObject Next_button;

    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void FadeButton() //게임 시작할 때 버튼
    {
        Debug.Log("!!");
        button.SetActive(false);
        button1.SetActive(false);
        StartCoroutine(FadeInCorountine());
    }

    IEnumerator FadeInCorountine()
    {
        float fadeCount = 1f;
        for (float f = 1f; f > 0; f -= 0.01f)
        {
            Color c = image.GetComponent<Image>().color;
            c.a = f;
            image.GetComponent<Image>().color = c;
            yield return null;
            Debug.Log(fadeCount);
        }
        yield return new WaitForSeconds(1);
        fadeCount = 0;
        
        if(fadeCount <= 0)
        {
            StartCoroutine(FadeOutCorountine());
            SceneManager.LoadScene("StoryScenes");
        }
        
    }

    IEnumerator FadeOutCorountine()
    {
        
        for (float f = 0; f > 1f; f += 0.01f)
        {
            Color c = image.GetComponent<Image>().color;
            c.a = f;
            image.GetComponent<Image>().color = c;
            yield return null;
        }
        yield return new WaitForSeconds(1);

    }

 
    public void Main_Menu()
    {
        SceneManager.LoadScene("MainScenes");
    }
    public void Stage_Rage() //스테이지 선택 버튼
    {
        SceneManager.LoadScene("BossScenes_Rage");
    }
    public void Stage_Sad()
    {
        SceneManager.LoadScene("BossScenes_Sad");
    }
    public void Stage_Delight()
    {
        SceneManager.LoadScene("BossScenes_Delight");
    }
    public void Next_Stage()
    {
        SceneManager.LoadScene("StageSelect");
    }
    public void Game_Quit()
    {
        Application.Quit();
    }

    public void ClearCheck()
    {
        bool Sad_clear = SadBossDirector.isDie;
        bool Rage_clear = RageBossDirector.isDie;
        bool Delight_clear = DelightBossDirector.isDie;

        if (Sad_clear && Rage_clear && Delight_clear)
        {
            SceneManager.LoadScene("GameClear");
        }
    }

}
