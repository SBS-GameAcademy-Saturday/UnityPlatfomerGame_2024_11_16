using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    // ī�޶�
    public Camera cam;
    // �÷��̾� ��ġ
    public Transform followTarget;

    // ���� ���� ���� ��ġ
    Vector2 startingPosition;

    // ���� ���� ������Ʈ�� ��ġ�� Z ��
    float startingZ = 0;

    // ������ ���۵ǰ��� ī�޶� �����̴� ����
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;


    // ���� Z �� ��ġ���� follow Ÿ���� Z �� ���� �A��.
    float ZDistanceFromTarget => transform.position.z - followTarget.transform.position.z; // -0.24

    // ī�޶���� �Ÿ����� �������� Plane ���� �����´�.
    // - 10
    float clippingPlane => (cam.transform.position.z + ZDistanceFromTarget) > 0 ? cam.farClipPlane : cam.nearClipPlane;

    // Z ���� �Ÿ��� ������� Factor�� �����Ѵ�.
    // 0.24 / 0.1;
    private float parallaxFactor => Mathf.Abs(ZDistanceFromTarget) / clippingPlane;

    //[SerializeField] private float parallaxFactor = -1;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // LateUpdate Ȥ�� Update������ ȣ���ֱⰡ �������� �ʾƼ� �̵� ������ ó���ϴ� �� 
    // �浹 ó���� ���������� �ֱ������� �̷������ �ʾ� ���� ������ �߻��Ѵ�.
    // ���� FixedUpdate�� ���ؼ� ���� �ֱ�� ���� ����(�̵�, �⵿ ��)�� �����Ͽ� ���������� ������ �� �ִ�. 
    void FixedUpdate()
    {
        // 0 + 5 / (Factor)
        Vector2 newPosition = startingPosition + camMoveSinceStart / parallaxFactor;

        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
