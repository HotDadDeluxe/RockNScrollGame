using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI messageComponent;
    public TextMeshProUGUI nameComponent;
    public string[] dialogue;
    private string line;
    public float textSpeed;
    private int index;
    public TextMeshProUGUI corporateOption;
    public TextMeshProUGUI artisticOption;

    // Start is called before the first frame update
    void Start()
    {
        messageComponent.text = string.Empty;
        StartDielogue();

        corporateOption.gameObject.SetActive(false);
        artisticOption.gameObject.SetActive(false);
        corporateOption.GetComponent<Button>().onClick.AddListener(() => LoadScene("CorporateScene"));
        artisticOption.GetComponent<Button>().onClick.AddListener(() => LoadScene("ArtisticScene"));
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

    void StartDielogue()
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
            messageComponent.gameObject.SetActive(false);
            nameComponent.gameObject.SetActive(false);
            ShowOptions();
        }
    }

    void ShowOptions()
    {
        corporateOption.gameObject.SetActive(true);
        artisticOption.gameObject.SetActive(true);
    }

    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
