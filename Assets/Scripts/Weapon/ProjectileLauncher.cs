using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class ProjectileLauncher : MonoBehaviour
{
    // �츮�� ������ Projectile ������ ������Ʈ
    public GameObject projectilePrefab;
    // ������ ���� ��ġ
    public Transform launchPoint;

    public void FireProjectile() 
    {
        // ������ ������Ʈ�� ���� ������Ʈ�� ����
        GameObject projetile = Instantiate(projectilePrefab,launchPoint.position,projectilePrefab.transform.rotation);
        // ������ ���� ������Ʈ�� localScale ���� �����´�.
        Vector3 originScale = projetile.transform.localScale;

        // ���� �÷��̾� ĳ���Ͱ� ����, �������� �ٶ󺸴��� Ȯ���ϰ� 1 Ȥ�� -1�� �����´�.
        float value = transform.localScale.x > 0 ? 1 : -1;
        // ������ ���� ������Ʈ�� ���� ũ�⿡�� value���� �߰� �����ؼ� localScale�� �缳���Ѵ�.
        projetile.transform.localScale = new Vector3
        {
            x = originScale.x * value,
            y = originScale.y * value,
            z = originScale.z,
        };

    }

}
