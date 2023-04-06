using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] NavMeshAgent agent = null;

    Camera mainCamera;

    #region SERVER ONLY FUNCS

    [Command]
    void CmdMove(Vector3 position)
    {
        if(!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas))
            return;

        agent.SetDestination(hit.position);

    }

    #endregion


    #region CLIENT FUNCS

    // Start method for client who owns the object

    public override void OnStartAuthority()
    {
        mainCamera = Camera.main;
    }

    [ClientCallback] // Created as a client only update function (meaning only to ALL clients, exclude the server)
    private void Update()
    {
        // Each client owns each playabale game object
        if(!isOwned) // if(!hasAuthority) is the old function
          return;

        // Check for the right mouse button input
        if(!Input.GetMouseButtonDown(0))
            return;
        
        // Grabbing the mouse cursor information
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Checking where the ray hits in scene
        if(!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            return;

        CmdMove(hit.point);
    }

    #endregion
}
