using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController 
{
    private CommonCallbacks commonCallbacks;
    private HealthBarUIController healthBarUIController;
    private GameHUDController gameHUDController;
    public UIController(CommonCallbacks commonCallbacks, Transform uiRootTransform)
    {
        this.commonCallbacks = commonCallbacks;
        healthBarUIController = new HealthBarUIController(uiRootTransform.Find("WorldCanvas/HealthBarHolder"), commonCallbacks);
        gameHUDController = new GameHUDController(uiRootTransform.Find("ScreenCanvas/GameHUD"), commonCallbacks);
    }

    public void OnDestroy()
    {
        healthBarUIController.OnDestroy();  
        gameHUDController.OnDestroy();
    }
}
