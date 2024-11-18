using System.Collections.Generic;
using AI.BehaviourTree;
using AI.BehaviourTree.Base;
using UnityEngine;
using UnityEngine.AI;
using Sequence = AI.BehaviourTree.Base.Sequence;

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
                    new Timer(5.0f, new List<Node>
                    {
                        new TaskBuildHouse(transform)
                    })
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
        }
    }
}