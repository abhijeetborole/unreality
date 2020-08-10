using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.AI;
    
public class EnemyAI : MonoBehaviour
{
    public float EnemyHealth = 100f;
    public float DiscDamage = 55f;
    public float SaberDamage = 35f;
    private Transform target;
    private NavMeshAgent agent;
    private static Animator anim;
    private float damageTime;//Allows damage once every certain amount of seconds
    public float EnemyDamage = 75f;
    private bool flag;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("IsIdle",false);
        anim.SetBool("IsDying",false);
        float distance = Vector3.Distance(target.position, transform.position);    
        if (distance > agent.stoppingDistance)
        {
            agent.SetDestination(target.position);
            anim.SetBool("IsRunning", true);
            anim.SetBool("IsAttacking",false);
            anim.SetBool("IsPunching",false);
            anim.SetBool("IsDying",false);
            damageTime = 2.3f;
        }

        if (distance <= agent.stoppingDistance)
        {
            damageTime -= Time.deltaTime;
            FaceTarget();
            if (flag)
            {
                anim.SetBool("IsAttacking", true);
                anim.SetBool("IsPunching",false);
                anim.SetBool("IsRunning", false);
                anim.SetBool("IsDying",false);
                flag = false;
                if (damageTime <= 0f)
                {
                    SystemPowerUp.health -= EnemyDamage;
                    damageTime = 2.3f;
                }  
            }
            else
            {
                anim.SetBool("IsAttacking", false);
                anim.SetBool("IsPunching",true);
                anim.SetBool("IsRunning", false);
                anim.SetBool("IsDying",false);
                flag = true;
                if (damageTime < 0f)
                {
                    SystemPowerUp.health -= EnemyDamage;
                    damageTime = 2.3f;
                }
            }
        }
        if (EnemyHealth <= 0f)
        {
            anim.SetBool("IsAttacking",false);
            anim.SetBool("IsRunning",false);
            anim.SetBool("IsPunching",false);
            anim.SetBool("IsDying",true);
            Destroy(this);
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime*5f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Saber"))
        {
            EnemyHealth -= SaberDamage;
        }

        if (collision.gameObject.CompareTag("Disc"))
        {
            EnemyHealth -= DiscDamage;
        }
    }
    
}
