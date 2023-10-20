using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable 1591 // use this to disable comment warnings

namespace Fall2020_CSC403_Project.code {
  public class BattleCharacter : Character {
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }
    private float strength;
    public int level { get; private set; }
    public int xp { get; private set; }

    public event Action<int> AttackEvent;

    public BattleCharacter(Vector2 initPos, Collider collider) : base(initPos, collider) {
      MaxHealth = 20 + (2*level);
      strength = 2 + level;
      Health = MaxHealth;
    }

    public void OnAttack(int amount) {
      AttackEvent((int)(amount * strength));
    }

    public void AlterHealth(int amount) {
      Health += amount;
    }

    public void levelup()
        {
            Health += 8;
            strength += 2;
        }

    public void AddXP(int amount)
        {
            xp += amount;
            if (100 % xp == 0)
            {
                levelup();
            }
        }
  }
}
