using AI.BehaviourTree.Base;
using Spawn;
using UnityEngine;

namespace AI.BehaviourTree
{
    public class TaskFindTarget : Node
    {
        private Transform _transform;

        public TaskFindTarget(Transform transform)
        {
            _transform = transform;
        }
        
        public override NodeState Evaluate()
        {
            if (Spawner.Trees.Count == 0)
            {
                return NodeState.Failure;
            }

            GameObject closestTree = GetClosestTree(_transform.position);
            Root.SetData("Target", closestTree);
            return NodeState.Success;
        }
        
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