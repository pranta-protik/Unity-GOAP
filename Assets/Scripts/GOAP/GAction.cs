using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GOAP
{
    public abstract class GAction : MonoBehaviour
    {
        public string actionName = "Action";

        public float cost = 1.0f;

        public GameObject target;

        public string targetTag;

        public float duration = 0f;

        public WorldState[] preConditions;

        public WorldState[] afterEffects;

        public NavMeshAgent agent;

        public Dictionary<string, int> preconditions;

        public Dictionary<string, int> effects;

        public WorldStates agentBeliefs;

        public GInventory inventory;

        public WorldStates beliefs;

        public bool running = false;


        public GAction()
        {
            preconditions = new Dictionary<string, int>();
            effects = new Dictionary<string, int>();
        }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        
            if (preConditions != null)
            {
                foreach (WorldState worldState in preConditions)
                {
                    preconditions.Add(worldState.key, worldState.val);
                }
            }

            if (afterEffects != null)
            {
                foreach (WorldState worldState in afterEffects)
                {
                    effects.Add(worldState.key, worldState.val);
                }
            }

            inventory = GetComponent<GAgent>().inventory;
            beliefs = GetComponent<GAgent>().beliefs;
        }

        public bool IsAchievable()
        {
            return true;
        }

        public bool IsAchievableGiven(Dictionary<string, int> conditions)
        {
            foreach (KeyValuePair<string, int> precondition in preconditions)
            {
                if (!conditions.ContainsKey(precondition.Key))
                {
                    return false;
                }
            }
            return true;
        }

        public abstract bool PrePerform();
        public abstract bool PostPerform();
    }
}
