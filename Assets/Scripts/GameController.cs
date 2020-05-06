using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public GameObject[] playerList;
    public Transform player1SpawPoint;
    public Transform player2SpawnPoint;
    public GameObject playerLightPrefab;
    public Color32 secondaryColor = new Color32(255, 146, 233, 255);
    public GameObject pauseScreen;
    private bool isPausedP1 = false;
    private bool isPausedP2 = false;
    EventSystem eventSystem;
    StandaloneInputModule inputModule;
    private bool hasGameStarted = false;
    [SerializeField] TextMeshProUGUI p1Score;
    [SerializeField] TextMeshProUGUI p2Score;

    void Start()
    {
        //Sets the score to 0 at the start of the game, before the players were instantiated
        p1Score.text = "P1 - " + PlayerPrefs.GetString("p1Selection") + ": 0";
        p2Score.text = "P2 - " + PlayerPrefs.GetString("p2Selection") + ": 0";
    }

    // Update is called once per frame
    void Update()
    {
        if(CountdownController.instance.canStartGame() && hasGameStarted == false)
        {
            BeginGame();
            hasGameStarted = true;
        }
        

        Pause();
    }

    void Pause()
    {
        if(Input.GetButtonDown("Pause1"))
        {
            if (isPausedP1 && !isPausedP2)
            {
                pauseScreen.SetActive(false);
                isPausedP1 = false;
                Time.timeScale = 1f;

            } else if (!isPausedP1 && !isPausedP2)
            {
                ConfigMenuButtons("Horizontal", "Vertical", "Submit", "Cancel");
                pauseScreen.SetActive(true);
                isPausedP1 = true;
                Time.timeScale = 0f;
            }
        } else if (Input.GetButtonDown("Pause2"))
        {
            if (isPausedP2 && !isPausedP1)
            {
                pauseScreen.SetActive(false);
                isPausedP2 = false;
                Time.timeScale = 1f;

            }
            else if (!isPausedP2 && !isPausedP1)
            {
                ConfigMenuButtons("Horizontal2", "Vertical2", "Submit2", "Cancel2");
                pauseScreen.SetActive(true);
                isPausedP2 = true;
                Time.timeScale = 0f;
            }
        }
    }

    void BeginGame()
    {
        eventSystem = EventSystem.current;
        inputModule = eventSystem.gameObject.GetComponent<StandaloneInputModule>();
        inputModule.ActivateModule();

        var player1Name = !string.IsNullOrEmpty(PlayerPrefs.GetString("p1Selection")) ? PlayerPrefs.GetString("p1Selection") : "Crazy Doc";
        var player2Name = !string.IsNullOrEmpty(PlayerPrefs.GetString("p2Selection")) ? PlayerPrefs.GetString("p2Selection") : "Crazy Doc";
       
        GameObject player1 = Instantiate(Array.Find(playerList, p1 => p1.name == player1Name), player1SpawPoint);
        GameObject player2 = Instantiate(Array.Find(playerList, p2 => p2.name == player2Name), player2SpawnPoint);

        if (player1Name == player2Name)
        {
            player2.GetComponent<SpriteRenderer>().color = secondaryColor;
        }

        if (playerLightPrefab != null)
        {
            GameObject player1Light = Instantiate(playerLightPrefab, player1.transform);
            GameObject player2Light = Instantiate(playerLightPrefab, player2.transform);

            player1Light.transform.parent = player1.transform;
            player2Light.transform.parent = player2.transform;
        }

        player1.AddComponent<PlayerMovementHandler>();
        player1.AddComponent<PlayerAnimationHandler>();

        player2.AddComponent<Player2MovementHandler>();
        player2.AddComponent<Player2AnimationHandler>();

        player2.GetComponent<SpriteRenderer>().flipX = true;
        player2.GetComponent<SpriteRenderer>().sortingOrder -= 1;
    }

    public bool GetIsPaused()
    {
        if(isPausedP1 || isPausedP2)
        {
            return true;
        }

        return false;
    }

    void ConfigMenuButtons(string horizontalAxis, string verticalAxis, string submit, string cancel)
    {
        inputModule.horizontalAxis = horizontalAxis;
        inputModule.verticalAxis = verticalAxis;
        inputModule.submitButton = submit;
        inputModule.cancelButton = cancel;

    }
}
