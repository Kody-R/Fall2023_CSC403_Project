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
    public int level;
    public int xp;

    public event Action<int> AttackEvent;

    public BattleCharacter(Vector2 initPos, Collider collider) : base(initPos, collider) {
      MaxHealth = 20;
      strength = 2;
      Health = MaxHealth;
    }

    public void OnAttack(int amount) {
      AttackEvent((int)(amount * strength));
    }

    public void AlterHealth(int amount) {
      Health += amount;
    }

    public void levelUp()     //levelUp function that will give the player more health and strength as they level up 
        {
            level += 1;
            AlterHealth(MaxHealth);
            MaxHealth += 4;
            strength += 1;
        }

    public void AddXP(int amount)   //AddXP function that will be called when a player defeats a character or picks up an xp item
        {
            xp += amount;
            if (xp % 100 == 0)
            {
                levelUp();
            }
        }
  }
}
