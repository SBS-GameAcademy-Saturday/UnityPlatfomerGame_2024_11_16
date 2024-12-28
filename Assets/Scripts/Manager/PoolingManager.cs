using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PoolingManager : MonoBehaviour
{
    // PoolingManager�� ������ �� �ֵ��� �ϴ� ���� Instance
    public static PoolingManager Instance;

    // Pooling�� ������Ʈ
    [SerializeField] private GameObject objectToPool;

    // �̸� ����� ���� ��
    [SerializeField] private int amountToPool = 10;

    // �̸� ����� ���� ��ü���� �����ϰ� �����ϴ� List
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
        //amountToPool���� �ݺ��Ѵ�.
        for (int i = 0; i < amountToPool; i++)
        {
            // ��ü�� �����Ѵ�.
            GameObject obj = Instantiate(objectToPool);
            // �⺻������ ó���� ������ ���� ���� ������� �ʱ� ������ ��ü�� ��Ȱ��ȭ �Ѵ�.
            obj.SetActive(false);
            //pooledObjects�� ���� ��ü�� �߰��Ѵ�.
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0;i < pooledObjects.Count; i++)
        {
            // �̹� ��� ���̸� ���� ��ü�� ã�´�.
            if (pooledObjects[i].activeInHierarchy) continue;

            pooledObjects[i].SetActive(true);
            // ����� ���ϴ� ��ü�� �ٷ� ��ȯ�Ѵ�.
            return pooledObjects[i];
        }

        // ���ο� ��ü�� �����Ѵ�.
        GameObject obj = Instantiate(objectToPool);

        obj.SetActive(true);
        //pooledObjects�� ���� ���ο� ��ü�� �߰��Ѵ�.
        pooledObjects.Add(obj);

        return obj;
    }


}
