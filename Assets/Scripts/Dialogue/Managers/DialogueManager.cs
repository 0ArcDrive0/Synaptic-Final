using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]

    [SerializeField] private GameObject[] choices;

    private TextMeshProUGUI[] choicesText;

    Image[] diagImages;
    TMP_Text[] diagText;


    private Story currentStory;

    public bool dialogueIsPlaying { get; private set; }

   

    

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;

    }

    // Update is called once per frame
    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;

        diagImages = GameObject.Find("DialogueBox").GetComponentsInChildren<Image>();
        diagText = GameObject.Find("DialogueBox").GetComponentsInChildren<TMP_Text>();

        TurnOffDiag();
        //dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
            foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    public void Update()
    {
        //return right away if dialogue isnt playing
        if (!dialogueIsPlaying)
        {
            return;
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        if (!dialogueIsPlaying)
        {
            TurnOnDiag();
            currentStory = new Story(inkJSON.text);
            dialogueIsPlaying = true;
        }

        ContinueStory();
    }


    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialogueText.text = "";
        TurnOffDiag();


    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            Debug.Log(dialogueText);
            dialogueText.text = currentStory.Continue();
            //
            DisplayChoice();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    void TurnOffDiag()
    {
        foreach (TMP_Text text in diagText)
        {
            text.enabled = false;
        }

        foreach (Image image in diagImages)
        {
            image.enabled = false;
        }
    }

    void TurnOnDiag()
    {
        foreach (TMP_Text text in diagText)
        {
            text.enabled = true;
        }

        foreach (Image image in diagImages)
        {
            image.enabled = true;
        }
    }

    private void DisplayChoice()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("Too many choices given than the fixed amount that it can support"
                + currentChoices.Count);
        }
        int index = 0;
        //
        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        //Go through other choices and make sure they stay hidden
        for(int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
    }
}
