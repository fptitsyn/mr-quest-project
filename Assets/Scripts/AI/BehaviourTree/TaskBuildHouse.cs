using AI.BehaviourTree.Base;
using UnityEngine;

namespace AI.BehaviourTree
{
    public class TaskBuildHouse : Node
    {
        private const float houseWoodCost = 150f;
        private Transform _transform;

        public TaskBuildHouse(Transform transform)
        {
            _transform = transform;
        }
        
        public override NodeState Evaluate()
        {
            int r = Random.Range(1, 4);
            GameObject housePrefab = Resources.Load($"Prefabs/Buildings/House{r}") as GameObject;
            if (CharacterAI.CurrentWoodInStock >= houseWoodCost)
            {
                CharacterAI.CurrentWoodInStock -= houseWoodCost;
                Object.Instantiate(housePrefab, _transform.position, Quaternion.identity);
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