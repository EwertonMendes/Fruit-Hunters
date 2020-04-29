using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class FinishGame : MonoBehaviour
{
    public List<GameObject> objectsToDeactivate;
    public List<GameObject> objectsToActivate;

    private PlayerMovementHandler Player1;
    private Player2MovementHandler Player2;

    [SerializeField] TextMeshProUGUI WinnerText;
    
    // Start is called before the first frame update
    void Start()
    {
        // This is to make the game run properly when restarting
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameObject.FindGameObjectWithTag("Player1") != null)
        {
            Player1 = GameObject.FindGameObjectWithTag("Player1").GetComponentInChildren<PlayerMovementHandler>();
        }

        if (GameObject.FindGameObjectWithTag("Player2") != null)
        {
            Player2 = GameObject.FindGameObjectWithTag("Player2").GetComponentInChildren<Player2MovementHandler>();
        }
        
        if (GameObject.Find("TelaCanvas").GetComponent<Timer>().currentTime == 0)
        {
            GetComponent<AudioSource>().Stop();
            foreach(var obj in objectsToDeactivate)
            {
                obj.SetActive(false);
            }

            foreach (var obj in objectsToActivate)
            {
                obj.SetActive(true);
            }

            if (Player1.GetScore() > Player2.GetScore())
            {

                WinnerText.text = "Player 1 Wins";
                SetControlButtons();

            } else if (Player1.GetScore() < Player2.GetScore())
            {

                WinnerText.text = "Player 2 Wins";
                SetControlButtons();

            } else
            {
                WinnerText.text = "Draw";
                SetControlButtons();
            }
        }

       
    }

    void SetControlButtons()
    {
        EventSystem eventSystem = EventSystem.current;
        StandaloneInputModule inputModule = eventSystem.gameObject.GetComponent<StandaloneInputModule>();
        inputModule.ActivateModule();

        inputModule.horizontalAxis = "Horizontal Menu Navigation 2 Players";
        inputModule.verticalAxis = "Vertical Menu Navigation 2 Players";
        inputModule.submitButton = "Submit Menu Navigation 2 Players";
    }

    
}
