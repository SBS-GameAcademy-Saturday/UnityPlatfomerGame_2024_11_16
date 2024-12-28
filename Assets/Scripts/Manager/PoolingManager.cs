using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PoolingManager : MonoBehaviour
{
    // PoolingManager를 접근할 수 있도록 하는 전역 Instance
    public static PoolingManager Instance;

    // Pooling할 오브젝트
    [SerializeField] private GameObject objectToPool;

    // 미리 만들어 놓을 수
    [SerializeField] private int amountToPool = 10;

    // 미리 만들어 놓은 객체들을 저장하고 관리하는 List
    [SerializeField] private List<GameObject> pooledObjects = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AddPooling();
    }

    private void AddPooling()
    {
        //amountToPool까지 반복한다.
        for (int i = 0; i < amountToPool; i++)
        {
            // 객체를 생성한다.
            GameObject obj = Instantiate(objectToPool);
            // 기본적으로 처음에 생성할 떄는 아직 사용하지 않기 때문에 객체를 비활성화 한다.
            obj.SetActive(false);
            //pooledObjects에 만든 객체를 추가한다.
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0;i < pooledObjects.Count; i++)
        {
            // 이미 사용 중이면 다음 객체를 찾는다.
            if (pooledObjects[i].activeInHierarchy) continue;

            pooledObjects[i].SetActive(true);
            // 사용을 안하는 객체는 바로 반환한다.
            return pooledObjects[i];
        }

        // 새로운 객체를 생성한다.
        GameObject obj = Instantiate(objectToPool);

        obj.SetActive(true);
        //pooledObjects에 만든 새로운 객체를 추가한다.
        pooledObjects.Add(obj);

        return obj;
    }


}
