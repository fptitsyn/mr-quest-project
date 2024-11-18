using AI.BehaviourTree.Base;
using Spawn;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

namespace AI.BehaviourTree
{
    public class TaskCollect : Node
    {
        private static readonly int IsChopping = Animator.StringToHash("IsChopping");
        private Animator _animator;
        private int _collectAmount;

        public TaskCollect(Animator animator)
        {
            _animator = animator;
        }
        
        public override NodeState Evaluate()
        {
            GameObject target = (GameObject)Root.GetData("Target");
            _animator.SetBool(IsChopping, true);
            
            CharacterAI.CurrentWoodInStock += _collectAmount;
            Root.ClearData("Target");
            Spawner.trees.Remove(target);
            Object.Destroy(target);
            
            _state = NodeState.Running;
            return _state;
        }
    }
}