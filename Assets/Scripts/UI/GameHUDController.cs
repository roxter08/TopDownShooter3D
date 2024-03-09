using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHUDController
{
    private CommonCallbacks commonCallbacks;
    private TextMeshProUGUI currentWeaponName;

    public GameHUDController(Transform rootTransform, CommonCallbacks commonCallbacks)
    {
        this.commonCallbacks = commonCallbacks;
        currentWeaponName = rootTransform.Find("WeaponName").GetComponent<TextMeshProUGUI>();
        AddListeners();
    }

    private void AddListeners()
    {
        commonCallbacks.OnPlayerWeaponSwapped += ShowCurrentWeapon;
    }

    private void RemoveListeners()
    {
        commonCallbacks.OnPlayerWeaponSwapped -= ShowCurrentWeapon;
    }

    private void ShowCurrentWeapon(WeaponData data)
    {
        currentWeaponName.text = data.Name;
    }

    public void OnDestroy()
    {
        RemoveListeners();
    }
}
