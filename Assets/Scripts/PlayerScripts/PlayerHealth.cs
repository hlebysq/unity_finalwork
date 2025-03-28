using System;
using UnityEngine;

namespace PlayerScripts
{
    [Obsolete("Needs to be rewritten")]
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] float Infection;
        [SerializeField] float Madness;

        private float damage_dealt = 0;
        private float damage_restored = 0;
        private float last_healed = 0;
        private float last_damaged = 0;

        public float regenTick = 0.5f;
        public float regenDelay = 3f;
        void Start()
        {
            Infection = 0;
            Madness = 0;
        }

        // Update is called once per frame
        void Update()
        {
            Infection = Mathf.Clamp(Infection, 0, 100);
            Madness = Mathf.Clamp(Madness, 0, 100);

            if ((Time.time - last_damaged >= regenDelay) && (Time.time - last_healed >= regenTick) && (damage_restored < damage_dealt / 2))
            {
                Infection -= 1;
                Infection = Mathf.Clamp(Infection, 0, 100);
            }

            if (Infection == 100)
            {
                TurnToMonster();
                Infection = 0;
                Madness = 100;
            }

        }

        void TurnToMonster()
        {
            Debug.Log("Player is a monster now");
        }

        public void InflictDamage(float damage)
        {
            Infection += damage;
            Infection = Mathf.Clamp(Infection, 0, 100);
            damage_dealt = Mathf.Clamp(damage_dealt, 0, 100);
            damage_restored = 0;
            last_damaged = Time.time;
            last_healed = Time.time;
        }
    }
}
