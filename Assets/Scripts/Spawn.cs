using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject patientPrefab;
    public int numPatients;
    
    void Start()
    {
        // for (int i = 0; i < numPatients; i++)
        // {
        //     Instantiate(patientPrefab, transform.position, Quaternion.identity);
        // }
        Invoke(nameof(SpawnPatient), 5f);
    }

    void SpawnPatient()
    {
        Instantiate(patientPrefab, transform.position, Quaternion.identity);
        Invoke(nameof(SpawnPatient), Random.Range(2f, 10f));
    }

    void Update()
    {
        
    }
}
