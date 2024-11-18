using AI.BehaviourTree.Base;

namespace AI.BehaviourTree
{
    public class CheckHasHouse : Node
    {
        public override NodeState Evaluate()
        {
            bool hasHouse = Root.GetData("House") != null;
            _state = hasHouse ? NodeState.Success : NodeState.Failure;
            return _state;
        }
    }
}