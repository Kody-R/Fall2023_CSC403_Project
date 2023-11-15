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
    public int strength { get; private set; }
    public int level { get; private set; }
    public int xp;
    public int lives;
    public int previousLevel;
    public int defeated;

    public event Action<int> AttackEvent;

    public BattleCharacter(Vector2 initPos, Collider collider) : base(initPos, collider) {
      level = 1;
      MaxHealth = 20 + (2+level);
      strength = 2 + level;
      Health = MaxHealth;
      lives = 3;
      defeated = 0;
    }

    public void OnAttack(int amount) {

      AttackEvent((int)(amount * strength));
    }

    public void AlterHealth(int amount) {
      Health += amount;
    }

    public void AlterLives()
    {
        lives -= 1;
    }

        public void levelUp()     //levelUp function that will give the player more health and strength as they level up 
        {
            previousLevel = level;
            level += 1;
            //AlterHealth(8);
            MaxHealth += 2;
            //strength += level;
        }

    public void AddXP(int amount)   //AddXP function that will be called when a player defeats a character or picks up an xp item
        {
            xp += amount;
            if (xp % 100 == 0)
            {
                levelUp();
            }
        }

    public void setLevel(int P_level)
        {
            level += P_level - 1;
        }
  }
}
