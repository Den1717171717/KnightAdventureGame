using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{   
    public static Player Instance { get; private set; }

    [SerializeField] private float movingSpeed = 3f;

    private Rigidbody2D rb;
    Vector2 inputVector;

    private float minMovingSpeed = 0.1f;
    private bool isRunning = false;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
    }

    private void GameInput_OnPlayerAttack(object sender, System.EventArgs e)
    {
        ActiveWeapons.Instance.GetActiveWeapon().Attack();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }
    private void Update()
    {
        inputVector = GameInput.Instance.GetMovementVector(); 
    }
    private void HandleMovement()
    {


        
        rb.MovePosition(rb.position + inputVector * movingSpeed * Time.fixedDeltaTime);

        if (Mathf.Abs(inputVector.x) > minMovingSpeed || Mathf.Abs(inputVector.y) > minMovingSpeed)
        {
           isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    public bool IsRunning()
    {
        return isRunning;
    }
    public Vector3 GetPlayerScreenPosition()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }

    }

