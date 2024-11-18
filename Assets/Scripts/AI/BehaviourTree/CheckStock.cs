using AI.BehaviourTree.Base;

namespace AI.BehaviourTree
{
    public class CheckStock : Node
    {
        public override NodeState Evaluate()
        {
            int wood = CharacterAI.CurrentWoodInStock;
            if (wood >= 10)
            {
                return NodeState.Success;
            }
            
            return NodeState.Failure;
        }
    }
}