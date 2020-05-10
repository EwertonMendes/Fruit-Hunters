using System.Collections;
using TMPro;
using UnityEngine;

public class CountdownController : MonoBehaviour
{
    public static CountdownController instance;
    public int countdownTime;
    public TextMeshProUGUI countdownText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        
        StartCoroutine(CountdownStart());
    }

    IEnumerator CountdownStart()
    {
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(0.8f);
            countdownTime--;
        }

        countdownText.text = "GO!!!";

        yield return new WaitForSeconds(0.8f);

        countdownText.gameObject.SetActive(false);
    }

    public bool CanStartGame()
    {
        return countdownTime <= 0 ? true : false;
    }
}
