using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    [Serializable]
    public class WorldState
    {
        public string key;
        public int val;
    }

    public class WorldStates
    {
        private Dictionary<string, int> _states;

        public WorldStates()
        {
            _states = new Dictionary<string, int>();
        }

        public bool HasState(string key)
        {
            return _states.ContainsKey(key);
        }

        public void AddState(string key, int val)
        {
            _states.Add(key, val);
        }

        public void ModifyState(string key, int val)
        {
            if (_states.ContainsKey(key))
            {
                _states[key] += val;
                if (_states[key] <= 0)
                {
                    _states.Remove(key);
                }
            }
            else
            {
                _states.Add(key, val);
            }
        }

        public void RemoveState(string key)
        {
            if (_states.ContainsKey(key))
            {
                _states.Remove(key);
            }
        }

        public void SetState(string key, int val)
        {
            if (_states.ContainsKey(key))
            {
                _states[key] = val;
            }
            else
            {
                _states.Add(key, val);
            }
        }

        public Dictionary<string, int> GetStates()
        {
            return _states;
        }
    }
}