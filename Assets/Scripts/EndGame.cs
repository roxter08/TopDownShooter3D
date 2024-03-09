using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame 
{
    public EndGame(CommonCallbacks commonCallbacks)
    {
        commonCallbacks.OnCharacterRemovedFromGame += OnPlayerKilled;
    }

    private void OnPlayerKilled(Character obj)
    {
        if(obj is Player)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
