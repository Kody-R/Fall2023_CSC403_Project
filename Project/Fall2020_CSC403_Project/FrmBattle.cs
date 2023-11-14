using Fall2020_CSC403_Project.code;
using Fall2020_CSC403_Project.Properties;
using System;
using System.Drawing;
using System.Media;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project
{
    public partial class FrmBattle : Form
    {
        public static FrmBattle instance = null;
        private Enemy enemy;
        private Player player;
        SoundPlayer soundtrack = new SoundPlayer(Resources.GameSoundtrack);
        public Random random = new Random();
        public int randInt;

        public FrmBattle(FrmLevel frmLevel)
        {
            InitializeComponent();
            player = Game.player;
            soundtrack.Stop();
            this.FormClosed += (s, args) => { instance = null; enemy.AttackEvent -= PlayerDamage; player.AttackEvent -= EnemyDamage; };
        }
        private void HandleControllerButtonPressed(int buttonIndex, FrmLevel frmLevel)
        {
            object dummySender = null;
            EventArgs dummyEventArgs = EventArgs.Empty;
            if (frmLevel != null && instance != null)
            {
                if (buttonIndex == 0)
                {
                    btnLightAttack_Click(dummySender, dummyEventArgs);
                }
                else if (buttonIndex == 1)
                {
                    btnHeavyAttack_Click(dummySender, dummyEventArgs);
                }
                else if (buttonIndex == 2)
                {
                    btnHeal_Click(dummySender, dummyEventArgs);
                }
                else if (buttonIndex == 3)
                {
                    btnFlee_Click(dummySender, dummyEventArgs);
                }
            }
              
                
            
        }

        public void Setup()
        {
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

        public void SetupForBossBattle()
        {
            picBossBattle.Location = Point.Empty;
            picBossBattle.Size = ClientSize;
            picBossBattle.Visible = true;

            SoundPlayer simpleSound = new SoundPlayer(Resources.final_battle);
            simpleSound.Play();


            tmrFinalBattle.Enabled = true;
        }

        public static FrmBattle GetInstance(Enemy enemy, FrmLevel frmLevel)
        {
            if (instance == null)
            {
                if (frmLevel != null)
                {
                    instance = new FrmBattle(frmLevel);
                    instance.enemy = enemy;
                    instance.Setup();
                    frmLevel.ControllerButtonPressed += instance.HandleControllerButtonPressed;
                }
                
            }
            return instance;
        }

        private void UpdateHealthBars()
        {
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

        private void btnLightAttack_Click(object sender, EventArgs e)
        {

            SoundPlayer light_Attack = new SoundPlayer(Resources.attack);
            light_Attack.Play();
            randInt = random.Next(1, 4);
            player.OnAttack(-randInt);
            if (enemy.Health > 0)
            {
                enemy.OnAttack(-2);
            }

            UpdateHealthBars();
            if (player.Health <= 0 || enemy.Health <= 0)
            {
                instance = null;
                Close();
                soundtrack.Play();
            }
        }

        private void btnAttack_Click(object sender, EventArgs e)
        {

            player.OnAttack(-4);
        }
        private void btnHeavyAttack_Click(object sender, EventArgs e)
        {
            SoundPlayer Heavy_Attack = new SoundPlayer(Resources.heavyattack);
            Heavy_Attack.Play();
            randInt = random.Next(3, 6);
            player.OnAttack(-randInt);
            if (enemy.Health > 0)
            {
                enemy.OnAttack(-2);
            }

            UpdateHealthBars();
            if (player.Health <= 0 || enemy.Health <= 0)
            {
                instance = null;
                Close();
                soundtrack.Play();
            }
        }

        private void btnHeal_Click(object sender, EventArgs e)
        {

            if (enemy.Health > 0)
            {
                enemy.OnAttack(-2);
            }

            randInt = random.Next(1, 6);
            while (player.MaxHealth < player.Health + randInt)
            {
                randInt--;
            }
            player.AlterHealth(randInt);


            UpdateHealthBars();
            if (player.Health <= 0 || enemy.Health <= 0)
            {
                instance = null;
                Close();
            }
        }

        private void btnFlee_Click(object sender, EventArgs e)
        {
            instance = null;
            Close();
        }


        private void EnemyDamage(int amount)
        {
            enemy.AlterHealth(amount);
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

            SoundPlayer death = new SoundPlayer(Resources.death);

            death.Play();

        }

        private void enemyDeath(Enemy enemy)
        {
            instance = null;
            enemy.Collider.MovePosition(0, 0);
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
