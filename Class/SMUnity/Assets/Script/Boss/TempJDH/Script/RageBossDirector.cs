using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RageBossDirector : MonoBehaviour
{
    public GameObject Pattern1_Prefab1; 
    public GameObject Pattern1_Prefab2; 
    public GameObject Pattern2_Prefab1;
    public GameObject Pattern2_Prefab2;
    public GameObject Pattern3_Prefab1; 
    public GameObject Pattern3_Prefab2; 
    public GameObject main_carema;
    public Slider BossHealthBar;

    //패턴 전체
    enum RageBossState { wait, pattern1, pattern2, pattern3, rest};
    RageBossState state = RageBossState.rest;
    float screen_width;
    float screen_height;
    float image1_x;
    int patternindex = 1;
    float runTime = 0.0f;

    public int HP = 50;
    public static bool isDie = false;

    //패턴 1 관련
    enum Pattern1State { phase1, phase2, phase3, ignore };
    Pattern1State p1state = Pattern1State.ignore;
    UnityEngine.Vector3 p1pos;

    //패턴 2 관련
    enum Pattern2State { phase1, phase2, phase3, ignore };
    Pattern2State p2state = Pattern2State.ignore;

    const int pattenr2_width_count = 18;
    const int pattenr2_height_count = 10;
    int[] pattern2_firepos_width = new int[4];
    int[] pattern2_firepos_height = new int[4];
    float pattern2_width_stride;
    float pattern2_height_stride;
    GameObject[] fireballobj = new GameObject[4];

    //패턴 3 관련
    enum Pattern3State { phase1, phase2, phase3, phase4, phase5, ignore };
    Pattern3State p3state = Pattern3State.ignore;
    UnityEngine.Vector3 p3pos;
    float speed;
    float imagey;
    GameObject p3pre;
    UnityEngine.Vector3 p3temp;

    // Start is called before the first frame update
    void Start()
    {
        HP = 50;
        image1_x = Pattern1_Prefab1.GetComponent<SpriteRenderer>().bounds.size.x;
        //camera = GameObject.FindGameObjectWithTag("MainCamera");
        screen_height = main_carema.GetComponent<Camera>().orthographicSize;
        screen_width = screen_height * Screen.width / Screen.height;

        pattern2_width_stride = screen_width / 9;
        pattern2_height_stride = screen_height / 5;
        RagePattern2Init();

        speed = screen_height / 1.5f;
        imagey = Pattern3_Prefab2.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == RageBossState.rest)
        {
            runTime += Time.deltaTime;
            if (runTime > 2.0f)
            {
                //Debug.Log("스타트로들어옴");
                
                runTime = 0.0f;
                if (patternindex == 1)
                {
                    patternindex = 2;
                state = RageBossState.pattern1;
                p1state = Pattern1State.phase1;
                StartCoroutine(RagePattern1_Update());
                }
                else if (patternindex == 2)
                {
                    patternindex = 3;
                    state = RageBossState.pattern2;
                    //p2state = Pattern2State.phase1;
                    RagePattern2Init();
                    RagePattern2Random();
                    StartCoroutine(RagePattern2_Update());
                }
                else if (patternindex == 3)
                {
                    patternindex = 1;
                    p3state = Pattern3State.phase1;
                    state = RageBossState.pattern3;
                }
                //Debug.Log("스타트에서나감");
            }
        }
        else if (state == RageBossState.pattern3)
        {
            RagePattern3_Update();
        }

        if(HP <= 0)
        {
            isDie = true;
            SceneManager.LoadScene("StageClear");
        }

    }

    IEnumerator RagePattern1_Update()
    {
        for (int i = 0; i < 3; i++)
        {
            float temp = -screen_width + (image1_x / 2);
            p1pos = new UnityEngine.Vector3(temp + ((screen_width / 3) * i * 2), 0.0f, 0.0f);
            Instantiate(Pattern1_Prefab2, p1pos, this.transform.rotation);
            
        }

        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < 3; i++)
        {
            float temp = -screen_width + (image1_x / 2);
            p1pos = new UnityEngine.Vector3(temp + ((screen_width / 3) * i * 2), 0.0f, 0.0f);
            Instantiate(Pattern1_Prefab1, p1pos, this.transform.rotation);
            BossSoundManager_Rage sound = GameObject.Find("BossSoundManager_Rage").GetComponent<BossSoundManager_Rage>();
            sound.BossPlay("RAGEPATTERN1");
        }

        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < 3; i++)
        {
            float temp = -screen_width + (image1_x / 2) + (screen_width / 3);
            p1pos = new UnityEngine.Vector3(temp + ((screen_width / 3) * i * 2), 0.0f, 0.0f);
            Instantiate(Pattern1_Prefab2, p1pos, this.transform.rotation);
        }

        yield return new WaitForSeconds(1.5f);


        for (int i = 0; i < 3; i++)
        {
            float temp = -screen_width + (image1_x / 2) + (screen_width / 3);
            p1pos = new UnityEngine.Vector3(temp + ((screen_width / 3) * i * 2), 0.0f, 0.0f);
            Instantiate(Pattern1_Prefab1, p1pos, this.transform.rotation);
            BossSoundManager_Rage sound = GameObject.Find("BossSoundManager_Rage").GetComponent<BossSoundManager_Rage>();
            sound.BossPlay("RAGEPATTERN1");
        }

        yield return new WaitForSeconds(1.0f);
            state = RageBossState.rest;
            p1state= Pattern1State.ignore;
    }

    void RagePattern2Init()
    {
        for (int i = 0; i < 4; i++)
            pattern2_firepos_width[i] = pattern2_firepos_height[i] = -20;
    }

    void RagePattern2Random()
    {
        bool duplication = false;

        for (int i = 0; i < 4;)
        {
            int ran = Random.Range(-pattenr2_width_count / 2, pattenr2_width_count/2);
            for (int j = 0; j < i; j++)
            {
                if (ran == pattern2_firepos_width[j])
                {
                    duplication = true;
                    break;
                }
            }

            if (!duplication)
            {
                pattern2_firepos_width[i] = ran;
                i++;
            }
            duplication = false;
        }

        //string str = "가로 : ";

        //for (int k = 0; k < 4; k++)
        //{
        //    str = str + pattern2_firepos_width[k] + ", ";
        //}

        //Debug.Log(str);

        for (int i = 0; i < 4;)
        {
            int ran = Random.Range(-pattenr2_height_count / 2, pattenr2_height_count/2);
            for (int j = 0; j < i; j++)
            {
                if (ran == pattern2_firepos_height[j])
                {
                    duplication = true;
                    break;
                }
            }

            if (!duplication)
            {
                pattern2_firepos_height[i] = ran;
                i++;
            }
            duplication = false;
        }

        //str = "세로 : ";

        //for (int k = 0; k < 4; k++)
        //{
        //    str = str + pattern2_firepos_height[k] + ", ";
        //}

        //Debug.Log(str);

    }

    IEnumerator RagePattern2_Update()
    {
        UnityEngine.Vector3 pos = new UnityEngine.Vector3(0.0f, 0.0f, 0.0f);

        int ran = Random.Range(1, 5);
        switch (ran)
        {
            case 1: //LEFT
                pos.x = -(screen_width) + Pattern2_Prefab1.GetComponent<SpriteRenderer>().bounds.size.x;
                break;
            case 2: //RIGHT
                pos.x = (screen_width) - Pattern2_Prefab1.GetComponent<SpriteRenderer>().bounds.size.x;
                break;
            case 3: //TOP
                pos.y = (screen_height) - Pattern2_Prefab1.GetComponent<SpriteRenderer>().bounds.size.y / 2;
                break;
            case 4: //BOTTOM
                pos.y = -(screen_height) + Pattern2_Prefab1.GetComponent<SpriteRenderer>().bounds.size.y / 2;
                break;
        }
        //경고 표시
        if (ran == 1 || ran == 2)
        {
            for (int i = 0; i < 4; i++)
            {
                pos.y = pattern2_height_stride * pattern2_firepos_height[i] + Pattern2_Prefab1.GetComponent<SpriteRenderer>().bounds.size.y/2;
                Instantiate(Pattern2_Prefab1, pos, transform.rotation);
                
            }
        }
        else if (ran == 3 || ran == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                pos.x = pattern2_width_stride * pattern2_firepos_width[i] + Pattern2_Prefab1.GetComponent<SpriteRenderer>().bounds.size.x / 2;
                Instantiate(Pattern2_Prefab1, pos, transform.rotation);
            }
        }
        yield return new WaitForSeconds(1.0f);
        //화염구 생성
        UnityEngine.Vector3 speed = new UnityEngine.Vector3(0.0f, 0.0f, 0.0f);
        float move_sum = -1.0f;
        

        if (ran == 1 || ran == 2)
        {
            if (ran == 1)
            {
                speed.x = screen_width / 0.2f;
                pos.x -= (Pattern2_Prefab1.GetComponent<SpriteRenderer>().bounds.size.x + Pattern2_Prefab2.GetComponent<SpriteRenderer>().bounds.size.x) ;
            }
            else
            {
                speed.x = -(screen_width / 0.2f);
                pos.x += (Pattern2_Prefab1.GetComponent<SpriteRenderer>().bounds.size.x + Pattern2_Prefab2.GetComponent<SpriteRenderer>().bounds.size.x);
            }

            move_sum = screen_width * 2;
            for (int i = 0; i < 4; i++)
            {
                pos.y = pattern2_height_stride * pattern2_firepos_height[i] + Pattern2_Prefab2.GetComponent<SpriteRenderer>().bounds.size.y / 2;
                fireballobj[i] = Instantiate(Pattern2_Prefab2, pos, transform.rotation);
                BossSoundManager_Rage sound = GameObject.Find("BossSoundManager_Rage").GetComponent<BossSoundManager_Rage>();
                sound.BossPlay("RAGEPATTERN2");
            }
           
        }
        else if (ran == 3 || ran == 4)
        {
            if (ran == 3)
            {
                speed.y = -(screen_height / 0.2f);
                pos.y += (Pattern2_Prefab1.GetComponent<SpriteRenderer>().bounds.size.y/2) + Pattern2_Prefab2.GetComponent<SpriteRenderer>().bounds.size.y;
            }
            else
            {
                speed.y = (screen_height / 0.2f);
                pos.y -= (Pattern2_Prefab1.GetComponent<SpriteRenderer>().bounds.size.y / 2 + Pattern2_Prefab2.GetComponent<SpriteRenderer>().bounds.size.y);
            }

            move_sum = screen_height * 2;
            for (int i = 0; i < 4; i++)
            {
                pos.x = pattern2_width_stride * pattern2_firepos_width[i] + Pattern2_Prefab2.GetComponent<SpriteRenderer>().bounds.size.x / 2;
                fireballobj[i] = Instantiate(Pattern2_Prefab2, pos, transform.rotation);
            }
        }
        //화염구 움직이기
        yield return null;
        for (int i = 0; i < 4; i++)
            fireballobj[i].GetComponent<RagePattern2>().SetSpeed(speed);

        yield return new WaitForSeconds(2.0f);
        state = RageBossState.rest;
        p2state = Pattern2State.ignore;
    }

    void RagePattern3_Update()
    {
        runTime += Time.deltaTime;
        if (p3state == Pattern3State.phase1)
        {
            p3pos = new UnityEngine.Vector3(0.0f, -screen_height, 0.0f);
            Instantiate(Pattern3_Prefab1, p3pos, this.transform.rotation);
            p3state = Pattern3State.phase2;
            runTime = 0.0f;
        }
        else if (p3state == Pattern3State.phase2 && runTime >= 1.0f)
        {
            p3pos.y = -screen_height * 2;
            p3pre = Instantiate(Pattern3_Prefab2, p3pos, this.transform.rotation);
            BossSoundManager_Rage sound = GameObject.Find("BossSoundManager_Rage").GetComponent<BossSoundManager_Rage>();
            sound.BossPlay("RAGEPATTERN3");
            UnityEngine.Vector3 temp = new UnityEngine.Vector3(0.0f, 0.0f, 0.0f);
            runTime= 0.0f;
            p3state= Pattern3State.phase3;
        }
        else if (p3state == Pattern3State.phase3 && p3pos.y < -(imagey / 2))
        {
            p3pos.y += speed * Time.deltaTime;
            p3temp.y = speed * Time.deltaTime;
            p3pre.transform.Translate(p3temp);
        }
        else if (p3state == Pattern3State.phase3 && p3pos.y >= -(imagey / 2))
        {
            p3state = Pattern3State.phase4;
            runTime = 0.0f;
        }
        else if(p3state == Pattern3State.phase4 && runTime >= 3.0f)
        {
            runTime= 0.0f;
            p3state= Pattern3State.phase5;
        }
        else if(p3state == Pattern3State.phase5 && p3pos.y >= ((-screen_height * 2) + imagey / 2 - 1.0f))
        {
            p3pos.y -= speed * Time.deltaTime;
            p3temp.y = -speed * Time.deltaTime;
            p3pre.transform.Translate(p3temp);
        }
        else if (p3state == Pattern3State.phase5 && p3pos.y < ((-screen_height * 2) + imagey / 2 - 1.0f))
        {
            runTime= 0.0f;
            state = RageBossState.rest;
            p3state = Pattern3State.ignore;
        }
    }
    public void Hit(int n)
    {
        HP-=n;
        BossHealthBar.value -= 0.1f * n;
    }
}