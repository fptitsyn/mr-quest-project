using System;
using AI;
using UnityEngine;

namespace Spawn
{
    public class Tree : MonoBehaviour
    {
        [SerializeField] private float health;

        public float Health
        {
            get => health;
            set => health = Mathf.Clamp(value, 0f, 50f);
        }

        private void Start()
        {
            Spawner.Trees.Add(gameObject);
        }

        public bool ChopTree(float damage)
        {
            Health -= damage;
            if (Health <= 0f)
            {
                Spawner.Trees.Remove(gameObject);
                Destroy(gameObject);
                return true;
            }

            return false;
        }
    }
}