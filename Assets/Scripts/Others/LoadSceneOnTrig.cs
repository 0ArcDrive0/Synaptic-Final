using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnTrig : MonoBehaviour
{
    public bool inCollider = false;
    public GameObject enterText;
    public string levelToLoad;

    void Start()
    {
        enterText.SetActive(false);
    }

    void Update() {
        if (inCollider) {
            enterText.SetActive(true);
            if (Input.GetButtonDown("Use"))
            {
                SceneManager.LoadScene(levelToLoad);
            }
        } else {
            enterText.SetActive(false);
        }
    }


    // Update is called once per frame
    void OnTriggerEnter(Collider plyr)
    {
        if (plyr.gameObject.tag == "Player")
        {
            inCollider = true;
        }
    }
    void OnTriggerExit(Collider plyr)
    {
        if (plyr.gameObject.tag == "Player")
        {
            inCollider = false;
        }
    }
}