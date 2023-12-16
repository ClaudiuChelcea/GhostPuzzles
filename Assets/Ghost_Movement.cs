using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ghost_Movement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;

    float horizontalInput;
    float verticalInput;

    Rigidbody rb;
    Vector3 moveDirection;

    [Header("Ghosting")]
    private bool ghosting = false;
    public float ghostingTimer;
    private float starterGhostingTimer;
    public CapsuleCollider collider;
    public Material getPlayerMaterial;
    public Color[] Colors;
    private bool ghostingOnCooldown = false;
    public bool amInUnghostableZone = false;

    [Header("Dooring")]
    public bool canGoThroughGate1 = false;
    public bool canGoThroughGate2 = false;
    public bool canGoThroughGate3 = false;
    public bool canGoThroughGate4 = false;
    public bool canGoThroughGate5 = false;
    public bool canGoThroughGate6 = false;

    [Header("hinting")]
    public bool amGettingHinted;
    public GameObject hintComponent;
    public bool mustRefresh = false;

    [Header("Camera")]
    public bool isMovingForward;

    [Header("Character rotation")]
    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;

    [Header("Pickup")]
    public bool amLooting;
    public GameObject key;
    public bool mustLootRefresh;
    public GameObject lootComponent;

    [Header("Game Over")]
    public bool GameOver = false;
    public GameObject gameOverScreen;
    public TextMeshProUGUI timeCompletion;
    public TextMeshProUGUI coins;
    public Ghost_ManageUI manager;
    public GameObject portal1;
    public GameObject portal2;

    // check if in unghostable zone enter
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 23)
        {
            amInUnghostableZone = true;
            ghostingOnCooldown = true;
            ghosting = false;
        } else if(other.gameObject.layer == 24)
        {
            // key
            // activate pick-up button
            amLooting = true;
        }
        else if (other.gameObject.layer == 13)
        {
            // key
            // activate pick-up button
            amGettingHinted = true;
        }
        else if (other.gameObject.layer == 18)
        {
            // finish
            GameOver = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 23)
        {
            amInUnghostableZone = true;
            ghostingOnCooldown = true;
            ghosting = false;
        }
        else if (other.gameObject.layer == 24)
        {
            // key
            // activate pick-up button
            amLooting = true;
        }
        else if (other.gameObject.layer == 13)
        {
            // key
            // activate pick-up button
            amGettingHinted = true;
        }
        else if (other.gameObject.layer == 18)
        {
            // finish
            GameOver = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 23)
        {
            amInUnghostableZone = false;
        }
        else if (other.gameObject.layer == 24)
        {
            // key
            // disable pick-up button
            amLooting = false;
            mustLootRefresh = true;
        }
        else if (other.gameObject.layer == 13)
        {
            // key
            // activate pick-up button
            amGettingHinted = false;
            mustRefresh = true;
        }
    }

    // Start is called before the first frame update
    void Start()
        {
        // Movement
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.drag = groundDrag;

        // Ghosting
        collider = GetComponent<CapsuleCollider>();
        starterGhostingTimer = ghostingTimer;
        getPlayerMaterial.color = new Color(255, 255, 255);

        // Pick up
        key.gameObject.SetActive(true);

        // Game over
        gameOverScreen.gameObject.SetActive(false);
        GameOver = false;
        portal1.gameObject.SetActive(false);
        portal2.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameOver == true)
        {
            // show game over screen
            gameOverScreen.gameObject.SetActive(true);

            Time.timeScale = 0;

            timeCompletion.text = manager.gameCompletion.text;
            coins.text = manager.coinsAmount.ToString();
            return;
        }

        MyInput();
        MovePlayer();
        Ghosting();
        Hinting();
        Looting();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void Hinting()
    {
        if (amGettingHinted == true)
        {
            hintComponent.SetActive(true);
        } else
        {
            hintComponent.SetActive(false);
        }
    }

    private void Looting()
    {
        if (amLooting == true && canGoThroughGate1 == false)
        {
            lootComponent.SetActive(true);
        }
        else
        {
            lootComponent.SetActive(false);
        }
    }

    private void Ghosting()
    {
        // check if we want to ghost ourselves
        if(Input.GetKey(KeyCode.Space) && ghostingOnCooldown == false && amInUnghostableZone == false)
        {
            // start ghosting time
            float timer = ghostingTimer - Time.deltaTime;
            ghostingTimer = Mathf.Max(timer, 0f);

            float neededMultiplier = 255 / starterGhostingTimer; // the color now can be just the timer * this multiplier
            float trueColor = neededMultiplier * ghostingTimer;
            int colorIndex = selectColorIndex(trueColor);
            getPlayerMaterial.color = Colors[colorIndex];
            ghosting = true;

            if (ghostingTimer == 0f)
            {
                ghostingOnCooldown = true;
                ghosting = false;
            }
        } 
        
        if(ghosting == false && ((ghostingTimer < starterGhostingTimer) == true))
        {
            float timer = ghostingTimer + Time.deltaTime;
            ghostingTimer = Mathf.Min(timer, starterGhostingTimer);

            float neededMultiplier = 255 / starterGhostingTimer; // the color now can be just the timer * this multiplier
            float trueColor = neededMultiplier * ghostingTimer;
            int colorIndex = selectColorIndex(trueColor);
            getPlayerMaterial.color = Colors[colorIndex];
            ghosting = false;
        }

        if (ghostingTimer == starterGhostingTimer)
        {
            ghostingOnCooldown = false;
        }

        else if (Input.GetKeyUp(KeyCode.Space))
        {
            // stop ghosting timer
            ghosting = false;
        }

        // What actually happens when you are ghosting
        if(ghosting == true)
        {
            collider.enabled = false;
        } else
        {
            collider.enabled = true;
        }

        // Rotation - used by camera
        if(Input.GetKey(KeyCode.W))
        {
            isMovingForward = true;
        } else if(Input.GetKey(KeyCode.S))
        {
            isMovingForward = false;
        }

        // Rotation character
        // If input.sqrtMagnitude != 0 to prevent it going to forward automatically, but we like that
        var targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = Vector3.forward * verticalInput + Vector3.right * horizontalInput;

        rb.velocity = moveDirection * moveSpeed;
    }

    // Getters
    public float getGhostingStarterTime()
    {
        return starterGhostingTimer;
    }

    // Select color
    private int selectColorIndex(float color)
    {
        if (color <= 59)
        {
            return 4;
        }
        else if (color >= 60 && color <= 119)
        {
            return 3;
        }
        else if (color >= 120 && color <= 179)
        {
            return 2;
        }
        else if (color >= 180 && color <= 240)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
