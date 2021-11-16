using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicController : MonoBehaviour
{
    [SerializeField] private float speedEnemy = 50f;
    [SerializeField] private float timer = 5f;
    [SerializeField] private float deathTimer = 3f;
    [SerializeField] private float attackRate = 2f;
    private float attackCD = 0;
    [SerializeField] private GameObject player;
    [SerializeField] private int maxHealth = 5;
    [SerializeField]private int currentHealth;
    [SerializeField] int attackDmg = 1;
    private Animator animMimic;
    private Rigidbody rbEnemy;

    [SerializeField] Transform attackPoint;
    [SerializeField] private float attackRange = .5f;
    [SerializeField] private float dmgRange = .5f;
    private bool playerInAttackRange;
    [SerializeField] private LayerMask playerLayers;
    [SerializeField] Difficulty difficulty;

    private enum Difficulty { Easy, Medium, Hard };
    private bool Active = false;
    private bool isDead = false;
    private bool isAttack = false;


    // Start is called before the first frame update
    void Start()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                speedEnemy = 0.3f;
                attackRate = .5f;
                maxHealth = 1;
                break;
            case Difficulty.Medium:
                speedEnemy = 0.5f;
                attackRate = 1f;
                maxHealth = 3;
                break;
            case Difficulty.Hard:
                speedEnemy = 1f;
                attackRate = 2f;
                maxHealth = 5;
                break;

        }

        currentHealth = maxHealth;
        rbEnemy = GetComponent<Rigidbody>();
        animMimic = gameObject.transform.GetChild(0).GetComponent<Animator>();

        

    }

    // Update is called once per frame
    void Update()
    {
        animMimic.SetBool("Active", Active);
        
        
    }
    private void FixedUpdate()
    {
        
        
            
        Vector3 playerDirection = GetPlayerDirection();
        if (timer<0 && isDead == false)
        {
            Active = true;


            if (playerDirection.magnitude > attackRange)
            {
                rbEnemy.rotation = Quaternion.LookRotation(new Vector3(playerDirection.x, 0, playerDirection.z));
                rbEnemy.AddForce(playerDirection.normalized * speedEnemy, ForceMode.Impulse);
            }

            if (Time.time >= attackCD && playerDirection.magnitude < attackRange)
            {
                AnimAttack();
                Attack();
                attackCD = Time.time + 1f / attackRate;
            }
        }
        
        timer -= Time.deltaTime;

        if (isDead == true)
        {

            if (deathTimer < 0)
            {
                Destroy(gameObject);
            }
            deathTimer -= Time.deltaTime;
            
        }


    }
    private Vector3 GetPlayerDirection()
    {
        return player.transform.position - transform.position;
    }
    public void TakeDamage(int Dmg)
    {
        currentHealth -= Dmg;
        if (currentHealth >= 0)
        {


            animMimic.SetTrigger("Hurt");


        }
        if (currentHealth <= 0)
        {
            Die();


        }
        
    }

    private void Die()
    {

        animMimic.SetBool("isDead", true);

        isDead = true;

        GameManager.instance.AddExp();
        Debug.Log(GameManager.getLevel());

    }

    private void AnimAttack()
    {
        animMimic.SetTrigger("Attack");
    }

    private void Attack()
    {
        
        Collider[] hitPLayers = Physics.OverlapSphere(attackPoint.position, dmgRange, playerLayers);

        foreach (Collider player in hitPLayers)
        {
            player.GetComponent<MainCharController>().TakeDamage(attackDmg);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
