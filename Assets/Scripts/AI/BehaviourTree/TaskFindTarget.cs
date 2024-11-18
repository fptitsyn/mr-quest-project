using AI.BehaviourTree.Base;
using Spawn;
using UnityEngine;

namespace AI.BehaviourTree
{
    public class TaskFindTarget : Node
    {
        private Vector3 _position;

        public TaskFindTarget(Vector3 position)
        {
            _position = position;
        }
        
        public override NodeState Evaluate()
        {
            if (Spawner.trees.Count == 0)
            {
                Debug.Log("no trees found");
                return NodeState.Failure;
            }

            GameObject closestTree = GetClosestTree(_position);
            Root.SetData("Target", closestTree);
            Debug.Log("tree found and set");
            return NodeState.Success;
        }
        
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