using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    public sealed class GWorld
    {
        private static WorldStates _world;
        private static Queue<GameObject> _patients;
        private static Queue<GameObject> _cubicles;

        static GWorld()
        {
            _world = new WorldStates();
            _patients = new Queue<GameObject>();
            _cubicles = new Queue<GameObject>();

            GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cubicle");
            foreach (GameObject cube in cubes)
            {
                _cubicles.Enqueue(cube);
            }

            if (cubes.Length > 0)
            {
                _world.ModifyState("FreeCubicle", cubes.Length);
            }

            Time.timeScale = 5;
        }

        private GWorld()
        {
        
        }

        public void AddPatient(GameObject patient)
        {
            _patients.Enqueue(patient);
        }

        public GameObject RemovePatient()
        {
            if (_patients.Count == 0)
            {
                return null;
            }

            return _patients.Dequeue();
        }

        public void AddCubicle(GameObject cubicle)
        {
            _cubicles.Enqueue(cubicle);
        }

        public GameObject RemoveCubicle()
        {
            if (_cubicles.Count == 0)
            {
                return null;
            }

            return _cubicles.Dequeue();
        }
    
        public static GWorld Instance { get; } = new GWorld();

        public WorldStates GetWorld()
        {
            return _world;
        }
    }
}
