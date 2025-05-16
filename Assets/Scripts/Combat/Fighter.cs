using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour , IAction
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponRange;
        [SerializeField] float weaponDamage = 10f;

        Health targetObject;
        float timeSinceLastAttack;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (targetObject == null)
            {
                return;
            }           
            if(targetObject.IsDead()== true)
            {
                GetComponent<Animator>().ResetTrigger("Attack");
                Cancel();
                return;
            }

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(targetObject.transform.position,1f);
            }
            else
            {
                AttackMethod();
                GetComponent<Mover>().Cancel();
            }
        }

        private void AttackMethod()
        {
            transform.LookAt(targetObject.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0f;

            }

        }
        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget== null)
            {
                return false;
            }
            Health healtToTest = GetComponent<Health>();
            return healtToTest != null && !healtToTest.IsDead();
        }
        public void Attack(GameObject target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            targetObject = target.GetComponent<Health>();
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("Attack");
        }

        void Hit()
        {
            if (targetObject == null)
            {
                return;
            }
            targetObject.takeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, targetObject.transform.position) < weaponRange;
        }


        public void Cancel()
        {
            StopAttack();
            targetObject = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("Attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }
}
