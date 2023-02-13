using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SadBossDirector : MonoBehaviour
{
    public GameObject Pattern1_warn;
    public GameObject Pattern1_Pillar;
    public GameObject Pattern2_Prefab;
    public GameObject Pattern3_Prefab;
    public GameObject main_carema;

    public Slider BossHealthBar;

    enum SadBossState { wait, pattern1, pattern2, pattern3, rest };
    SadBossState state = SadBossState.rest;

    float screen_width;
    float screen_height;
    int patternindex = 1;
    float runTime = 0.0f;

    public int HP = 50;
    public static bool isDie = false;

    // Start is called before the first frame update
    void Start()
    {
        screen_height = main_carema.GetComponent<Camera>().orthographicSize;
        screen_width = screen_height * Screen.width / Screen.height;
        HP = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == SadBossState.rest)
        {
            runTime += Time.deltaTime;
            if (runTime > 2.0f)
            {
                runTime = 0.0f;
                if (patternindex == 1)
                {
                    patternindex = 2;
                    state = SadBossState.pattern1;
                    StartCoroutine(SadPattern1_Coroutine());
                }
                else if (patternindex == 2)
                {
                    patternindex = 3;
                    state = SadBossState.pattern2;
                    StartCoroutine(SadPattern2_Coroutine());
                }
                else if (patternindex == 3)
                {
                    patternindex = 1;
                    state = SadBossState.pattern3;
                    StartCoroutine(SadPattern3_Coroutine());
                }
            }
        }
        if(HP <= 0)
        {
            isDie = true;
            SceneManager.LoadScene("StageClear");
        }
    }
    
    IEnumerator SadPattern1_Coroutine()
    {
        float warn_x = screen_width - Pattern1_warn.GetComponent<SpriteRenderer>().bounds.size.x * 2;
        float attack_x = 0.0f;//Pattern1_Pillar.GetComponent<SpriteRenderer>().bounds.size.x * 2;

        Vector3 pos = new Vector3(warn_x,
            -screen_height + (screen_height/5 *2.0f),
            0.0f);
        Instantiate(Pattern1_warn, pos, transform.rotation);
        yield return new WaitForSeconds(1.0f);

        pos.x= attack_x;
        Instantiate(Pattern1_Pillar, pos, transform.rotation);
        BossSoundManager_Sad sound = GameObject.Find("BossSoundManager_Sad").GetComponent<BossSoundManager_Sad>();
        sound.BossPlay("SADPATTERN1");
        
        yield return new WaitForSeconds(2.0f);


        pos.y += (screen_height / 5 * 3.0f);

        pos.x = warn_x;
        Instantiate(Pattern1_warn, pos, transform.rotation);
        yield return new WaitForSeconds(1.0f);

        pos.x = attack_x;
        Instantiate(Pattern1_Pillar, pos, transform.rotation);
        yield return new WaitForSeconds(2.0f);


        pos.y += (screen_height / 5 * 3.0f);
        pos.x = warn_x;
        Instantiate(Pattern1_warn, pos, transform.rotation);
        yield return new WaitForSeconds(1.0f);
        pos.x = attack_x;
        Instantiate(Pattern1_Pillar, pos, transform.rotation);
        yield return new WaitForSeconds(2.0f);
        state = SadBossState.rest;
    }

    IEnumerator SadPattern2_Coroutine()
    {
        Instantiate(Pattern2_Prefab, new Vector3(0.0f,0.0f,0.0f), transform.rotation);
        BossSoundManager_Sad sound = GameObject.Find("BossSoundManager_Sad").GetComponent<BossSoundManager_Sad>();
        sound.BossPlay("SADPATTERN2");
        yield return new WaitForSeconds(9.0f);
        state = SadBossState.rest;
    }

    IEnumerator SadPattern3_Coroutine()
    {
        GameObject obj;
        Vector3 pos = new Vector3(9.0f,-3.0f,0.0f);
        float temp = 16.0f / 10.0f;
        WaitForSeconds oneseconds = new WaitForSeconds(0.1f);

        //경고
        //yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 10; i++)
        {
            pos.x -= temp;
            obj = Instantiate(Pattern1_warn , pos, transform.rotation);
            yield return null;
            obj.GetComponent<SadPattern1Warn>().SetAlpha(true);
            yield return oneseconds;
        }
        yield return new WaitForSeconds(1.0f);

        //파도생성
        pos = new Vector3(screen_width + Pattern3_Prefab.GetComponent<SpriteRenderer>().bounds.size.x * 2, -2.38f, 0.0f);
        obj = Instantiate(Pattern3_Prefab, pos, transform.rotation);
        BossSoundManager_Sad sound = GameObject.Find("BossSoundManager_Sad").GetComponent<BossSoundManager_Sad>();
        sound.BossPlay("SADPATTERN3");
        yield return null;
        float Speed = -(screen_width * 2 + Pattern3_Prefab.GetComponent<SpriteRenderer>().bounds.size.x * 2) / 3.0f;
        obj.GetComponent<SadPattern3>().SetSpeed(Speed);
        yield return new WaitForSeconds(4.0f);

        state = SadBossState.rest;
    }

    public void Hit(int n)
    {
        HP-=n;
        BossHealthBar.value -= 0.1f * n;
    }
}
