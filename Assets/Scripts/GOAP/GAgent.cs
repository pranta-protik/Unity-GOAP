using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GOAP
{
    public class SubGoal
    {
        public Dictionary<string, int> sgoals;
        public bool remove;

        public SubGoal(string key, int val, bool rem)
        {
            sgoals = new Dictionary<string, int>();
            sgoals.Add(key, val);
            remove = rem;
        }
    }
    public class GAgent : MonoBehaviour
    {
        public List<GAction> actions = new List<GAction>();
        public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
        public GInventory inventory = new GInventory();
        public WorldStates beliefs = new WorldStates();

        private GPlanner _planner;

        private Queue<GAction> _actionQueue;

        public GAction currentAction;

        private SubGoal _currentGoal;
    
        private bool _invoked = false;
    
        public void Start()
        {
            GAction[] acts = GetComponents<GAction>();
            foreach (GAction action in acts)
            {
                actions.Add(action);
            }
        }

        void CompleteAction()
        {
            currentAction.running = false;
            currentAction.PostPerform();
            _invoked = false;
        }
    
        void LateUpdate()
        {
            if (currentAction != null && currentAction.running)
            {
                float distanceToTarget = Vector3.Distance(currentAction.target.transform.position, this.transform.position);
                if (currentAction.agent.hasPath && distanceToTarget < 2f) //currentAction.agent.remainingDistance < 1f
                {
                    if (!_invoked)
                    {
                        Invoke(nameof(CompleteAction), currentAction.duration);
                        _invoked = true;
                    }
                }
                return;
            }
        
            if (_planner == null || _actionQueue == null)
            {
                _planner = new GPlanner();
            
                var sortedGoals = from entry in goals orderby entry.Value descending select entry;
            
                foreach (KeyValuePair<SubGoal,int> sortedGoal in sortedGoals)
                {
                    _actionQueue = _planner.Plan(actions, sortedGoal.Key.sgoals, beliefs);
                    if (_actionQueue != null)
                    {
                        _currentGoal = sortedGoal.Key;
                        break;
                    }
                }
            }

            if (_actionQueue != null && _actionQueue.Count == 0)
            {
                if (_currentGoal.remove)
                {
                    goals.Remove(_currentGoal);
                }

                _planner = null;
            }

            if (_actionQueue != null && _actionQueue.Count > 0)
            {
                currentAction = _actionQueue.Dequeue();
                if (currentAction.PrePerform())
                {
                    if (currentAction.target == null && currentAction.targetTag != "")
                    {
                        currentAction.target = GameObject.FindWithTag(currentAction.targetTag);
                    }

                    if (currentAction.target != null)
                    {
                        currentAction.running = true;
                        currentAction.agent.SetDestination(currentAction.target.transform.position);
                    }
                }
                else
                {
                    _actionQueue = null;
                }
            }
        }
    }
}