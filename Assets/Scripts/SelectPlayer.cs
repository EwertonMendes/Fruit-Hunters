using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectPlayer : MonoBehaviour
{
    public GameObject p1PositionSet;
    public GameObject p2PositionSet;

    private List<GameObject> p1Chars;
    private List<GameObject> p2Chars;

    private string p1CharName;
    private string p2CharName;

    private readonly List<Vector3> p1OptionsInitialPositions = new List<Vector3>();
    private readonly List<Vector3> p2OptionsInitialPositions = new List<Vector3>();

    private Color32 highlightColor = new Color32(255, 255, 255, 255);
    private Color32 unselectedColor = new Color32(164, 164, 164, 255);

    private int p1Index = 0; // Index to access the Selectable Characters for Player 2
    private int p2Index = 0; // Index to access the Selectable Characters for Player 1

    private string p1State = "Selecting"; // The Player 1 state { Selecting or Ready }
    private string p2State = "Selecting"; // The Player 1 state { Selecting or Ready }

    private bool isP1HorizontalAxisInUse = false;
    private bool isP1VerticalAxisInUse = false;
    private bool isP2HorizontalAxisInUse = false;
    private bool isP2VerticalAxisInUse = false;
    void Start()
    {
        p1Chars = GameObject.FindGameObjectsWithTag("P1Selectables").OrderBy(p1 => p1.name).ToList();
        p2Chars = GameObject.FindGameObjectsWithTag("P2Selectables").OrderBy(p2 => p2.name).ToList();

        foreach (var ca in p1Chars)
        {
            p1OptionsInitialPositions.Add(ca.GetComponentInChildren<Transform>().localPosition);
        }

        foreach (var ca in p2Chars)
        {
            p2OptionsInitialPositions.Add(ca.GetComponentInChildren<Transform>().localPosition);
        }

        // Sets the first selectable character name 
        p1CharName = p1Chars[0].name;
        p2CharName = p2Chars[0].name;

        HighlightChar(p1Chars[0]); // Highlights the first Character of the list of Player 1
        HighlightChar(p2Chars[0]); // Highlights the first Character of the list of Player 2
    }

    void Update()
    {
        if(p1State == "Selecting") {

            float horizontalAxis = Input.GetAxisRaw("Horizontal");
            float verticalAxis = Input.GetAxisRaw("Vertical");

            if (horizontalAxis == -1)
            {
                if (isP1HorizontalAxisInUse == false)
                {
                    if (p1Index > 0)
                    {
                        p1Index--;
                        HighlightChar(p1Chars[p1Index]);
                        p1CharName = p1Chars[p1Index].name;
                        var buttonsToUnselect = new List<GameObject>();
                        buttonsToUnselect.AddRange(p1Chars.Where(c => c != p1Chars[p1Index]));
                        UnhighlightChars(buttonsToUnselect);
                    }
                    isP1HorizontalAxisInUse = true;
                }
                
            }

            if (horizontalAxis == 1)
            {
                if (isP1HorizontalAxisInUse == false)
                {
                    if (p1Index >= 0 && p1Index < p1Chars.Count - 1)
                    {
                        p1Index++;
                        HighlightChar(p1Chars[p1Index]);
                        p1CharName = p1Chars[p1Index].name;
                        var buttonsToUnselect = new List<GameObject>();
                        buttonsToUnselect.AddRange(p1Chars.Where(c => c != p1Chars[p1Index]));
                        UnhighlightChars(buttonsToUnselect);
                    }
                    isP1HorizontalAxisInUse = true;
                }
            }

            if (horizontalAxis == 0)
            {
                isP1HorizontalAxisInUse = false;
            }

            if (verticalAxis == -1)
            {
                if (isP1VerticalAxisInUse == false)
                {
                    if (p1Index >= 0)
                    {
                        p1Index = p1Index + 2 <= p1Chars.Count - 1 ? p1Index += 2 : p1Index;
                        HighlightChar(p1Chars[p1Index]);
                        p1CharName = p1Chars[p1Index].name;
                        var buttonsToUnselect = new List<GameObject>();
                        buttonsToUnselect.AddRange(p1Chars.Where(c => c != p1Chars[p1Index]));
                        UnhighlightChars(buttonsToUnselect);
                    }
                    isP1VerticalAxisInUse = true;
                }
            }

            if (verticalAxis == 1)
            {
                if (isP1VerticalAxisInUse == false)
                {
                    if (p1Index >= 0)
                    {
                        p1Index = p1Index - 2 >= 0 ? p1Index -= 2 : p1Index;
                        HighlightChar(p1Chars[p1Index]);
                        p1CharName = p1Chars[p1Index].name;
                        var buttonsToUnselect = new List<GameObject>();
                        buttonsToUnselect.AddRange(p1Chars.Where(c => c != p1Chars[p1Index]));
                        UnhighlightChars(buttonsToUnselect);
                    }
                    isP1VerticalAxisInUse = true;
                }
            }

            if (verticalAxis == 0)
            {
                isP1VerticalAxisInUse = false;
            }

            if (Input.GetButtonDown("Submit"))
            {
                p1State = "Ready";
                p1Chars[p1Index].transform.position = new Vector3(Mathf.Lerp(p1Chars[p1Index].transform.position.x, p1PositionSet.transform.position.x, 800 * Time.deltaTime), Mathf.Lerp(p1Chars[p1Index].transform.position.y, p1PositionSet.transform.position.y, 800 * Time.deltaTime), p1PositionSet.transform.position.z);
                var buttonsToUnselect = new List<GameObject>();
                buttonsToUnselect.AddRange(p1Chars.Where(c => c != p1Chars[p1Index]));
                DeactivateChars(buttonsToUnselect);
            }
        }

        if (p1State == "Ready")
        {
            if (Input.GetButtonDown("Cancel"))
            {
                p1Chars[p1Index].transform.position = p1OptionsInitialPositions[p1Index];
                p1State = "Selecting";
                foreach (var c in p1Chars)
                {
                    c.SetActive(true);
                }
            }   
        }

        if (p2State == "Selecting")
        {
            float horizontalAxis = Input.GetAxisRaw("Horizontal2");
            float verticalAxis = Input.GetAxisRaw("Vertical2");

            if (horizontalAxis == -1)
            {
                if (isP2HorizontalAxisInUse == false)
                {
                    if (p2Index > 0)
                    {
                        if (p2Index == 0)
                            return;

                        p2Index--;
                        HighlightChar(p2Chars[p2Index]);
                        p2CharName = p2Chars[p2Index].name;
                        var buttonsToUnselect = new List<GameObject>();
                        buttonsToUnselect.AddRange(p2Chars.Where(c => c != p2Chars[p2Index]));
                        UnhighlightChars(buttonsToUnselect);
                    }
                    isP2HorizontalAxisInUse = true;
                }

            }

            if (horizontalAxis == 1)
            {
                if (isP2HorizontalAxisInUse == false)
                {
                    if (p2Index >= 0 && p2Index < p2Chars.Count - 1)
                    {
                        p2Index++;
                        HighlightChar(p2Chars[p2Index]);
                        p2CharName = p2Chars[p2Index].name;
                        var buttonsToUnselect = new List<GameObject>();
                        buttonsToUnselect.AddRange(p2Chars.Where(c => c != p2Chars[p2Index]));
                        UnhighlightChars(buttonsToUnselect);
                    }
                    isP2HorizontalAxisInUse = true;
                }
            }

            if (horizontalAxis == 0)
            {
                isP2HorizontalAxisInUse = false;
            }

            if (verticalAxis == -1)
            {
                if (isP2VerticalAxisInUse == false)
                {
                    if (p2Index >= 0)
                    {
                        p2Index = p2Index + 2 <= p2Chars.Count - 1 ? p2Index += 2 : p2Index;
                        HighlightChar(p2Chars[p2Index]);
                        p2CharName = p2Chars[p2Index].name;
                        var buttonsToUnselect = new List<GameObject>();
                        buttonsToUnselect.AddRange(p2Chars.Where(c => c != p2Chars[p2Index]));
                        UnhighlightChars(buttonsToUnselect);
                    }
                    isP2VerticalAxisInUse = true;
                }
            }

            if (verticalAxis == 1)
            {
                if (isP2VerticalAxisInUse == false)
                {
                    if (p2Index >= 0)
                    {
                        p2Index = p2Index - 2 >= 0 ? p2Index -= 2 : p2Index;
                        HighlightChar(p2Chars[p2Index]);
                        p2CharName = p2Chars[p2Index].name;
                        var buttonsToUnselect = new List<GameObject>();
                        buttonsToUnselect.AddRange(p2Chars.Where(c => c != p2Chars[p2Index]));
                        UnhighlightChars(buttonsToUnselect);
                    }
                    isP2VerticalAxisInUse = true;
                }
            }

            if (verticalAxis == 0)
            {
                isP2VerticalAxisInUse = false;
            }

            if (Input.GetButtonDown("Submit2"))
            {
                p2State = "Ready";
                p2Chars[p2Index].transform.position = p2PositionSet.transform.position;
                var buttonsToUnselect = new List<GameObject>();
                buttonsToUnselect.AddRange(p2Chars.Where(c => c != p2Chars[p2Index]));
                DeactivateChars(buttonsToUnselect);
            }
        }

        if (p2State == "Ready")
        {
            if (Input.GetButtonDown("Cancel2"))
            {
                p2Chars[p2Index].transform.position = p2OptionsInitialPositions[p2Index];
                p2State = "Selecting";
                foreach (var c in p2Chars)
                {
                    c.SetActive(true);
                }
            }
        }

        if(p1State == "Ready" && p2State == "Ready")
        {
            PlayerPrefs.SetString("p1Selection", p1CharName);
            PlayerPrefs.SetString("p2Selection", p2CharName);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void HighlightChar(GameObject character)
    {
        character.GetComponent<SpriteRenderer>().color = highlightColor;
        var charNameText = character.GetComponentInChildren<TextMeshProUGUI>();
        charNameText.text = character.name;
    }

    void UnhighlightChars(List<GameObject> character)
    {
        foreach(var c in character)
        {
            c.GetComponent<SpriteRenderer>().color = unselectedColor;
            var charNameText = c.GetComponentInChildren<TextMeshProUGUI>();
            charNameText.text = "";
        }
        
    }

    void DeactivateChars(List<GameObject> character)
    {
        foreach (var c in character)
        {
            c.SetActive(false);
        }
    }
}
