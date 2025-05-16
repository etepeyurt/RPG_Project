using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.Core{ 
public class Health : MonoBehaviour
{
        [SerializeField] float health = 100f;
        bool isDead = false;
        public bool IsDead()
        {
            return isDead;
        }
        public void takeDamage(float damage)
        {
            health = Mathf.Max(health - damage , 0) ;
            if (health == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if(isDead == true)
            {
                return;
            }
            isDead = true;
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}