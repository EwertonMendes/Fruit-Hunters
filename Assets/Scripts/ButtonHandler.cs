using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour
{
    public Button firstSelectedButton;
    public EventSystem _eventSystem;
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void OnEnable()
    {
        // This is to resolve the issue of not selecting the restart Button after the game finishes
        if (firstSelectedButton != null)
        {
            StartCoroutine(SelectContinueButtonLater());
            firstSelectedButton.Select();
        }
    }

    // This is a workaround to get the select restart button issue to work
    IEnumerator SelectContinueButtonLater()
    {
        if (firstSelectedButton != null)
        {
            yield return null;
            _eventSystem.SetSelectedGameObject(null);
            _eventSystem.SetSelectedGameObject(firstSelectedButton.gameObject);
        }
    }
}
