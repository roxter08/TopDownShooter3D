using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCallback
{
    public Action<Vector2> OnMoveButtonActivated = delegate { };
    public Action<bool> OnFireButtonPressed = delegate { };
    public Action<bool> OnSwapButtonPressed = delegate { };
}
