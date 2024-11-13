using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Simulation;

namespace AI
{
    public class Character : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Transform _playerTransform;
        
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _playerTransform = GameObject.Find("SimulationCamera").GetComponent<Transform>();
        }

        private void Update()
        {
            if (_agent.isOnNavMesh)
            {
                _agent.SetDestination(_playerTransform.position);
            }
            else
            {
                if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 2f, 1))
                {
                    // "hit.position" now contains a valid position on the mesh
                    if (_agent.Warp(hit.position))
                    {
                        // We're good, and the agent should work now.
                        Debug.Log("Cool");
                        _agent.SetDestination(_playerTransform.position);
                    }
                    else
                    {
                        Debug.Log("Help");
                        // Something weird happened, probably the NavMesh or NavMeshAgent became invalid
                        // somehow.   We have to handle it, of course, but it's going to
                        // be a rare case, so maybe just destroying the object is good enough?
                    }
                }
                else
                {
                    Debug.Log("Even worse");
                }
            }
        }
    }
}