using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    [SerializeField] TMP_Text myMoney, howManyStocksToBuyOrSell, myStocksText, loanText, maximum;
    [SerializeField] GameObject loanBtn;
    long howMany = 0;
    public long myStocks = 0;
    public long loanMoney = 0;
    byte interestRate = 0;
    public bool didLoan = false;
    int maximumHowMany = 200;

    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;

        LobbyManager.Instance.money = 1000;
        ChangeMoney(LobbyManager.Instance.money);
    }

    IEnumerator StartLoan()
    {
        if (didLoan)
        {
            interestRate += 1;
            loanText.text = "현재 이자율: " + interestRate + "% (총 " + (loanMoney + (long)(interestRate / 100f * loanMoney)).ToString("N0") + "\\)";
        }
        else
        {
            interestRate = 0;
            loanMoney = 0;
            loanText.text = "현재 이자율: " + interestRate + "% (총 " + (loanMoney + (long)(interestRate / 100f * loanMoney)).ToString("N0") + "\\)";
        }

        if (interestRate == 100)
        {
            EndingManager.Instance.SetImageToLoaned();
        }

        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(StartLoan());
    }

    // Start is called before the first frame update
    void Start()
    {
        maximum.text = maximumHowMany.ToString();
        StartCoroutine(StartLoan());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddStock()
    {
        howMany += 1;
        if (howMany > maximumHowMany) howMany = maximumHowMany;
        howManyStocksToBuyOrSell.text = howMany.ToString();
        SoundManager.Instance.PlayMore();
    }
    public void SumStock()
    {
        if (howMany > 0)
            howMany -= 1;
        howManyStocksToBuyOrSell.text = howMany.ToString();
        SoundManager.Instance.PlayLess();
    }
    public void BuyStock()
    {
        if (howMany + myStocks <= maximumHowMany)
        {
            if (LobbyManager.Instance.money < (howMany * GraphManager.Instance.value))
            {
                didLoan = true;
                loanBtn.SetActive(true);
                loanMoney = howMany * GraphManager.Instance.value;
                LobbyManager.Instance.money = 0;
                SoundManager.Instance.PlayLoanWarningSound();
            }
            else
                LobbyManager.Instance.money -= howMany * GraphManager.Instance.value;
            myStocks += howMany;
            myStocksText.text = myStocks.ToString();
            ChangeMoney(LobbyManager.Instance.money);
            ResetHowManyStock();
            SoundManager.Instance.PlayBuy();
        }
    }
    public void SellStock()
    {
        if (howMany <= myStocks)
        {
            LobbyManager.Instance.money += howMany * GraphManager.Instance.value;
            myStocks -= howMany;
            myStocksText.text = myStocks.ToString();
            ChangeMoney(LobbyManager.Instance.money);
            ResetHowManyStock();
            SoundManager.Instance.PlaySell();
        }
    }
    public void TwiceAsHowManyStocks()
    {
        howMany *= 2;
        if (howMany > maximumHowMany) howMany = maximumHowMany;
        howManyStocksToBuyOrSell.text = howMany.ToString();
        SoundManager.Instance.PlayTwice();
    }
    public void DivideAsHowManyStocks()
    {
        howMany /= 2;
        howManyStocksToBuyOrSell.text = howMany.ToString();
        SoundManager.Instance.PlayDivide();
    }

    public void SelectAllMyStock()
    {
        howMany = myStocks;
        howManyStocksToBuyOrSell.text = howMany.ToString();
        SoundManager.Instance.PlaySelectAllMyStocks();
    }
    public void SelectMaxStock()
    {
        howMany = maximumHowMany - myStocks;
        howManyStocksToBuyOrSell.text = howMany.ToString();
        SoundManager.Instance.PlayMaxStocksSound();
    }

    public void GiveLoanMoney()
    {
        if (LobbyManager.Instance.money >= loanMoney + (long)(interestRate / 100f * loanMoney))
        {
            LobbyManager.Instance.money -= loanMoney +  (long)(interestRate / 100f * loanMoney);
            ChangeMoney(LobbyManager.Instance.money);
            loanMoney = 0;
            interestRate = 0;
            didLoan = false;
            loanBtn.SetActive(false);
        }
    }

    public void ResetHowManyStock()
    {
        howMany = 0;
        howManyStocksToBuyOrSell.text = "얼마만큼";
    }

    private void ChangeMoney(long val)
    {
        myMoney.text = "\\ " + val.ToString("N0");
    }
}
