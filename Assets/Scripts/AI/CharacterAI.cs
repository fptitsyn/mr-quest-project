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
using Tree = AI.BehaviourTree.Base.Tree;

namespace AI
{
    using Tree = BehaviourTree.Base.Tree;

    public class CharacterAI : Tree
    {
        private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
        private NavMeshAgent _agent;
        private Transform _playerTransform;
        private Animator _animator;
        public static int CurrentWoodInStock = 0;

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
                    new TaskFindTarget(transform.position),
                    // has target tree
                    new CheckHasTarget(),
                    // new TaskFindTarget(transform.position),
                    new TaskWalk(_agent, transform.position),
                    new Timer(2.0f, new List<Node>
                    {
                        new TaskCollect(_animator)
                    })
                    // go collect this tree
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
            GameObject closestTree = Spawner.trees[0];
            float minSqrDistance = Vector3.SqrMagnitude(Spawner.trees[0].transform.position - position);
            foreach (var tree in Spawner.trees)
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