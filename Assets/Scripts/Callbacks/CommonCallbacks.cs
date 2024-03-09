using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonCallbacks
{
    public Action<Character> OnCharacterAddedToGame = delegate{ };
    public Action<Character> OnCharacterRemovedFromGame = delegate{ };
    public Action<Transform, int> OnHitOccured = delegate { };
    public Action<WeaponData> OnPlayerWeaponSwapped = delegate { };
}
