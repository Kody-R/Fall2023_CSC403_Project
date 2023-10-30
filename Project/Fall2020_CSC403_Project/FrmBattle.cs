using Fall2020_CSC403_Project.code;
using Fall2020_CSC403_Project.Properties;
using System;
using System.Drawing;
using System.Media;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project {
  public partial class FrmBattle : Form {
    public static FrmBattle instance = null;
    private Enemy enemy;
    private Player player;

    private FrmBattle() {
      InitializeComponent();
      player = Game.player;
    }

    public void Setup() {
      // update for this enemy
      picEnemy.BackgroundImage = enemy.Img;
      picEnemy.Refresh();
      BackColor = enemy.Color;
      picBossBattle.Visible = false;

      // Observer pattern
      enemy.AttackEvent += PlayerDamage;
      player.AttackEvent += EnemyDamage;

      // show health
      UpdateHealthBars();
    }

    public void SetupForBossBattle() {
      picBossBattle.Location = Point.Empty;
      picBossBattle.Size = ClientSize;
      picBossBattle.Visible = true;

      SoundPlayer simpleSound = new SoundPlayer(Resources.final_battle);
      simpleSound.Play();

      tmrFinalBattle.Enabled = true;
    }

    public static FrmBattle GetInstance(Enemy enemy) {
      if (instance == null) {
        instance = new FrmBattle();
        instance.enemy = enemy;
        instance.Setup();
      }
      return instance;
    }

    private void UpdateHealthBars() {
      float playerHealthPer = player.Health / (float)player.MaxHealth;
      float enemyHealthPer = enemy.Health / (float)enemy.MaxHealth;

      const int MAX_HEALTHBAR_WIDTH = 226;
      lblPlayerHealthFull.Width = (int)(MAX_HEALTHBAR_WIDTH * playerHealthPer);
      lblEnemyHealthFull.Width = (int)(MAX_HEALTHBAR_WIDTH * enemyHealthPer);

      lblPlayerHealthFull.Text = player.Health.ToString();
      lblEnemyHealthFull.Text = enemy.Health.ToString();

            if (player.Health <= 0)
            {
                playerDeath();
            }

            if (enemy.Health <= 0)
            {
                player.AddXP(50);    //Calling the AddXP function to give the character more xp and possibly level up
                enemyDeath(enemy);
            }
    }
 
        private void playerDeath()
        {
            // Create a transparent panel to cover the form
            Panel panelDeathScreen = new Panel();
            panelDeathScreen.Name = "panelDeathScreen";
            panelDeathScreen.Size = this.ClientSize;
            panelDeathScreen.BackColor = Color.Transparent;
            // set the background to death screen and make it cover the window
            panelDeathScreen.BackgroundImage = Properties.Resources.DeathScreen;
            panelDeathScreen.BackgroundImageLayout = ImageLayout.Stretch;

            // Make the panel visible to cover the entire form
            panelDeathScreen.Visible = true;

            // Add the panel to the form's Controls collection
            Controls.Add(panelDeathScreen);
        }

        private void enemyDeath(Enemy enemy)
        {
            instance = null;
            enemy.Collider.MovePosition(0,0);
        }

    private void btnAttack_Click(object sender, EventArgs e)
    {
        player.OnAttack(-4);
        if (enemy.Health > 0)
        {
            enemy.OnAttack(-2);
        }
            UpdateHealthBars();
        }

    private void EnemyDamage(int amount)
    {
        enemy.AlterHealth(amount);
    }

    private void PlayerDamage(int amount)
    {
        player.AlterHealth(amount);
    }

    private void tmrFinalBattle_Tick(object sender, EventArgs e)
    {
        picBossBattle.Visible = false;
        tmrFinalBattle.Enabled = false;
    }
}
}
