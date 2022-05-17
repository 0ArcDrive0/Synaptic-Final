using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("inkJSON")]
    [SerializeField] private TextAsset inkJSON;

 
    private bool playerInRange;

    public void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    public void Update()
    {
        if (playerInRange)
        {
            //Debug.Log("Advance");
            visualCue.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider plyr)
    {
        if (plyr.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider plyr)
    {
        if (plyr.gameObject.CompareTag("Player"))
        {
            playerInRange = false;

            
        }
    }
}