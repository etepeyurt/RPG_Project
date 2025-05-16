using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Controller
{
public class AIController : MonoBehaviour
{
        [SerializeField] float chaseDistance;
        [SerializeField] float suspTime = 5f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointLifetime = 3f;
        [Range(0,1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;
        GameObject player;
        Fighter fighter;
        Health health;
        Mover mover;
        Vector3 enemylocation;
        float timeSinceLastSawPlayer;
        float timeSinceArriveWaypoint;
        int currentWaypointIndex = 0;
    void Start()
    {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
            enemylocation = transform.position;
            mover = GetComponent<Mover>();
    }

    void Update()
        {
            if (health.IsDead())
            {
                return;
            }
            if (DistanceToPlayer() < chaseDistance && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                fighter.Attack(player);       
            }
            else if(timeSinceLastSawPlayer < suspTime)
            {
                GetComponent<ActionScheduler>().CancelCurrentAction();
            }
            else
            {
                Vector3 nextPosition = enemylocation;
                if (patrolPath != null)
                {
                    if (AtWaypoint())
                    {
                        timeSinceArriveWaypoint = 0;
                        CycleWaypoint();
                    }
                    nextPosition = GetNextWaypoint();
                }
                if (timeSinceArriveWaypoint > waypointLifetime) {
                    mover.StartMoveAction(nextPosition,patrolSpeedFraction);
                }
            }
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArriveWaypoint += Time.deltaTime;
        }

        private Vector3 GetNextWaypoint()
        {
            return patrolPath.GetWaypointPosition(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceWaypoint = Vector3.Distance(transform.position, GetNextWaypoint());
            return distanceWaypoint < waypointTolerance;
        }

        private float DistanceToPlayer()
        {
            return Vector3.Distance(player.transform.position, transform.position);
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}