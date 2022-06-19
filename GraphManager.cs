using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GraphManager : MonoBehaviour
{
    public static GraphManager Instance;
    [SerializeField] TMP_Text valueText;
    [SerializeField] Sprite circle;
    public int value;

    [SerializeField] RectTransform graphContainer;
    [SerializeField] Color up, down;
    [SerializeField] List<RectTransform> pointList;
    [SerializeField] List<GameObject> lineList;

    int[] valueList;
    float topValue = 0;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;

        valueList = new int[pointList.Count];
    }

    // Start is called before the first frame update
    void Start()
    {
        value = Random.Range(200, 600);
        valueText.text = value.ToString();

        int mul = Random.Range(0, 10);
        int highLow = Random.Range(0, 2);
        value += mul * (highLow == 0 ? -1 : 1) * (value != 0 ? 1 : 0);

        if (value < 0) value = 0;

        valueText.text = value.ToString();

        for (int i = 0; i < valueList.Length; i++)
        {
            valueList[i] = value;
        }

        StartCoroutine(ChangeValueLive());
    }

    IEnumerator ChangeValueLive()
    {
        int mul = Random.Range(0, 5);
        int highLow = Random.Range(0, 2);
        value += mul * (highLow == 0 ? -1 : 1);
        valueText.text = value.ToString();

        if (value < 10)
        {
            value = 50;
            NewsManager.Instance.WillDelisting();
        }

        for (int i = 0; i < valueList.Length - 1; i++)
        {
            valueList[i] = valueList[i + 1];
        }
        valueList[valueList.Length - 1] = value;

        topValue = valueList[valueList.Length - 1] > valueList[valueList.Length - 2] ? valueList[valueList.Length - 1] : valueList[valueList.Length - 2];
        UpdateGraph();

        yield return new WaitForSecondsRealtime(0.1f);
        StartCoroutine(ChangeValueLive());
    }

    private void UpdateGraph()
    {
        float graphHeight = graphContainer.sizeDelta.y;

        for (int i = 0; i < valueList.Length; i++)
        {
            float yPosition = 0;

            if (valueList[i] != 0)
            {
                float valuePercent = valueList[i] / topValue * 100;
                yPosition = graphHeight * valuePercent / 100;
            }
            ChangePointHeight(i, yPosition);
            if (i != 0)
                CreateLine(pointList[i - 1].anchoredPosition, pointList[i].anchoredPosition, i - 1);
        }
    }

    private void ChangePointHeight(int index, float height)
    {
        GameObject obj = pointList[index].gameObject;
        if (index != pointList.Count - 1)
        {
            if (pointList[index].anchoredPosition.y < pointList[index + 1].anchoredPosition.y)
                obj.GetComponent<Image>().color = up;
            else if (pointList[index].anchoredPosition.y > pointList[index + 1].anchoredPosition.y)
                obj.GetComponent<Image>().color = down;
            else
                obj.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        else if (index == pointList.Count - 1)
        {
            if (pointList[index - 1].anchoredPosition.y < pointList[index].anchoredPosition.y)
                obj.GetComponent<Image>().color = up;
            else if (pointList[index - 1].anchoredPosition.y > pointList[index].anchoredPosition.y)
                obj.GetComponent<Image>().color = down;
            else
                obj.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
        }

        RectTransform rt = obj.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, height);
    }

    private void CreateLine(Vector2 A, Vector2 B, int index)
    {
        GameObject obj = lineList[index];
        if (A.y < B.y)
            obj.GetComponent<Image>().color = up;
        else if (A.y > B.y)
            obj.GetComponent<Image>().color = down;
        else
            obj.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);

        RectTransform rt = obj.GetComponent<RectTransform>();
        Vector2 dir = (B - A).normalized;
        float distance = Vector2.Distance(A, B);
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(0, 0);
        rt.sizeDelta = new Vector2(distance, 10f);
        rt.anchoredPosition = A + dir * distance * 0.5f;
        //rt.anchoredPosition = (A + B) / 2;
        rt.localEulerAngles = new Vector3(0, 0, GetAngle(A,B));
    }
    float GetAngle(Vector2 start, Vector2 end)
    {
        Vector2 v2 = end - start;
        return Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
    }

}
