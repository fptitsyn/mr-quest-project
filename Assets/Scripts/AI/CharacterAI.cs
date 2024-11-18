using System;
using System.Collections.Generic;
using AI.BehaviourTree;
using AI.BehaviourTree.Base;
using Spawn;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Simulation;
using Sequence = AI.BehaviourTree.Base.Sequence;
using Timer = AI.BehaviourTree.Base.Timer;

namespace AI
{
    public class CharacterAI : BehaviourTree.Base.BehaviourTree
    {
        [SerializeField] private GameObject axeObject;
        
        private NavMeshAgent _agent;
        private Transform _playerTransform;
        private Animator _animator;
        public static float CurrentWoodInStock = 0f;

        private readonly float _searchThreshold = 0.3f;
        private Node _rootNode;
        
        private void Start()
        {
            _rootNode = SetupTree();
            // _agent = GetComponent<NavMeshAgent>();
            // _animator = GetComponent<Animator>();
            // _playerTransform = GameObject.Find("SimulationCamera").GetComponent<Transform>();
        }

        protected override Node SetupTree()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            
            Node root = new Selector();
            // root.SetData("House", new object());
            // CurrentWoodInStock = 10;
            root.SetChildren(new List<Node>
            {
                new CheckHasHouse(),
                new Sequence(new List<Node>
                {
                    new CheckStock(),
                    // build house
                }),
                
                // collect wood
                new Sequence(new List<Node> {
                    new TaskFindTarget(transform),
                    // has target tree
                    new CheckHasTarget(),
                    // go collect this tree
                    new TaskWalk(_agent, _animator, transform, _searchThreshold),
                    new CheckTargetInRange(transform, _searchThreshold),
                    new TaskCollect(_animator, axeObject)
                })
            }, forceRoot: true);
            
            return root;
        }

        private void Update()
        {
            _rootNode.Evaluate();
            // if (_agent.isOnNavMesh)
            // {
            //     _animator.SetFloat(MoveSpeed, _agent.velocity.magnitude);
            //     if (Spawner.trees.Count > 0)
            //     {
            //         GameObject closestTree = GetClosestTree(transform.position);
            //         _agent.SetDestination(closestTree.transform.position);
            //         // if (_agent.remainingDistance < _agent.stoppingDistance)
            //         // {
            //         //     Debug.Log("Tree destroyed");
            //         //     Spawner.trees.Remove(closestTree);
            //         //     Destroy(closestTree);
            //         // }
            //     }
            // }

            // else
            // {
            //     if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 0.2f, 1))
            //     {
            //         // "hit.position" now contains a valid position on the mesh
            //         if (_agent.Warp(hit.position))
            //         {
            //             // We're good, and the agent should work now.
            //             Debug.Log("Cool");
            //             _agent.SetDestination(_playerTransform.position);
            //         }
            //         else
            //         {
            //             Debug.Log("Help");
            //             // Something weird happened, probably the NavMesh or NavMeshAgent became invalid
            //             // somehow.   We have to handle it, of course, but it's going to
            //             // be a rare case, so maybe just destroying the object is good enough?
            //         }
            //     }
            //     else
            //     {
            //         Debug.Log("Even worse");
            //     }
            // }
        }

        // private void OnCollisionEnter(Collision other)
        // {
        //     if (other.gameObject.CompareTag("Tree"))
        //     {
        //         Spawner.trees.Remove(other.gameObject);
        //         Destroy(other.gameObject);
        //     }
        // }

        private GameObject GetClosestTree(Vector3 position)
        {
            GameObject closestTree = Spawner.Trees[0];
            float minSqrDistance = Vector3.SqrMagnitude(Spawner.Trees[0].transform.position - position);
            foreach (var tree in Spawner.Trees)
            {
                float sqrDistance = Vector3.SqrMagnitude(tree.transform.position - position);
                if (sqrDistance < minSqrDistance)
                {
                    minSqrDistance = sqrDistance;
                    closestTree = tree;
                }
            }

            return closestTree;
        }
    }
}