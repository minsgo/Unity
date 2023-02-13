using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DelightBossDirector : MonoBehaviour
{
    public Slider BossHealthBar;
    public GameObject Pattern1_warn;
    public GameObject Pattern1_Feather;
    public GameObject Pattern2_Prefab;
    public GameObject Pattern3_Prefab_warn;
    public GameObject Pattern2_Prefab_shield;
    public GameObject Pattern3_Prefab;
    public GameObject main_carema;
    


    int MAXHP = 50;
    public int HP;
    int shield = 10;

    enum DelightBossState { wait, pattern1, pattern2, pattern3, rest };
    DelightBossState state = DelightBossState.rest;

    private GameObject Player;
    float screen_width;
    float screen_height;
    int patternindex = 1;
    float runTime = 0.0f;
    public static bool isDie = false;

    float start1_x;
    float start1_x_stride;
    float start2_x;

    float start1_y;
    float start1_y_stride;
    float start2_y;

    GameObject[] Featherobj = new GameObject[14];

    bool pattern3 = false;


    // Start is called before the first frame update
    void Start()
    {
        HP = MAXHP;
        pattern3 = false;
        screen_height = main_carema.GetComponent<Camera>().orthographicSize;
        screen_width = screen_height * Screen.width / Screen.height;
        Player = GameObject.FindWithTag("Player");

        start1_x_stride = screen_width / 9;
        start1_x = screen_width / 18 - screen_width;
        start2_x = screen_width / 18 + start1_x_stride - screen_width;

        start1_y_stride = screen_height / 5;
        start1_y = (screen_height / 10 + start1_y_stride + screen_height) - 1;
        start2_y = (screen_height / 10 + screen_height) - 1;
    }

    // Update is called once per frame
    void Update()
    {

        if (state == DelightBossState.rest)
        {
            runTime += Time.deltaTime;
            if (runTime > 2.0f)
            {
                runTime = 0.0f;
                if (patternindex == 1)
                {
                    patternindex = 2;
                    state = DelightBossState.pattern1;
                    StartCoroutine(DelightPattern1_Coroutine());
                }
                else if (patternindex == 2)
                {
                    patternindex = 1;
                    state = DelightBossState.pattern2;
                    StartCoroutine(DelightPattern2_Coroutine());
                }
                    
            }
        }
        if (HP <= 25 && !pattern3) {
            StartCoroutine(DelightPattern3_Coroutine());
            pattern3 = true;
        }
        if(HP <= 0)
        {
            isDie = true;
            SceneManager.LoadScene("GameClear");
        }
    }

    IEnumerator DelightPattern1_Coroutine()
    {
        Vector3 pos = Vector3.zero;
        Quaternion ROT = transform.rotation;
        //첫번째  warn
        pos.x = start1_x - start1_x_stride * 2;
        pos.y = start2_y;
        for (int i = 0; i < 9; i++)
        {
            pos.x += start1_x_stride * 2;
            Instantiate(Pattern1_warn, pos, Quaternion.identity);
        }
        pos.x += start1_x_stride;
        pos.y = start1_y;
        for (int i = 9; i < 14; i++)
        {
            pos.y -= start1_y_stride * 2;
            Instantiate(Pattern1_warn, pos, Quaternion.identity);
        }
        yield return new WaitForSeconds(1.0f);

        //첫번째 공격
        pos.x = start1_x - start1_x_stride;
        pos.y = start2_y + start1_y_stride;
        for (int i = 0; i < 9; i++)
        {
            pos.x += start1_x_stride * 2;
            Featherobj[i] = Instantiate(Pattern1_Feather, pos, ROT);
            BossSoundManager_Delight sound = GameObject.Find("BossSoundManager_Delight").GetComponent<BossSoundManager_Delight>();  
            sound.BossPlay("DELIGHTPATTERN1");
        }
        
        pos.x += start1_x_stride;
        pos.y = start1_y - start1_y_stride;
        for (int i = 9; i < 14; i++)
        {
            pos.y -= start1_y_stride * 2;
            Featherobj[i] = Instantiate(Pattern1_Feather, pos, ROT);
            BossSoundManager_Delight sound = GameObject.Find("BossSoundManager_Delight").GetComponent<BossSoundManager_Delight>();
            sound.BossPlay("DELIGHTPATTERN1");
        }
        yield return null;

        for (int i = 0; i < 14; i++)
            Featherobj[i].GetComponent<DelightPattern1>().SetSpeed(new Vector3(0.0f, -20.0f, 0.0f));
        yield return new WaitForSeconds(1.5f);


        //두번째  warn
        pos.x = start2_x - start1_x_stride * 2;
        pos.y = start2_y;
        for (int i = 0; i < 9; i++)
        {
            pos.x += start1_x_stride * 2;
            Instantiate(Pattern1_warn, pos, Quaternion.identity);
        }
        pos.y = start2_y + start1_y_stride * 2;
        for (int i = 9; i < 13; i++)
        {
            pos.y -= start1_y_stride * 2;
            Instantiate(Pattern1_warn, pos, Quaternion.identity);
        }
        yield return new WaitForSeconds(1.0f);



        //두번째 공격
        pos.x = start2_x - start1_x_stride;
        pos.y = start2_y - start1_y_stride;
        for (int i = 0; i < 9; i++)
        {
            pos.x += start1_x_stride * 2;
            Featherobj[i] = Instantiate(Pattern1_Feather, pos, ROT);
            BossSoundManager_Delight sound = GameObject.Find("BossSoundManager_Delight").GetComponent<BossSoundManager_Delight>();
            sound.BossPlay("DELIGHTPATTERN1");
        }
        pos.y = start2_y - start1_y_stride;
        for (int i = 9; i < 13; i++)
        {
            pos.y -= start1_y_stride * 2;
            Featherobj[i] = Instantiate(Pattern1_Feather, pos, ROT);
            BossSoundManager_Delight sound = GameObject.Find("BossSoundManager_Delight").GetComponent<BossSoundManager_Delight>();
            sound.BossPlay("DELIGHTPATTERN1");
        }
        yield return null;
        for (int i = 0; i < 13; i++)
            Featherobj[i].GetComponent<DelightPattern1>().SetSpeed(new Vector3(0.0f, -20.0f, 0.0f));
        yield return new WaitForSeconds(1.5f);

        state = DelightBossState.rest;
    }
    IEnumerator DelightPattern2_Coroutine()
    {
        shield = 10;
        Instantiate(Pattern2_Prefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        yield return new WaitForSeconds(10.0f);

        if(shield > 0)
        {
            Instantiate(Pattern2_Prefab, new Vector3(0.0f,0.0f,0.0f), Quaternion.identity);  
        }
        state = DelightBossState.rest;
    }
    IEnumerator DelightPattern3_Coroutine()
    {
        while (!isDie) {
            Instantiate(Pattern3_Prefab_warn, Player.transform.position, Quaternion.identity);
            BossSoundManager_Delight sound = GameObject.Find("BossSoundManager_Delight").GetComponent<BossSoundManager_Delight>();
            sound.BossPlay("DELIGHTPATTERN3");
            yield return new WaitForSeconds(3.5f);
        }
    }

    public void Hit(int n)
    {
        if (!isDie)
        {
            HP-=n;
            BossHealthBar.value -= 0.1f * n;
        }
    }

    public void Heal(int n)
    {
        HP += n;
        BossHealthBar.value += 0.1f * n;
    }



}

