using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using RPG.Controller;
using RPG.Movement;

namespace RPG.Cinematics { 
public class CinematicControlRemover : MonoBehaviour
{
        GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            player = GameObject.FindWithTag("Player");
            GetComponent<PlayableDirector>().played += DisabledControl;
            GetComponent<PlayableDirector>().stopped += EnabledControl;
    }

    // Update is called once per frame
    void EnabledControl(PlayableDirector pd)
    {
            player.GetComponent<PlayerController>().enabled = true;
        }
    void DisabledControl(PlayableDirector pd) 
    {
            player.GetComponent<PlayerController>().enabled = false;
        }
}
}