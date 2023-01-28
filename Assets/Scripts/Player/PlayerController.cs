using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System;

public class PlayerController : MonoBehaviour
{
    public bool moving;
    private bool mouseHeldDown = false;
    public bool isDead = false;

    private int health;
    public float horizontalMovement;
    public float verticalMovement;
    [Range(0, 1)] private float mouseHeldDownTime;
    [SerializeField] private float moveSpeed;

    private BoundsInt tileArea;
    public Vector3 facingDirection;
    [SerializeField] private LayerMask impassable;
    [SerializeField] private LayerMask darkness;

    [SerializeField] private GameObject deathScreen;

    [SerializeField] private Transform movePoint;
    private Slider mouseDownSlider;
    private Slider healthSlider;
    private Tilemap darkTilemap;
    private DetectionSystem detectionSystem;
    private BreathSystem breathSystem;
    private LevelManager levelManager;
    [SerializeField] private TutorialScript tutorial;
    private SFXPlayer sfx;

    void Start()
    {
        health = 5;
        facingDirection = new Vector3(0, 0, 0);
        horizontalMovement = 0;
        verticalMovement = 0;
        movePoint.SetParent(null);
        mouseHeldDownTime = 0;
        mouseDownSlider = GameObject.FindGameObjectWithTag("MouseDownSlider").GetComponent<Slider>();
        mouseDownSlider.gameObject.SetActive(false);
        darkTilemap = GameObject.FindGameObjectWithTag("Darkness").GetComponent<Tilemap>();
        detectionSystem = GameObject.FindGameObjectWithTag("Detection").GetComponent<DetectionSystem>();
        breathSystem = GameObject.FindGameObjectWithTag("Breath").GetComponent<BreathSystem>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        tutorial = GameObject.FindGameObjectWithTag("Tutorial").GetComponent<TutorialScript>();
        sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXPlayer>();
        healthSlider = GameObject.FindGameObjectWithTag("Health").GetComponent<Slider>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead && !tutorial.tutorialOpen)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                mouseHeldDown = true;
                sfx.PlaySmallInhale();
                mouseDownSlider.gameObject.SetActive(true);
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                detectionSystem.AddToDetection(mouseHeldDownTime * 5 * breathSystem.GetBreath());
                if (mouseHeldDownTime > 0.8f)
                {
                    mouseHeldDownTime = 1f;
                    sfx.PlayLongBlow();
                }
                else if (mouseHeldDownTime > 0.4f)
                {
                    mouseHeldDownTime = 0.6f;
                    sfx.PlayShortBlow();
                }
                else
                {
                    mouseHeldDownTime = 0.2f;
                    sfx.PlayShortBlow();
                }
                breathSystem.AddToBreath(0.4f * mouseHeldDownTime);
                UndoDarkness(mouseHeldDownTime);
                mouseHeldDownTime = 0;
                mouseHeldDown = false;
                mouseDownSlider.value = 0;
                mouseDownSlider.gameObject.SetActive(false);
            }

            if (mouseHeldDown)
            {
                mouseHeldDownTime += (Time.deltaTime / breathSystem.GetBreath());
                mouseDownSlider.value = mouseHeldDownTime;
                if (mouseHeldDownTime > 1)
                {
                    mouseHeldDownTime = 1;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isDead && !tutorial.tutorialOpen)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (!moving)
        {
            Vector2 newMovePoint = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (newMovePoint != new Vector2(0, 0) && !(newMovePoint.x != 0 && newMovePoint.y != 0))
            {
                facingDirection = new Vector3(newMovePoint.x, newMovePoint.y, 0);
                if (isMovementPossible(newMovePoint))
                {
                    moving = true;
                    horizontalMovement = newMovePoint.x;
                    verticalMovement = newMovePoint.y;
                    movePoint.position += new Vector3(newMovePoint.x, newMovePoint.y, 0);
                }
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
            {
                moving = false;
            }
        }

    }

    private void UndoDarkness(float mouseHeldDownTime)
    {
        Vector3Int boundsIntSize = new Vector3Int(Mathf.RoundToInt(5 * mouseHeldDownTime), Mathf.RoundToInt(5 * mouseHeldDownTime), 1);
        Vector3 shiftedPlayerArea = (new Vector3(transform.position.x - boundsIntSize.x/2, transform.position.y - boundsIntSize.y/2, transform.position.z)) + facingDirection;
        tileArea = new BoundsInt(darkTilemap.WorldToCell(shiftedPlayerArea), boundsIntSize);
        foreach (var pos in tileArea.allPositionsWithin)
        {
            Vector3Int tilePos = new Vector3Int(pos.x, pos.y, pos.z);
            if (darkTilemap.HasTile(tilePos))
            {
                //Color colourOfLiftedDarkness = new Color(1f, 1f, 1f, 0.3f);
                //darkTilemap.SetTileFlags(tilePos, TileFlags.None);
                darkTilemap.SetTile(tilePos, null);
            }  
        }
    }

    private bool isMovementPossible(Vector2 movementLocation)
    {
        if (Physics2D.OverlapCircle(movePoint.position + new Vector3(movementLocation.x, movementLocation.y, 0f), 0.2f, impassable))
        {
            return false;
        }
        if (Physics2D.OverlapCircle(movePoint.position + new Vector3(movementLocation.x, movementLocation.y, 0f), 0.2f, darkness))
        {
            breathSystem.AddToBreath(0.3f);
            TakeDamage();
        }

        return true;
    }

    private void TakeDamage()
    {
        health--;
        healthSlider.value = health;
        sfx.PlayPainedInhale();
        if (health < 1)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            sfx.PlayDoorOpen();
            levelManager.KeyGotten();
        }
    }

    public void Die()
    {
        tutorial.canOpenTutorial = false;
        isDead = true;
        deathScreen.SetActive(true);
    }
}
