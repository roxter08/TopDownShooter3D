using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : Character , IInputProvider
{
    private Rigidbody thisRigidbody;
    private Vector3 headDirection;
    private WeaponController weaponController;
    private Animator animator;
    private InputController inputController;

    private int velocityX;
    private int velocityY;

    protected override void Start()
    {
        base.Start();
        thisRigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        weaponController = new WeaponController(weaponHolder, inputController, commonCallback);

        velocityX = Animator.StringToHash("VelocityX");
        velocityY = Animator.StringToHash("VelocityY");
    }

    private void SetHeadDirection(Vector2 value)
    {
        headDirection = new Vector3(value.x, 0, value.y); 
        headDirection.Normalize();
    }

    protected override void Move()
    {
        SetHeadDirection(inputController.GetMoveDirection());

        AnimatePlayer();

        headDirection =  transform.TransformDirection(headDirection);
        thisRigidbody.position += headDirection * moveSpeed * Time.deltaTime;
    }

    private void AnimatePlayer()
    {
        animator.SetFloat(velocityX, Mathf.Clamp(headDirection.x, -1, 1), 0.1f, Time.deltaTime);
        animator.SetFloat(velocityY, Mathf.Clamp(headDirection.z, -1, 1), 0.1f, Time.deltaTime);
    }

    protected override void Turn()
    {


        Vector3 turnDir = new Vector3(inputController.GetTurnDirection().x, 0, inputController.GetTurnDirection().y);
        if (turnDir.magnitude > 0)
        {
            Quaternion rotation = Quaternion.LookRotation(turnDir.normalized, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed * Time.deltaTime);
        }
    }

    private void Update()
    {
        weaponController.Update();
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void OnDestroy()
    {
        weaponController.OnDestroy();
    }

    public void SetInputController(InputController inputController)
    {
        this.inputController = inputController;
    }
}
