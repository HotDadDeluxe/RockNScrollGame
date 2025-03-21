using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Outro : MonoBehaviour
{
    public TextMeshProUGUI messageComponent;
    public TextMeshProUGUI nameComponent;
    public string[] dialogue;
    private string line;
    public float textSpeed;
    private int index;
    public Image rockmanImage;
    public Image bandmateImage;
    public Image producerImage;
    public Color highlightColor = Color.white;
    public Color dimColor = Color.gray;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        messageComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (messageComponent.text == line)
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                messageComponent.text = line;
            }
        }

    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        // parse the line to extract the name (all characters before ":") and message (after ": ")
        // and type it without textSpeed delay
        var parts = dialogue[index].Split(':');
        nameComponent.text = parts[0];
        line = parts[1];

        if (parts[0].Trim() == "ROCKMAN")
        {
            rockmanImage.color = highlightColor;
            bandmateImage.color = dimColor;
            producerImage.color = dimColor;
        }
        else if (parts[0].Trim() == "PRODUCER")
        {
            rockmanImage.color = dimColor;
            bandmateImage.color = dimColor;
            producerImage.color = highlightColor;
        }
        else if (parts[0].Trim() == "BANDMATE")
        {
            rockmanImage.color = dimColor;
            bandmateImage.color = highlightColor;
            producerImage.color = dimColor;
        }

        foreach (char c in line.ToCharArray())
        {
            messageComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            messageComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        GameManager.Instance.SetState(GameManager.GameState.LevelComplete);
        GameManager.Instance.SetState(GameManager.GameState.LevelSelection);
    }
}
