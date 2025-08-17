using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 10f;       // forward speed
    public float laneDistance = 3f;       // distance between lanes
    public int currentLane = 1;           // 0 = left, 1 = middle, 2 = right

    [Header("Milestone Icon Settings")]
    public GameObject milestoneIcon;      // assign in Inspector

    private Vector2 startTouchPos;
    private Vector2 endTouchPos;
    private bool isSwiping = false;

    void Start()
    {
        if (milestoneIcon != null)
            milestoneIcon.SetActive(false);

        // Show the icon after 15 seconds
        Invoke(nameof(ShowMilestoneIcon), 15f);
    }

    void Update()
    {
        // Forward movement
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.World);

        // Keyboard input (PC test)
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeLane(-1);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeLane(1);
        }

        // Touch swipe input (Mobile)
        HandleTouch();

        // Smoothly move player into lane position
        Vector3 targetPosition = new Vector3((currentLane - 1) * laneDistance, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);
    }

    void HandleTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPos = touch.position;
                isSwiping = true;
            }
            else if (touch.phase == TouchPhase.Ended && isSwiping)
            {
                endTouchPos = touch.position;
                Vector2 swipeDelta = endTouchPos - startTouchPos;

                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                {
                    if (swipeDelta.x > 50f) // Swipe Right
                        ChangeLane(1);
                    else if (swipeDelta.x < -50f) // Swipe Left
                        ChangeLane(-1);
                }

                isSwiping = false;
            }
        }
    }

    void ChangeLane(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, 0, 2); // only 3 lanes
    }

    void ShowMilestoneIcon()
    {
        if (milestoneIcon != null)
        {
            milestoneIcon.SetActive(true);
            Invoke(nameof(HideMilestoneIcon), 2f);
        }
    }

    void HideMilestoneIcon()
    {
        if (milestoneIcon != null)
            milestoneIcon.SetActive(false);
    }
}
