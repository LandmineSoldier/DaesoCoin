using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip buySound;
    [SerializeField] AudioClip sellSound;
    [SerializeField] AudioClip moreSound;
    [SerializeField] AudioClip lessSound;
    [SerializeField] AudioClip twiceSound;
    [SerializeField] AudioClip divideSound;
    [SerializeField] AudioClip selectAllMyStocksSound;
    [SerializeField] AudioClip maxStocksSound;
    [SerializeField] AudioClip[] fallingSound;
    [SerializeField] AudioClip goUpSound;
    [SerializeField] AudioClip loanWarningSound;
    [SerializeField] AudioClip loanDeadSound;
    [SerializeField] AudioClip delistingDeadSound;
    [SerializeField] AudioClip[] delistingSound;
    [SerializeField] AudioClip delistingHappySound;
    [SerializeField] AudioClip[] newsSound;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        audioSource = Instance.GetComponent<AudioSource>();
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

    public void PlayBuy()
    {
        audioSource.PlayOneShot(buySound);
    }
    public void PlaySell()
    {
        audioSource.PlayOneShot(sellSound);
    }
    public void PlayMore()
    {
        audioSource.PlayOneShot(moreSound);
    }
    public void PlayLess()
    {
        audioSource.PlayOneShot(lessSound);
    }
    public void PlayTwice()
    {
        audioSource.PlayOneShot(twiceSound);
    }
    public void PlayDivide()
    {
        audioSource.PlayOneShot(divideSound);
    }
    public void PlaySelectAllMyStocks()
    {
        audioSource.PlayOneShot(selectAllMyStocksSound);
    }
    public void PlayMaxStocksSound()
    {
        audioSource.PlayOneShot(maxStocksSound);
    }
    public void PlayFallingSound()
    {
        audioSource.PlayOneShot(fallingSound[Random.Range(0, fallingSound.Length)]);
    }
    public void PlayGoUpSound()
    {
        audioSource.PlayOneShot(goUpSound);
    }
    public void PlayLoanWarningSound()
    {
        audioSource.PlayOneShot(loanWarningSound);
    }
    public void PlayLoanDeadSound()
    {
        audioSource.PlayOneShot(loanDeadSound);
    }
    public void PlayDelistingDeadSound()
    {
        audioSource.PlayOneShot(delistingDeadSound);
    }
    public void PlayDelistingSound()
    {
        audioSource.PlayOneShot(delistingSound[Random.Range(0, delistingSound.Length)]);
    }
    public void PlayDelistingHappySound()
    {
        audioSource.PlayOneShot(delistingHappySound);
    }
    public void PlayNewsSound()
    {
        audioSource.PlayOneShot(newsSound[Random.Range(0, newsSound.Length)]);
    }
}
