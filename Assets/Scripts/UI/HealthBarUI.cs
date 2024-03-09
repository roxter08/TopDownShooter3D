using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI
{
    private Character targetCharacter;
    private GameObject healthBar;
    private Camera targetCamera;
    private RectTransform healthBarRectTransform;

    private Image fillBar;

    public HealthBarUI(GameObject healthBar, Character targetCharacter, Camera targetCamera)
    {
        this.targetCharacter = targetCharacter;
        this.healthBar = healthBar;
        this.targetCamera = targetCamera;

        healthBarRectTransform = healthBar.GetComponent<RectTransform>();
        fillBar = healthBarRectTransform.Find("Fill").GetComponent<Image>();

        CinemachineCore.CameraUpdatedEvent.AddListener(FollowTargetCharacter);

        targetCharacter.OnHealthUpdated += UpdateHealthBar;
    }

    private void UpdateHealthBar()
    {
        fillBar.fillAmount = targetCharacter.GetNormalizedHealth();
    }

    private void FollowTargetCharacter(CinemachineBrain cinemachineBrain)
    {
        //Position
        healthBarRectTransform.position = targetCharacter.HealthPoint.position;
        //healthBarRectTransform.position = targetCamera.WorldToScreenPoint(targetCharacter.HealthPoint.position);

        //Rotation
        Vector3 targetEuler = targetCharacter.HealthPoint.rotation.eulerAngles;
        healthBarRectTransform.localRotation = Quaternion.Euler(0,0,-targetEuler.y);
    }

    public void Destroy()
    {
        targetCharacter.OnHealthUpdated -= UpdateHealthBar;
        CinemachineCore.CameraUpdatedEvent.RemoveListener(FollowTargetCharacter);
        GameObject.Destroy(healthBar);
    }
}
