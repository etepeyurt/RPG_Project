using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using Unity.VisualScripting;
using NUnit.Framework;
using RPG.Combat;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour , IAction
    {
        [SerializeField] float maxSpeed = 6f;
        Health health;
        NavMeshAgent navMeshAgent;
        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }
        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();        
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 hit,float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            GetComponent<Fighter>().Cancel();
            MoveTo(hit , speedFraction);
        }
        public void MoveTo(Vector3 hit ,float speedFraction)
        {
            navMeshAgent.speed = maxSpeed * speedFraction;
            navMeshAgent.destination = hit;
            navMeshAgent.isStopped = false;
        }
        public void Cancel()
        {
        navMeshAgent.isStopped = true;
        }
        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }

}
