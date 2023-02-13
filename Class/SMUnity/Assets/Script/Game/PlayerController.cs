using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    GameObject[] Emotions;
    bool[] hasEmotions;

    bool[] Skilluse = new bool[3];

    float jumpPower = 8f;
    public int HP = 100;
    public int MaxHP = 100;
    public int Attack = 10;
    public float moveMax;
    bool isdie = false;
    public Vector2 NowPos;
    int JumpCount = 2;
    Vector3 flipmove = Vector3.zero;

    //스킬 전환
    enum Skillstatus { rage, sad, Void, delight }
    Skillstatus skillstatus = Skillstatus.rage;

    //분노 변수
    private bool isWeakAttack = false;      //약공격 중인가?
    private bool isStrongAttack = false;    //강공격 중인가?
    private bool isWeakAttackDelay = false;  //약공격 딜레이?
    private bool isStrongAttackDelay = false;//강공격 딜레이?
    private enum rageAttackState { Wait, Weak, Strong_move, Strong_Attack };
    rageAttackState ras = rageAttackState.Wait;

    public GameObject rageweakPrefab;
    public GameObject strongAttackPrefab1;
    public GameObject strongAttackPrefab2;

    //슬픔 스킬

    public GameObject Sad_yak;
    public Transform Sad_yak_pos;
    const float SadDelay = 0.5f;
    const float SadDelay2 = 2.5f;

    public GameObject Sad_gang;
    public Transform Sad_gang_pos;
    public Transform Sad_gang_pos2;
    bool isSadWeakAttackDelay = false;
    bool isSadStrongAttackDelay = false;

    //기쁨 스킬
    // 스킬 사용 가능 여부
    bool isDelightSkill1delay = false;
    bool isDelightSkill2delay;
    //스킬 쿨타임
    float DelightSkill1Delay = 1f;
    float DelightSkill2Delay = 2f;
    //스킬 데미지
    float DelightSkill1Damage = 8f;
    //가장 가까운 적 위치
    GameObject closestEnemy;
    //데미지 범위 설정
    public Transform Delight_pos;
    Vector2 Delight_boxSize = new Vector2(0.69f, 0.88f);

    //공허 스킬
    public GameObject blackBallP;
    const float VoidfireDelay = 0.5f;
    bool isVoidFireDelay1 = false;
    
    public GameObject blackBallH;
    const float VoidfireDelay2 = 1f;
    bool isVoidFireDelay2 = false;

    // Start is called before the first frame update
    void Start()
    {
        Skilluse[0] = false; //스킬 사용 여부 체크
        Skilluse[1] = false;
        Skilluse[2] = false;

        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        HP = MaxHP;

    }

    private void GetInput() //점프, 이동, 스킬
    {
        if (Input.GetButtonDown("Jump") && JumpCount > 0) //점프 기능
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            SoundManager sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            sound.SoundPlay("JUMP");
            JumpCount--;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) //왼쪽 오른쪽 기능
        {
            rigid.velocity = new Vector2(0.5f * rigid.velocity.normalized.x, rigid.velocity.y);
            flipmove = Vector3.right;
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            flipmove = Vector3.left;
            transform.localScale = new Vector3(-1, 1, 1);

        }
        //테스트용 코드 시작 - 분노
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (skillstatus == Skillstatus.rage && !isWeakAttackDelay)
            {
                Vector3 parent = this.transform.position;
                if (flipmove == Vector3.right)
                    parent.x = parent.x + 0.45f;
                else
                    parent.x = parent.x - 0.45f;

                GameObject tempobj = Instantiate(rageweakPrefab, parent, transform.rotation);
                SoundManager sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                sound.SoundPlay("RAGEWEAK");
                if (flipmove == Vector3.left)
                {
                    Vector3 tempvec3 = tempobj.transform.localScale;
                    tempvec3.x *= -1;
                    tempobj.transform.localScale = tempvec3;
                }
                tempobj.transform.parent = transform;
                isWeakAttack = true;
                isWeakAttackDelay = true;
                StartCoroutine(RageWeakAttackDelay());
            }
            else if (skillstatus == Skillstatus.sad && !isSadWeakAttackDelay) //z-> 슬픔
            {
                isSadWeakAttackDelay = true;
                SoundManager sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                sound.SoundPlay("SADWEAK");
                StartCoroutine(SadControl());
            }
            else if (skillstatus == Skillstatus.delight && !isDelightSkill1delay)
            {
                closestEnemy = FindClosestEnemy();
                isDelightSkill1delay = true;
                DelightSkill1();
                SoundManager sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                sound.SoundPlay("DELIGHTWEAK");
                // 타격 범위를 collider로 적용
                Collider2D[] colliders = Physics2D.OverlapBoxAll(Delight_pos.position, Delight_boxSize, 0);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.tag == "Delight")
                    {
                        DelightBossDirector del_Damaged = GameObject.Find("DelightBossDirector").GetComponent<DelightBossDirector>();
                        del_Damaged.Hit(8);
                    }
                    else if (collider.tag == "Rage")
                    {
                        RageBossDirector rage_Damaged = GameObject.Find("RageBossDirector").GetComponent<RageBossDirector>();
                        rage_Damaged.Hit(8);
                    }
                    else if (collider.tag == "Sad")
                    {
                        SadBossDirector sad_Damaged = GameObject.Find("SadBossDirector").GetComponent<SadBossDirector>();
                        sad_Damaged.Hit(8);
                    }
                }
                    Debug.Log("쿨타임 입니다.");

            }
            else if (skillstatus == Skillstatus.Void && !isVoidFireDelay1)
            {
                isVoidFireDelay1 = true;
                SoundManager sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                sound.SoundPlay("VOIDWEAK");
                StartCoroutine(VoidFireControll());
            }
        }
        if (Input.GetKeyDown(KeyCode.X)) //강공격 스킬들
        {
            if (skillstatus == Skillstatus.rage && !isStrongAttackDelay)
            {
                isStrongAttack = true;
                isStrongAttackDelay = true;
                ras = rageAttackState.Strong_move;
                SoundManager sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                sound.SoundPlay("RAGESTRONG");
                StartCoroutine(RageStrongAttackMove());
            }
            else if (skillstatus == Skillstatus.sad && !isSadStrongAttackDelay)
            {
                isSadStrongAttackDelay = true;
                SoundManager sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                sound.SoundPlay("SADSTRONG");
                StartCoroutine(SadControl2());
            }
            else if (skillstatus == Skillstatus.Void && !isVoidFireDelay2)
            {
                isVoidFireDelay2 = true;
                SoundManager sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                sound.SoundPlay("VOIDSTRONG");
                StartCoroutine(VoidFire2Controll());
            }
        }
        //테스트용 코드 끝 - 분노
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            skillstatus = Skillstatus.Void;
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            skillstatus = Skillstatus.sad;
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            skillstatus = Skillstatus.delight;
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            skillstatus = Skillstatus.rage;
            
        }

    }

    //슬픔 스킬
    IEnumerator SadControl()
    {
        Instantiate(Sad_yak, Sad_yak_pos.position, Sad_yak_pos.rotation);
        yield return new WaitForSeconds(SadDelay);
        isSadWeakAttackDelay = false;
    }

    IEnumerator SadControl2()
    {
        gameObject.layer = 9;
        Instantiate(Sad_gang, Sad_gang_pos.position, Sad_gang_pos.rotation);
        Instantiate(Sad_gang, Sad_gang_pos2.position, Sad_gang_pos2.rotation);
        yield return new WaitForSeconds(SadDelay2);
        isSadStrongAttackDelay = false;
    }

    //테스트용 코드 -분노
    IEnumerator RageWeakAttackDelay()
    {
        yield return new WaitForSeconds(0.2f);
        isWeakAttack = false;
        isWeakAttackDelay = false;
    }
    IEnumerator RageStrongAttackDelay(bool success)
    {
        float Time = success ? 5.0f : 10.0f;
        yield return new WaitForSeconds(Time);
        isStrongAttack = false;
        isStrongAttackDelay = false;
    }

    IEnumerator RageStrongAttackMove()
    {
        //GetComponent<CapsuleCollider2D>().isTrigger = true;
        yield return new WaitForSeconds(2.0f);
        if (isStrongAttack && ras == rageAttackState.Strong_move)
        {
            ras = rageAttackState.Wait;
            isStrongAttack = false;
            StartCoroutine(RageStrongAttackDelay(false));
        }
    }

    IEnumerator RageStrongAttackAttack()
    {
        bool enemydeath = true;
        GameObject obj;
        for (int i = 0; i < 5; i++)
        {
            //적에게 데미지
            //만약 적이 죽으면 enemydeath = true; break;
            float f = (i - 1) * 1.0f + 1.0f;

            if (i % 2 == 1)
            {
                obj = Instantiate(strongAttackPrefab1, transform.position, Quaternion.identity);
                Transform trns = obj.transform.parent;
                obj.transform.localScale = new Vector3(f, f, 1.0f);
            }
            else
            {
                obj = Instantiate(strongAttackPrefab2, transform.position, Quaternion.identity);
                obj.transform.localScale = new Vector3(f, f, 1.0f);
            }

            yield return new WaitForSeconds(0.5f);
        }
        isStrongAttack = false;
        ras = rageAttackState.Wait;
        rigid.velocity = new Vector2(-flipmove.x * moveMax, rigid.velocity.y);
        StartCoroutine(RageStrongAttackDelay(enemydeath));
    }

    void RageStrongUpdate()
    {
        if (isStrongAttack)
        {
            if (ras == rageAttackState.Strong_move)
            {
                Debug.Log("강공격중");
                if (flipmove == Vector3.right)
                    rigid.velocity = new Vector2(Vector3.right.x * 5.0f, rigid.velocity.y);
                else
                    rigid.velocity = new Vector2(Vector3.left.x * 5.0f, rigid.velocity.y);
            }
        }
    }
    //테스트용 코드 - 분노

    //스킬 기쁨
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Delight_pos.position, Delight_boxSize);
    }
    IEnumerator CoolTime(float cool)
    {
        yield return new WaitForSeconds(cool);
        isDelightSkill1delay = false;
    }
    //가까운 적 찾기
    GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    public void DelightSkill1()
    {
        transform.position = new Vector2(closestEnemy.transform.position.x - 0.3f, closestEnemy.transform.position.y);
        StartCoroutine(CoolTime(DelightSkill1Delay));
    }

    //스킬 공허
    IEnumerator VoidFireControll()
    {
        Vector3 voidpos = this.transform.position;
        if (flipmove == Vector3.right)
            voidpos.x = voidpos.x + 0.45f;
        else
            voidpos.x = voidpos.x - 0.45f;


        GameObject obj1 = Instantiate(blackBallP, voidpos, Quaternion.identity);
        yield return null;
        if (obj1 != null)
        {
            if (flipmove == Vector3.right)
                obj1.GetComponent<Fire>().SetSpeed(1);
            else
                obj1.GetComponent<Fire>().SetSpeed(-1);
        }
        yield return new WaitForSeconds(VoidfireDelay);
        isVoidFireDelay1 = false;
    }

    IEnumerator VoidFire2Controll()
    {
        Vector3 voidpos = this.transform.position;
        if (flipmove == Vector3.right)
            voidpos.x = voidpos.x + 0.45f;
        else
            voidpos.x = voidpos.x - 0.45f;

        GameObject obj2 = Instantiate(blackBallH, voidpos, Quaternion.identity);
        yield return null;
        if (obj2 != null)
        {
            if (flipmove == Vector3.right)
                obj2.GetComponent<Fire2>().SetSpeed(1);
            else
                obj2.GetComponent<Fire2>().SetSpeed(-1);
        }
        yield return new WaitForSeconds(VoidfireDelay2);
        isVoidFireDelay2 = false;
    }


    // Update is called once per frame
    void Update()
    {

        GetInput();

        if (HP <= 0) //죽었을 때
        {
            isdie = true;
            Die();
        }

        ChangeSkill();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();

        if (HP == 0)
            return;
    }
    void OnCollisionEnter2D(Collision2D collision) //플레이어 몬스터 충돌 피격
    {
        if (collision.gameObject.tag == "Enemy")
        {
            SoundManager sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            sound.SoundPlay("DAMAGED");
            OnDamaged();
        }
    }

    void OnDamaged() //피격 판정 후 무적
    {
        gameObject.layer = 9;
        spriteRenderer.color = new Color32(255, 255, 255, 55);
        Invoke("OffDamaged", 3);
    }

    void OffDamaged() //피격 후 무적 판정 해제
    {
        gameObject.layer = 8;
        spriteRenderer.color = new Color32(255, 255, 255, 255);
    }

    void Move() //이동
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > moveMax)  //오른쪽으로 이동 (+) , 최대 속력을 넘으면 
        {
            rigid.velocity = new Vector2(moveMax, rigid.velocity.y); //해당 오브젝트의 속력은 maxSpeed 
        }
        //Max speed left
        else if (rigid.velocity.x < moveMax * (-1)) // 왼쪽으로 이동 (-) 
        {
            rigid.velocity = new Vector2(moveMax * (-1), rigid.velocity.y); //y값은 점프의 영향이므로 0으로 제한을 두
        }

        if (Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            animator.SetBool("isWalking", false);
        }
        else
            animator.SetBool("isWalking", true);
       

    }
    void Jump()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
        //빔의 시작위치, 빔의 방향 , 1:distance , ( 빔에 맞은 오브젝트를 특정 레이어로 한정 지어야할 때 사용 ) // RaycastHit2D : Ray에 닿은 오브젝트 클래스 
        Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
        //rayHit는 여러개 맞더라도 처음 맞은 오브젝트의 정보만을 저장(?) 
        if (rigid.velocity.y < 0)
        { // 뛰어올랐다가 아래로 떨어질 때만 빔을 쏨 
            if (rayHit.collider != null)
            { //빔을 맞은 오브젝트가 있을때  -> 맞지않으면 collider도 생성되지않음 
                if (rayHit.distance < 1f)
                {
                    animator.SetBool("isJumping", false); //거리가 1보다 작아지면 변경
                    JumpCount = 2;
                }
                else
                    animator.SetBool("isJumping", true);

                
            }
        }
    } 
   void ChangeSkill()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("스킬 변경");
            animator.SetBool("isChange", true);
        }
        else if (Input.GetKeyDown(KeyCode.E))
            animator.SetBool("isChange", false);
    }

    void Die()
    {
        Debug.Log("die");
        SceneManager.LoadScene("GameOver");
    }
    public void SetSkillbool(int n)
    {
        Skilluse[n] = true;
    }

}

