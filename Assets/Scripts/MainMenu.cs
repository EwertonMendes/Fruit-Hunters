using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play("Main Theme");

        // Reseting the player selection when starting the game
        PlayerPrefs.SetString("p1Selection", null);
        PlayerPrefs.SetString("p2Selection", null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame(GameObject ConfirmBox)
    {
        ConfirmBox.SetActive(true);
    }
}
