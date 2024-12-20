using AI.BehaviourTree.Base;
using UnityEngine;
using UnityEngine.AI;

namespace AI.BehaviourTree
{
    public class TaskWalk : Node
    {
        private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
        private NavMeshAgent _agent;
        private Animator _animator;
        private Transform _transform;
        private float _threshold;

        public TaskWalk(NavMeshAgent navMeshAgent, Animator animator, Transform transform, float threshold)
        {
            _agent = navMeshAgent;
            _animator = animator;
            _transform = transform;
            _threshold = threshold;
            _agent.speed = 0.5f;
        }
        
        public override NodeState Evaluate()
        {
            _state = NodeState.Running;

            // if (!_agent.isOnNavMesh)
            // {
            //     Debug.LogWarning("not on navmesh");
            //     _state = NodeState.Failure;
            // }
            // else
            {
                GameObject target = (GameObject)Root.GetData("Target");
                _agent.SetDestination(target.transform.position);
                _agent.isStopped = false;
                _animator.SetFloat(MoveSpeed, _agent.velocity.magnitude);
                _agent.updatePosition = true;
                
                if (Vector3.SqrMagnitude(_transform.position - target.transform.position) < _threshold)
                {
                    _agent.isStopped = true;
                    _animator.SetFloat(MoveSpeed, 0f);
                    _state = NodeState.Success;
                }
                // else
                // {
                //     Debug.Log("не");
                //     _state = NodeState.Running;
                // }
            }
            
            return _state;
        }
    }
}