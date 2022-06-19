using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndingManager : MonoBehaviour
{
    public static EndingManager Instance;
    [SerializeField] Sprite loaned;
    [SerializeField] Sprite happyDelistingEnd;
    [SerializeField] Sprite delistingEnd;
    [SerializeField] Sprite delistingAndNoMoney;
    [SerializeField] Image endingImage;
    [SerializeField] Image endingImage2;
    [SerializeField] TMP_Text earned;
    [SerializeField] TMP_Text reason;
    [SerializeField] Color failColor;
    [SerializeField] Color greatColor;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetImageToLoaned()
    {
        endingImage.sprite = loaned;
        earned.color = failColor;
        reason.color = failColor;
        reason.text = "���� ���� ���� ���Ͽ����ϴ�.";
        SoundManager.Instance.PlayLoanDeadSound();
        ChangeSceneToEnd();
    }
    public void SetImageToHappyDelistingEnd()
    {
        endingImage.sprite = happyDelistingEnd;
        earned.color = greatColor;
        reason.color = greatColor;
        reason.text = "���������� �Ǳ� ���� ��� ������ �Ȱ� �����̻��� �������ϴ�.";
        SoundManager.Instance.PlayDelistingHappySound();
        ChangeSceneToEnd();
    }
    public void SetImageToDelistingEnd()
    {
        endingImage.sprite = delistingEnd;
        earned.color = failColor;
        reason.color = failColor;
        reason.text = "���������� ���� ���� �Ҿ����ϴ�.";
        SoundManager.Instance.PlayDelistingSound();
        ChangeSceneToEnd();
    }
    public void SetImageToDelistingAndNoMoney()
    {
        endingImage.sprite = delistingAndNoMoney;
        earned.color = failColor;
        reason.color = failColor;
        reason.text = "���������� ���� ���� �Ҿ��� ���� ���ҽ��ϴ�";
        SoundManager.Instance.PlayDelistingDeadSound();
        ChangeSceneToEnd();
    }
    public void ChangeSceneToEnd()
    {
        if (loaned)
            earned.text = "�� ����: " + (LobbyManager.Instance.money - PlayerManager.Instance.loanMoney - 1000).ToString("N0") + " \\";
        else
            earned.text = "�� ����: " + (LobbyManager.Instance.money - 1000).ToString("N0") + " \\";
        endingImage.gameObject.SetActive(true);
        SceneManager.LoadScene("End");
    }
    
    public void GoLobby()
    {
        endingImage.gameObject.SetActive(false);
        //LobbyManager.Instance.lobbyImage.SetActive(true);
        LobbyManager.Instance.money = 1000;
        SceneManager.LoadScene("Game");
    }
}
