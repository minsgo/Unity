using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderPositionAutoSetter : MonoBehaviour
{   
    [SerializeField]
    Vector3                 distance = Vector3.up*10.0f;
    Transform               targetTransform;
    RectTransform           rectTransform;

    public void Setup(Transform target){
        targetTransform = target;
        rectTransform = GetComponent<RectTransform>();
    }
    private void LateUpdate() {
        if(targetTransform == null){
            Destroy(gameObject);
            return;
        }

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        rectTransform.position = screenPosition + distance;    
    }
}
