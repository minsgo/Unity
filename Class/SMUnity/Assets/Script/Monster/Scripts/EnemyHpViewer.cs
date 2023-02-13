using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHpViewer : MonoBehaviour
{
    private EnemyHp enemyHp;
    private Slider  hpSlider;
    // Start is called before the first frame update
    public void Setup(EnemyHp enemyHp){
        this.enemyHp = enemyHp;
        hpSlider     = GetComponent<Slider>();
    }

    // Update is called once per frame
    private void Update() {
        hpSlider.value = enemyHp.CurrentHp / enemyHp.MaxHp; 
    }
}
