using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatcherController : MonoBehaviour
{
    [SerializeField] private float distance = 10f;
    [SerializeField] private GameObject shootOrigin;
    [SerializeField] private int shotCD = 2;
    [SerializeField] private float shotTimer = 2f;

    [SerializeField] private GameObject proyectilePrefab;

    private bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canShoot)
        {
           RaycastWatcher();
        }
        else
        {
            shotTimer += Time.deltaTime;
        }
        if(shotTimer > shotCD)
        {
            canShoot = true;
        }
       
    }

    private void RaycastWatcher()
    {
        RaycastHit hit;

        if(Physics.Raycast(shootOrigin.transform.position, shootOrigin.transform.TransformDirection(Vector3.forward), out hit, distance))
        {
            Debug.Log("Hit");
            if(hit.transform.tag == "Player")
            {
                canShoot = false;
                shotTimer = 0;
                GameObject b = Instantiate(proyectilePrefab, shootOrigin.transform.position, proyectilePrefab.transform.rotation);
                b.GetComponent<Rigidbody>().AddForce(shootOrigin.transform.TransformDirection(Vector3.forward) * 10f, ForceMode.Impulse);
            }
            
        }
    }
    private void OnDrawGizmos()
    {
        if(canShoot)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(shootOrigin.transform.position, shootOrigin.transform.TransformDirection(Vector3.forward) * distance);
        }
        
    }
}
