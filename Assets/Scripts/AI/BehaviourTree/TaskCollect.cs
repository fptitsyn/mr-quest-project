using System.Collections;
using AI.BehaviourTree.Base;
using UnityEngine;
using Tree = Spawn.Tree;

namespace AI.BehaviourTree
{
    public class TaskCollect : Node
    {
        private static readonly int IsChopping = Animator.StringToHash("IsChopping");
        private Animator _animator;
        private float _collectAmount;
        
        private bool _canChop;
        private float _timer;
        private float _choppingCooldown = 3.15f; // Approximate chopping animation length

        private GameObject _axe;
        private AudioSource _audioSource;
        
        public TaskCollect(Animator animator, GameObject axe, AudioSource audioSource)
        {
            _animator = animator;
            _axe = axe;
            _audioSource = audioSource;
            _audioSource.clip = Resources.Load<AudioClip>("Audio/SFX/chop");
            _audioSource.loop = false;
            _collectAmount = 10f;
        }
        
        public override NodeState Evaluate()
        {
            GameObject target = (GameObject)Root.GetData("Target");
            
            _animator.SetBool(IsChopping, true);
            _axe.SetActive(true);
            if (_canChop)
            {
                _audioSource.Play();
                _canChop = false;
                
                Debug.Log("Collecting");
                
                Tree tree = target.GetComponent<Tree>();
                bool treeDestroyed = tree.ChopTree(_collectAmount);
                CharacterAI.CurrentWoodInStock += _collectAmount;
                if (treeDestroyed)
                {
                    Debug.Log("Tree chopped");
                    // _canChop = true;
                    Root.ClearData("Target");
                    _animator.SetBool(IsChopping, false);
                    _axe.SetActive(false);
                }
            }
            else
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    _timer = _choppingCooldown;
                    _canChop = true;
                }
            }
            
            _state = NodeState.Running;
            return _state;
        }
    }
}