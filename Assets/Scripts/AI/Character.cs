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
                // Debug.Log("OnNavMesh");
                _agent.SetDestination(_playerTransform.position);
            }
        }
    }
}