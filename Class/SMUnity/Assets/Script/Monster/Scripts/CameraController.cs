using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 카메라 조절
    public float cameraSpeed;

    public Transform target;
    // 카메라 제한 범위
    public Vector2 center;
    public Vector2 size;
    float height;
    float width;

    void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    void Update()
    {
        
    }
    // 카메라 제한 범위 표시
    private void DrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center,size);

    }
    void LateUpdate(){
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * cameraSpeed);
        // transform.position = new Vector3(transform.position.x,transform.position.y, -2f); 

        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x , -lx + center.x , lx + center.x);

        float ly = size.x * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y , -ly + center.y , ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);

    }
}
