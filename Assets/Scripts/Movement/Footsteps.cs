using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{

    TopDownCharMovement tdcm;
    // Start is called before the first frame update
    void Start()
    {
        tdcm = GetComponent<TopDownCharMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(tdcm.targetVector = true && GetComponent<AudioSource>().isPlaying == false)
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
