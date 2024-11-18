using AI.BehaviourTree.Base;
using UnityEngine;

namespace AI.BehaviourTree
{
    public class CheckHasTarget : Node
    {
        public override NodeState Evaluate()
        {
            Debug.Log("CheckHasTarget");
            bool hasTarget = Root.GetData("Target") != null;
            _state = hasTarget ? NodeState.Success : NodeState.Failure;
            return _state;
        }
    }
}