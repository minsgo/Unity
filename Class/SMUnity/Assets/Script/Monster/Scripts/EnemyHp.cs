using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    public float       maxHp;
    private float       currentHp;
    private bool        isDie = false;
    private Enemy       enemy;
    private SpriteRenderer sprite;

    public float        MaxHp => maxHp;
    public float        CurrentHp => currentHp;

    private void Awake() {
        currentHp = maxHp;
        enemy = GetComponent<Enemy>();
        sprite = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float damage){
        if(isDie) return;

        currentHp -= damage;
        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        if( currentHp <= 0){
            isDie = true;
            enemy.OnDie(); 
        }
    }

    private IEnumerator HitAlphaAnimation(){
        Color color = sprite.color;
        color.a = 0.4f;
        sprite.color = color;

        yield return new WaitForSeconds(0.05f);

        color.a = 1.0f;
        sprite.color = color;
    }
}
