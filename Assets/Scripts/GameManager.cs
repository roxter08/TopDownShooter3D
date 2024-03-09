using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Transform uiRootTransform;
    [SerializeField] Transform gameplayElements;
    private UIController uiController;
    private CommonCallbacks commonCallbacks;
    private InputController inputController;
    private void Awake()
    {
        commonCallbacks = new CommonCallbacks();
        inputController = new InputController(playerInput);
        uiController = new UIController(commonCallbacks, uiRootTransform);
        new EndGame(commonCallbacks);

        Setup();
    }

    private void Setup()
    {
        ICommonCallbackProvider[] commonCallbackProviders = gameplayElements.GetComponentsInChildren<ICommonCallbackProvider>(true);
        IInputProvider[] inputProviders = gameplayElements.GetComponentsInChildren<IInputProvider>(true);

        foreach (ICommonCallbackProvider callbackProvider in commonCallbackProviders)
        {
            callbackProvider.SetCallback(commonCallbacks);
        }
        foreach (IInputProvider inputProvider in inputProviders)
        {
            inputProvider.SetInputController(inputController);
        }
    }

    private void Update()
    {
        inputController.Update();
    }

    private void OnDestroy()
    {
        inputController.OnDestroy();
        uiController.OnDestroy();
    }
}
