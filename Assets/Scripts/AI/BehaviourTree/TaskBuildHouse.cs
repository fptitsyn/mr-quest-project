using AI.BehaviourTree.Base;
using UnityEngine;

namespace AI.BehaviourTree
{
    public class TaskBuildHouse : Node
    {
        private const float HouseWoodCost = 150f;
        private Transform _transform;

        public TaskBuildHouse(Transform transform)
        {
            _transform = transform;
        }
        
        public override NodeState Evaluate()
        {
            int r = Random.Range(1, 4);
            GameObject housePrefab = Resources.Load($"Prefabs/Buildings/House{r}") as GameObject;
            if (CharacterAI.CurrentWoodInStock >= HouseWoodCost)
            {
                CharacterAI.CurrentWoodInStock -= HouseWoodCost;
                GameObject house = Object.Instantiate(housePrefab, _transform.position, Quaternion.identity);
                Root.SetData("House", house);
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