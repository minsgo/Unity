using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelightSkill : MonoBehaviour
{   
    // 스킬 사용 가능 여부
    bool isSkill1delay = false;
    bool isSkill2delay;
    //스킬 쿨타임
    float Skill1Delay = 1f;
    float Skill2Delay = 2f;
    //스킬 데미지
    float Skill1Damage = 8f;
    //가장 가까운 적 위치
    GameObject closestEnemy;
    //데미지 범위 설정
    public Transform pos;
    public Vector2 boxSize;
    
    void Start()
    {
        
    }    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)){
            if(isSkill1delay == false){
                isSkill1delay = true;
                Skill1();
                // 타격 범위를 collider로 적용
                Collider2D[] colliders = Physics2D.OverlapBoxAll(pos.position,boxSize,0);
                foreach (Collider2D collider in colliders){
                    if(collider.tag == "Enemy"){
                        collider.GetComponent<EnemyHp>().TakeDamage(Skill1Damage);
                    }
                }
            }
            else Debug.Log("쿨타임 입니다.");
        }
        closestEnemy = FindClosestEnemy();

        //if(Input.GetKeyDonw(KeyCode.X)){
            //StartCoroutine(skill2Delay);

        //}
    }
    // 타격 범위 표시
    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position,boxSize);    
    }
    IEnumerator CoolTime (float cool){
        yield return new WaitForSeconds(cool);
        isSkill1delay = false;
    }
    //가까운 적 찾기
    GameObject FindClosestEnemy() {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos) {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = go;
                distance = curDistance;
            }
        }   
    return closest;
    }
    
    public void Skill1(){
        transform.position = new Vector2(closestEnemy.transform.position.x - 0.3f,closestEnemy.transform.position.y);
        StartCoroutine(CoolTime(Skill1Delay));
    }
    //public int Skill2(){

    //}
}