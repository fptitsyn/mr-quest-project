using AI.BehaviourTree.Base;

namespace AI.BehaviourTree
{
    public class CheckStock : Node
    {
        private readonly float _houseCost = 120f;
        
        public override NodeState Evaluate()
        {
            float wood = CharacterAI.CurrentWoodInStock;
            if (wood >= _houseCost)
            {
                return NodeState.Success;
            }
            
            return NodeState.Failure;
        }
    }
}