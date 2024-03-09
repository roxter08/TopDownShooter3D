using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController    
{
    private Weapon currentActiveWeapon;
    private List<Weapon> weaponsList;

    private int currentWeaponIndex = 0;
    private bool isFireButtonDown;

    private InputCallback inputCallback;
    private CommonCallbacks commonCallbacks;

    public WeaponController(Transform weaponHolder, InputController inputController, CommonCallbacks commonCallbacks)
    {
        this.inputCallback = inputController.InputCallback;
        this.commonCallbacks = commonCallbacks;

        AddListeners();

        weaponsList = new List<Weapon>();
        weaponHolder.GetComponentsInChildren<Weapon>(weaponsList);
        weaponsList.ForEach(x => x.ActivateWeapon(false));
        currentActiveWeapon = weaponsList[currentWeaponIndex];

        currentActiveWeapon.ActivateWeapon(true);
        commonCallbacks.OnPlayerWeaponSwapped(currentActiveWeapon.GetWeaponData());
    }

    private void FireWeapon(bool buttonState)
    {
        isFireButtonDown = buttonState;
    }

    public void Update()
    {
        if(isFireButtonDown)
        {
            if(currentActiveWeapon is FireArm fireArm && !fireArm.HasRapidFire())
            {
                isFireButtonDown=false;
            }
            currentActiveWeapon.Use();
        }
    }

    private void SwapWeapon(bool buttonState)
    {
        currentActiveWeapon.ActivateWeapon(false);
        currentWeaponIndex = (currentWeaponIndex + 1) % weaponsList.Count;
        currentActiveWeapon = weaponsList[currentWeaponIndex];
        currentActiveWeapon.ActivateWeapon(true);

        commonCallbacks.OnPlayerWeaponSwapped(currentActiveWeapon.GetWeaponData());
    }

    public void OnDestroy()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        inputCallback.OnFireButtonPressed += FireWeapon;
        inputCallback.OnSwapButtonPressed += SwapWeapon;
    }

    private void RemoveListeners()
    {
        inputCallback.OnFireButtonPressed -= FireWeapon;
        inputCallback.OnSwapButtonPressed -= SwapWeapon;
    }
}
