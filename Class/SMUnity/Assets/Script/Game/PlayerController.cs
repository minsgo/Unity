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

    //��ų ��ȯ
    enum Skillstatus { rage, sad, Void, delight }
    Skillstatus skillstatus = Skillstatus.rage;

    //�г� ����
    private bool isWeakAttack = false;      //����� ���ΰ�?
    private bool isStrongAttack = false;    //������ ���ΰ�?
    private bool isWeakAttackDelay = false;  //����� ������?
    private bool isStrongAttackDelay = false;//������ ������?
    private enum rageAttackState { Wait, Weak, Strong_move, Strong_Attack };
    rageAttackState ras = rageAttackState.Wait;

    public GameObject rageweakPrefab;
    public GameObject strongAttackPrefab1;
    public GameObject strongAttackPrefab2;

    //���� ��ų

    public GameObject Sad_yak;
    public Transform Sad_yak_pos;
    const float SadDelay = 0.5f;
    const float SadDelay2 = 2.5f;

    public GameObject Sad_gang;
    public Transform Sad_gang_pos;
    public Transform Sad_gang_pos2;
    bool isSadWeakAttackDelay = false;
    bool isSadStrongAttackDelay = false;

    //��� ��ų
    // ��ų ��� ���� ����
    bool isDelightSkill1delay = false;
    bool isDelightSkill2delay;
    //��ų ��Ÿ��
    float DelightSkill1Delay = 1f;
    float DelightSkill2Delay = 2f;
    //��ų ������
    float DelightSkill1Damage = 8f;
    //���� ����� �� ��ġ
    GameObject closestEnemy;
    //������ ���� ����
    public Transform Delight_pos;
    Vector2 Delight_boxSize = new Vector2(0.69f, 0.88f);

    //���� ��ų
    public GameObject blackBallP;
    const float VoidfireDelay = 0.5f;
    bool isVoidFireDelay1 = false;
    
    public GameObject blackBallH;
    const float VoidfireDelay2 = 1f;
    bool isVoidFireDelay2 = false;

    // Start is called before the first frame update
    void Start()
    {
        Skilluse[0] = false; //��ų ��� ���� üũ
        Skilluse[1] = false;
        Skilluse[2] = false;

        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        HP = MaxHP;

    }

    private void GetInput() //����, �̵�, ��ų
    {
        if (Input.GetButtonDown("Jump") && JumpCount > 0) //���� ���
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            SoundManager sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            sound.SoundPlay("JUMP");
            JumpCount--;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) //���� ������ ���
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
        //�׽�Ʈ�� �ڵ� ���� - �г�
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
            else if (skillstatus == Skillstatus.sad && !isSadWeakAttackDelay) //z-> ����
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
                // Ÿ�� ������ collider�� ����
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
                    Debug.Log("��Ÿ�� �Դϴ�.");

            }
            else if (skillstatus == Skillstatus.Void && !isVoidFireDelay1)
            {
                isVoidFireDelay1 = true;
                SoundManager sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                sound.SoundPlay("VOIDWEAK");
                StartCoroutine(VoidFireControll());
            }
        }
        if (Input.GetKeyDown(KeyCode.X)) //������ ��ų��
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
        //�׽�Ʈ�� �ڵ� �� - �г�
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

    //���� ��ų
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

    //�׽�Ʈ�� �ڵ� -�г�
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
            //������ ������
            //���� ���� ������ enemydeath = true; break;
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
                Debug.Log("��������");
                if (flipmove == Vector3.right)
                    rigid.velocity = new Vector2(Vector3.right.x * 5.0f, rigid.velocity.y);
                else
                    rigid.velocity = new Vector2(Vector3.left.x * 5.0f, rigid.velocity.y);
            }
        }
    }
    //�׽�Ʈ�� �ڵ� - �г�

    //��ų ���
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
    //����� �� ã��
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

    //��ų ����
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

        if (HP <= 0) //�׾��� ��
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
    void OnCollisionEnter2D(Collision2D collision) //�÷��̾� ���� �浹 �ǰ�
    {
        if (collision.gameObject.tag == "Enemy")
        {
            SoundManager sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            sound.SoundPlay("DAMAGED");
            OnDamaged();
        }
    }

    void OnDamaged() //�ǰ� ���� �� ����
    {
        gameObject.layer = 9;
        spriteRenderer.color = new Color32(255, 255, 255, 55);
        Invoke("OffDamaged", 3);
    }

    void OffDamaged() //�ǰ� �� ���� ���� ����
    {
        gameObject.layer = 8;
        spriteRenderer.color = new Color32(255, 255, 255, 255);
    }

    void Move() //�̵�
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > moveMax)  //���������� �̵� (+) , �ִ� �ӷ��� ������ 
        {
            rigid.velocity = new Vector2(moveMax, rigid.velocity.y); //�ش� ������Ʈ�� �ӷ��� maxSpeed 
        }
        //Max speed left
        else if (rigid.velocity.x < moveMax * (-1)) // �������� �̵� (-) 
        {
            rigid.velocity = new Vector2(moveMax * (-1), rigid.velocity.y); //y���� ������ �����̹Ƿ� 0���� ������ ��
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
        //���� ������ġ, ���� ���� , 1:distance , ( ���� ���� ������Ʈ�� Ư�� ���̾�� ���� ������� �� ��� ) // RaycastHit2D : Ray�� ���� ������Ʈ Ŭ���� 
        Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
        //rayHit�� ������ �´��� ó�� ���� ������Ʈ�� �������� ����(?) 
        if (rigid.velocity.y < 0)
        { // �پ�ö��ٰ� �Ʒ��� ������ ���� ���� �� 
            if (rayHit.collider != null)
            { //���� ���� ������Ʈ�� ������  -> ���������� collider�� ������������ 
                if (rayHit.distance < 1f)
                {
                    animator.SetBool("isJumping", false); //�Ÿ��� 1���� �۾����� ����
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
            Debug.Log("��ų ����");
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

