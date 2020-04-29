using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Scripts.Classes;

public class ConfirmBox : MonoBehaviour
{

    public TextMeshProUGUI messageTextObject;
    public ActionsEnum action;
    public GameObject confirmBoxObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (messageTextObject != null)
        {
            switch (action)
            {
                case ActionsEnum.BackToMainMenu:
                    break;

                case ActionsEnum.RestartGame:
                    break;

                case ActionsEnum.QuitGame:
                    messageTextObject.text = "Are you sure you want to leave me?";
                    break;
            }
        }
    }

    public void YesAction()
    {
        switch(action)
        {
            case ActionsEnum.BackToMainMenu:
                break;

            case ActionsEnum.RestartGame:
                break;

            case ActionsEnum.QuitGame:
                Application.Quit();
                break;

        }
    }

    public void NoAction(GameObject buttonToResselect)
    {
        confirmBoxObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(buttonToResselect);
    }
}
