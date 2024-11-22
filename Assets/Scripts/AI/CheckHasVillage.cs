using AI.BehaviourTree.Base;

namespace AI
{
    public class CheckHasVillage : Node
    {
        public override NodeState Evaluate()
        {
            _state = Root.GetData("Village") != null ? NodeState.Success : NodeState.Failure;
            return _state;
        }
    }
}