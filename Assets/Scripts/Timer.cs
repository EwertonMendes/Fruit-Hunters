using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float currentTime = 0f;
    float startingTime = 120f;

    [SerializeField] TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (CountdownController.instance.canStartGame())
        {
            currentTime -= 1 * Time.deltaTime;
            timerText.text = TimeSpan.FromSeconds(currentTime).ToString("mm\\:ss");

            if (currentTime <= 0)
            {
                currentTime = 0;
                Time.timeScale = 0f;
            }
        }
    }
}
