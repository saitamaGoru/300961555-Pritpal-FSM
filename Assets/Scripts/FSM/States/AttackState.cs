using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : MonoBehaviour , IState
{

    public GameObject bulletPrefab; 
    public Transform bulletSpawnPoint; 
    public Transform player; 
    public float attackRange = 5f; 
    public float fireRate = 2f; 
    private float nextFireTime = 0f;

    private NavMeshAgent _agent;

    public bool attack;
    public AttackState(GameObject bpf, Transform bsPoint, Transform pl, NavMeshAgent ag)
    {
        bulletPrefab = bpf;
        bulletSpawnPoint = bsPoint;
        player = pl;
        _agent = ag;
    }

     public void OnEnter()
     {
        Debug.Log("Entering Attack state");
        attack = true;
        _agent.speed = -5f;
     }
   public void OnExit()
   {
      Debug.Log("Exiting Attack state");
      attack = false;
     //_agent.speed = 8f;
   }
   public void OnUpdate()
   {
       
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate; // Reset fire cooldown
        }
    }
   
   void Shoot()
    {
        // Instantiate the bullet prefab at the spawn point
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Optionally, add a force to the bullet to make it move towards the player
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce((player.position - bulletSpawnPoint.position).normalized * 10f, ForceMode.Impulse);
        }
    }
   public void OnFixedUpdate(){}
}
