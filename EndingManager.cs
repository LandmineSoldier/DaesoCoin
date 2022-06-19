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
        reason.text = "빚을 제때 갚지 못하였습니다.";
        SoundManager.Instance.PlayLoanDeadSound();
        ChangeSceneToEnd();
    }
    public void SetImageToHappyDelistingEnd()
    {
        endingImage.sprite = happyDelistingEnd;
        earned.color = greatColor;
        reason.color = greatColor;
        reason.text = "상장폐지가 되기 전에 모든 스톡을 팔고 본전이상을 벌었습니다.";
        SoundManager.Instance.PlayDelistingHappySound();
        ChangeSceneToEnd();
    }
    public void SetImageToDelistingEnd()
    {
        endingImage.sprite = delistingEnd;
        earned.color = failColor;
        reason.color = failColor;
        reason.text = "상장폐지로 인해 돈을 잃었습니다.";
        SoundManager.Instance.PlayDelistingSound();
        ChangeSceneToEnd();
    }
    public void SetImageToDelistingAndNoMoney()
    {
        endingImage.sprite = delistingAndNoMoney;
        earned.color = failColor;
        reason.color = failColor;
        reason.text = "상장폐지로 인해 돈을 잃었고 빚만 남았습니다";
        SoundManager.Instance.PlayDelistingDeadSound();
        ChangeSceneToEnd();
    }
    public void ChangeSceneToEnd()
    {
        if (loaned)
            earned.text = "순 수익: " + (LobbyManager.Instance.money - PlayerManager.Instance.loanMoney - 1000).ToString("N0") + " \\";
        else
            earned.text = "순 수익: " + (LobbyManager.Instance.money - 1000).ToString("N0") + " \\";
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
