using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ICommonCallbackProvider
{
    public void SetCallback(CommonCallbacks callback);
}