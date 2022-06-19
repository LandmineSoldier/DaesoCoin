using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewsManager : MonoBehaviour
{
    public static NewsManager Instance;

    [SerializeField] string[] falling; //������
    [SerializeField] string[] goUp; //������
    [SerializeField] string[] willFall; //��������
    [SerializeField] string[] willGoUp; //���󿹰�
    [SerializeField] string[] fallingLied; //��������
    [SerializeField] string[] goUpLied; //���󱸶�
    [SerializeField] string[] willDelisting; //������������
    [SerializeField] string[] delisting; //��������
    [SerializeField] string[] delistingLied; //������������
    [SerializeField] TMP_Text newsTitle;

    bool willFallBool = false;
    bool willGoUpBool = false;
    bool willDelistingBool = false;
    bool bigNewsPassed = false; //WillDo ������ �ٷ� �ٸ��ҽ� ����Ǵ� �� ����

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeTitle());
    }

    IEnumerator ChangeTitle()
    {
        WillDo();
        if (!bigNewsPassed)
            RandomNews();
        else
            bigNewsPassed = false;

        yield return new WaitForSecondsRealtime(5f);
        StartCoroutine(ChangeTitle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void WillDo()
    {
        if (willDelistingBool)
        {
            switch (Random.Range(0, 2))
            {
                case 0: //������������
                    newsTitle.text = delistingLied[Random.Range(0, delistingLied.Length)];
                    break;
                case 1: //��������
                    newsTitle.text = delisting[Random.Range(0, delisting.Length)];
                    GraphManager.Instance.value = 0;

                    if (PlayerManager.Instance.didLoan)
                    {
                        EndingManager.Instance.SetImageToDelistingAndNoMoney();
                    }
                    else
                    {
                        if (LobbyManager.Instance.money > 1000)
                            EndingManager.Instance.SetImageToHappyDelistingEnd();
                        else
                            EndingManager.Instance.SetImageToDelistingEnd();
                    }

                    break;
            }
            SoundManager.Instance.PlayNewsSound();
            ResetBool();
            bigNewsPassed = true;
        }
        else
        {
            if (willFallBool)
            {
                switch (Random.Range(0, 2))
                {
                    case 0: //��������
                        newsTitle.text = fallingLied[Random.Range(0, fallingLied.Length)];
                        break;
                    case 1: //������
                        newsTitle.text = falling[Random.Range(0, falling.Length)];
                        GraphManager.Instance.value -= GraphManager.Instance.value / Random.Range(1, 2);
                        if (PlayerManager.Instance.myStocks > 0)
                            SoundManager.Instance.PlayFallingSound();
                        break;
                }
                SoundManager.Instance.PlayNewsSound();
                ResetBool();
                bigNewsPassed = true;
            }
            else if (willGoUpBool)
            {
                switch (Random.Range(0, 2))
                {
                    case 0: //���󱸶�
                        newsTitle.text = goUpLied[Random.Range(0, goUpLied.Length)];
                        break;
                    case 1: //������
                        newsTitle.text = goUp[Random.Range(0, goUp.Length)];
                        GraphManager.Instance.value += GraphManager.Instance.value / Random.Range(1, 2);
                        if (PlayerManager.Instance.myStocks > 0)
                            SoundManager.Instance.PlayGoUpSound();
                        break;
                }
                SoundManager.Instance.PlayNewsSound();
                ResetBool();
                bigNewsPassed = true;
            }
        }
    }

    void RandomNews()
    {
        switch (Random.Range(0, 3))
        {
            case 0: //��������
                if (ItsNoIssue())
                {
                    newsTitle.text = willFall[Random.Range(0, willFall.Length)];
                    willFallBool = true;
                }
                SoundManager.Instance.PlayNewsSound();
                break;
            case 1: //���󿹰�
                if (ItsNoIssue())
                {
                    newsTitle.text = willGoUp[Random.Range(0, willGoUp.Length)];
                    willGoUpBool = true;
                }
                SoundManager.Instance.PlayNewsSound();
                break;
            case 2: //���ҽ�
                if (ItsNoIssue())
                {
                    newsTitle.text = "������ �Ϸ�";
                }
                break;
        }
    }

    bool ItsNoIssue()
    {
        return !willFallBool && !willGoUpBool && !willDelistingBool;
    }

    void ResetBool()
    {
        willDelistingBool = false;
        willFallBool = false;
        willGoUpBool = false;
    }

    public void WillDelisting()
    {
        if (Random.Range(0, 2) == 0)
        {
            newsTitle.text = willDelisting[Random.Range(0, willDelisting.Length)];
            willDelistingBool = true;
        }
    }
}
