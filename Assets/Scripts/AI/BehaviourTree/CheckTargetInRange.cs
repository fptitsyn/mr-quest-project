using AI.BehaviourTree.Base;
using UnityEngine;

namespace AI.BehaviourTree
{
    public class CheckTargetInRange : Node
    {
        private Transform _transform;
        
        private float _threshold;
        
        public CheckTargetInRange(Transform transform, float threshold)
        {
            _transform = transform;
            _threshold = threshold;
        }

        public override NodeState Evaluate()
        {
            GameObject target = (GameObject)Root.GetData("Target");
            if ((_transform.position - target.transform.position).sqrMagnitude < _threshold)
            {
                _state = NodeState.Success;
            }
            else
            {
                _state = NodeState.Failure;
            }

            return _state;
        }
    }
}