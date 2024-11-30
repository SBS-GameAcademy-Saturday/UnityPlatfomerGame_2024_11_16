using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    // 카메라
    public Camera cam;
    // 플레이어 위치
    public Transform followTarget;

    // 현재 나의 시작 위치
    Vector2 startingPosition;

    // 현재 게임 오브젝트의 위치의 Z 값
    float startingZ = 0;

    // 게임이 시작되고나서 카메라가 움직이는 방향
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;


    // 현재 Z 축 위치에서 follow 타겟의 Z 축 값을 뺸다.
    float ZDistanceFromTarget => transform.position.z - followTarget.transform.position.z; // -0.24

    // 카메라와의 거리값을 기준으로 Plane 값을 가져온다.
    // - 10
    float clippingPlane => (cam.transform.position.z + ZDistanceFromTarget) > 0 ? cam.farClipPlane : cam.nearClipPlane;

    // Z 간의 거리를 기반으로 Factor를 구현한다.
    // 0.24 / 0.1;
    private float parallaxFactor => Mathf.Abs(ZDistanceFromTarget) / clippingPlane;

    //[SerializeField] private float parallaxFactor = -1;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // LateUpdate 혹은 Update에서는 호출주기가 일정하지 않아서 이동 로직을 처리하는 데 
    // 충돌 처리등 물리연산이 주기적으로 이루어지지 않아 떨림 현상이 발생한다.
    // 따라서 FixedUpdate를 통해서 일정 주기로 물리 연산(이동, 출동 등)를 진행하여 떨림현상을 방지할 수 있다. 
    void FixedUpdate()
    {
        // 0 + 5 / (Factor)
        Vector2 newPosition = startingPosition + camMoveSinceStart / parallaxFactor;

        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
