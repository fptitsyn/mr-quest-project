using AI.BehaviourTree.Base;

namespace AI.BehaviourTree
{
    public class CheckStock : Node
    {
        private readonly float _houseCost;

        public CheckStock(float houseWoodCost)
        {
            _houseCost = houseWoodCost;
        }
        
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