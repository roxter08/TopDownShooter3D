using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUIController
{
    private Dictionary<Character, HealthBarUI> healthBarUIDictionary;
    private Transform healthbarRootTransform;
    private GameObject healthBarPrefab;
    private CommonCallbacks commonCallbacks;
    public HealthBarUIController(Transform rootTransform, CommonCallbacks commonCallbacks)
    {
        this.commonCallbacks = commonCallbacks;

        healthBarUIDictionary = new Dictionary<Character, HealthBarUI>();
        healthbarRootTransform = rootTransform;
        healthBarPrefab = Resources.Load<GameObject>("HealthBar");

        AddListeners();
    }

    private void AddListeners()
    {
        commonCallbacks.OnCharacterAddedToGame += CreateNewHealthBar;
        commonCallbacks.OnCharacterRemovedFromGame += RemoveHealthBar;
    }

    private void RemoveHealthBar(Character obj)
    {
        healthBarUIDictionary[obj].Destroy();
        healthBarUIDictionary.Remove(obj);
    }

    private void RemoveAllHealthBars()
    {
        foreach (KeyValuePair<Character, HealthBarUI> kvp in healthBarUIDictionary)
        {
            kvp.Value.Destroy();
        }
        healthBarUIDictionary.Clear();
    }

    private void RemoveListeners()
    {
        commonCallbacks.OnCharacterAddedToGame -= CreateNewHealthBar;
        commonCallbacks.OnCharacterRemovedFromGame -= RemoveHealthBar;
    }

    private void CreateNewHealthBar(Character targetCharacter)
    {
        GameObject healthbar = GameObject.Instantiate(healthBarPrefab, healthbarRootTransform);
        HealthBarUI healthBarUI = new HealthBarUI(healthbar, targetCharacter, Camera.main);
        healthBarUIDictionary.Add(targetCharacter, healthBarUI);
    }

    public void OnDestroy()
    {
        RemoveAllHealthBars();
        RemoveListeners();
    }
}
