using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    // Floating text가 이동할 방향
    [SerializeField] Vector3 moveSpeed = new Vector3(0,75,0);
    // Floating Text가 사라질 시간
    public float timeToFade = 1f;

    // GUI를 움직일 수 있는 Rect Transfrom;
    RectTransform textTransfrom;
    TextMeshProUGUI textMeshPro;

    private float timeElapsed = 0;
    private Color startColor;

    private void Awake()
    {
        textTransfrom = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;
    }

    // Update is called once per frame
    void Update()
    {
        textTransfrom.position += moveSpeed * Time.deltaTime;

        timeElapsed += Time.deltaTime;
        if (timeElapsed < timeToFade)
        {
            float newAlpha = startColor.a * (1 - (timeElapsed / timeToFade));
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b,newAlpha);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
