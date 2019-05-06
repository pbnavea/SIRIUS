﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : Interactable
{
    public GameObject ExitShipPanel;

    public override void Interact()
    {
        isInteracting = !isInteracting;
        panelOpen = !panelOpen;
        ExitShipPanel.GetComponent<ExitShipScript>().ToggleExitShipPanel(isInteracting);
        //Debug.Log("leaving da shep");
        base.Interact();
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
            Interact();
        else if (!canInteract && panelOpen)
        {
            panelOpen = false;
            ExitShipPanel.GetComponent<Animator>().SetBool("isOpen", false);
        }
    }
}
