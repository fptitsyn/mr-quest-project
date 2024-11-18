using UnityEngine;

namespace AI.BehaviourTree.Base
{
    public abstract class Tree : MonoBehaviour
    {
        protected Node _root = null;

        protected virtual void Awake()
        {
            Debug.Log("Awake");
            Node.LastId = 0;
            _root = SetupTree();
        }

        private void Update()
        {
            Debug.Log("1");
            if (_root != null)
            {
                _root.Evaluate();
            }
        }

        public Node Root => _root;
        protected abstract Node SetupTree();
    }
}