using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesis
{
    class Statistics
    {
        public double AttackSpeed { get; set; }
        public float AngularVelocity { get; set; }
        public float Acceleration { get; set; }
        public int Health { get; set; }

        public Statistics(double attackSpeed, float angularVelocity, float acceleration, int health)
        {
            AttackSpeed = attackSpeed;
            AngularVelocity = angularVelocity;
            Acceleration = acceleration;
            Health = health;
        }
    }
}
