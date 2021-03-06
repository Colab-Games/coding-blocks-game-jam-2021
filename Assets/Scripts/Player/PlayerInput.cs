﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We first ensure this script runs before all other player scripts to prevent laggy
// inputs
[DefaultExecutionOrder(-100)]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    PlayerMovement playerMovement;  // The player movement controller 
    bool readyToClear;              // Bool used to keep input in sync

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        ClearInputs();
        ProcessInputs();
    }

    void FixedUpdate()
    {
        // Set a flag that lets inputs to be cleared out during the next Update().
        // This ensures that all code gets to use the current inputs
        readyToClear = true;
    }

    void ClearInputs()
    {
        if (!readyToClear)
            return;

        playerMovement.jump = false;
        playerMovement.boostJump = false;
        playerMovement.interact = false;

        readyToClear = false;
    }

    void ProcessInputs()
    {
        float horizontal = Input.GetAxis("Horizontal");
        playerMovement.horizontalMovement = Mathf.Clamp(horizontal, -1f, 1f);

        float vertical = Input.GetAxis("Vertical");
        playerMovement.verticalMovement = Mathf.Clamp(vertical, -1f, 1f);

        playerMovement.jump = playerMovement.jump || Input.GetButtonDown("Jump");
        playerMovement.boostJump = playerMovement.boostJump || Input.GetButton("Jump");
        playerMovement.interact = playerMovement.interact || Input.GetButtonDown("Interact");
    }
}
