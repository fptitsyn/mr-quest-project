using AI.BehaviourTree.Base;
using UnityEngine;
using UnityEngine.AI;

namespace AI.BehaviourTree
{
    public class TaskWalk : Node
    {
        private NavMeshAgent _agent;
        private Vector3 _position;

        public TaskWalk(NavMeshAgent navMeshAgent, Vector3 position)
        {
            _agent = navMeshAgent;
            _position = position;
        }
        
        public override NodeState Evaluate()
        {
            Debug.Log("walking");
            // _state = NodeState.Running;

            if (!_agent.isOnNavMesh)
            {
                _state = NodeState.Failure;
            }
            else
            {
                GameObject target = (GameObject)Root.GetData("Target");
                _agent.SetDestination(target.transform.position);
                
                if ((_position - target.transform.position).sqrMagnitude < 0.05f)
                {
                    Debug.Log("дошёл ура");
                    _state = NodeState.Success;
                }
                else
                {
                    Debug.Log("не");
                    _state = NodeState.Running;
                }
            }
            
            return _state;
        }
    }
}