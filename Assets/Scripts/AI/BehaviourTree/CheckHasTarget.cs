using AI.BehaviourTree.Base;
using UnityEngine;

namespace AI.BehaviourTree
{
    public class CheckHasTarget : Node
    {
        public override NodeState Evaluate()
        {
            bool hasTarget = Root.GetData("Target") != null;
            _state = hasTarget ? NodeState.Success : NodeState.Failure;
            // Debug.Log("Target " + hasTarget);
            return _state;
        }
    }
}