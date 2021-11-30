using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectribBall : MonoBehaviour
{
    [SerializeField] private float proyectileSpeed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddRelativeForce(0, 0, proyectileSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
