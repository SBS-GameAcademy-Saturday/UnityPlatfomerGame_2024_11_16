using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class ProjectileLauncher : MonoBehaviour
{
    // 우리가 생성할 Projectile 프리팹 오브젝트
    public GameObject projectilePrefab;
    // 프리팹 생성 위치
    public Transform launchPoint;

    public void FireProjectile() 
    {
        // 프리팹 오브젝트를 게임 오브젝트로 생성
        GameObject projetile = Instantiate(projectilePrefab,launchPoint.position,projectilePrefab.transform.rotation);
        // 생성된 게임 오브젝트의 localScale 값을 가져온다.
        Vector3 originScale = projetile.transform.localScale;

        // 현재 플레이어 캐릭터가 왼쪽, 오른쪽을 바라보는지 확인하고 1 혹은 -1을 가져온다.
        float value = transform.localScale.x > 0 ? 1 : -1;
        // 생성된 게임 오브젝트의 원래 크기에서 value값을 추가 연산해서 localScale을 재설정한다.
        projetile.transform.localScale = new Vector3
        {
            x = originScale.x * value,
            y = originScale.y * value,
            z = originScale.z,
        };

    }

}
