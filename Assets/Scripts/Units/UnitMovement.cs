﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class UnitMovement : NetworkBehaviour 
{
    
    [SerializeField] private NavMeshAgent agent = null;


    #region Server

    [ServerCallback]
    private void Update()
    {
        if (!agent.hasPath) { return; }
        if(agent.remainingDistance > agent.stoppingDistance) { return; }

        agent.ResetPath();
    }

    [Command]
    public void CmdMove(Vector3 position)
    {
        if(!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas)) { return; }

        agent.SetDestination(hit.position);
    }    

    #endregion

   




}


//#region Client

//public override void OnStartAuthority()
//{
//    mainCamera = Camera.main;
//}

//[ClientCallback]
//private void Update()
//{
//    {
//        if (!hasAuthority) { return; }
//        if (!Mouse.current.leftButton.wasPressedThisFrame) { return; }

//        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

//        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) { return; }

//        CmdMove(hit.point);
//    }
//}

//#endregion