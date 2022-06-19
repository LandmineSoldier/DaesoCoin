using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewsManager : MonoBehaviour
{
    public static NewsManager Instance;

    [SerializeField] string[] falling; //떡락중
    [SerializeField] string[] goUp; //떡상중
    [SerializeField] string[] willFall; //떡락예고
    [SerializeField] string[] willGoUp; //떡상예고
    [SerializeField] string[] fallingLied; //떡락구라
    [SerializeField] string[] goUpLied; //떡상구라
    [SerializeField] string[] willDelisting; //상장폐지예고
    [SerializeField] string[] delisting; //상장폐지
    [SerializeField] string[] delistingLied; //상장폐지구라
    [SerializeField] TMP_Text newsTitle;

    bool willFallBool = false;
    bool willGoUpBool = false;
    bool willDelistingBool = false;
    bool bigNewsPassed = false; //WillDo 끝나고 바로 다른소식 적용되는 것 방지

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
                case 0: //상장폐지구라
                    newsTitle.text = delistingLied[Random.Range(0, delistingLied.Length)];
                    break;
                case 1: //상장폐지
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
                    case 0: //떡락구라
                        newsTitle.text = fallingLied[Random.Range(0, fallingLied.Length)];
                        break;
                    case 1: //떡락중
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
                    case 0: //떡상구라
                        newsTitle.text = goUpLied[Random.Range(0, goUpLied.Length)];
                        break;
                    case 1: //떡상중
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
            case 0: //떡락예고
                if (ItsNoIssue())
                {
                    newsTitle.text = willFall[Random.Range(0, willFall.Length)];
                    willFallBool = true;
                }
                SoundManager.Instance.PlayNewsSound();
                break;
            case 1: //떡상예고
                if (ItsNoIssue())
                {
                    newsTitle.text = willGoUp[Random.Range(0, willGoUp.Length)];
                    willGoUpBool = true;
                }
                SoundManager.Instance.PlayNewsSound();
                break;
            case 2: //무소식
                if (ItsNoIssue())
                {
                    newsTitle.text = "무난한 하루";
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
