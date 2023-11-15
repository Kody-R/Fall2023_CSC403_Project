using Fall2020_CSC403_Project.code;
using Fall2020_CSC403_Project.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace Fall2020_CSC403_Project {
    public class GameState
    {
        public int PlayerLevel { get; set; }
        public DateTime TimeBegin { get; set; }

        public GameState(int playerLevel, DateTime timeBegin)
        {
            PlayerLevel = 0;
            TimeBegin = TimeBegin;
        }
    }

    public partial class FrmLevel : Form {
    private Player player;

    private Enemy enemyPoisonPacket;
    private Enemy bossKoolaid;
    private Enemy enemyCheeto;
    private Item xpItem;
    private Character[] walls;

    private DateTime timeBegin;
    private FrmBattle frmBattle;
    private bool transLvl2;
    private GameState gameState;



    public FrmLevel() {
      InitializeComponent();
    }

    private void FrmLevel_Load(object sender, EventArgs e) {
      
      const int PADDING = 7;
      const int NUM_WALLS = 13;
      gameState = new GameState(1, DateTime.Now);

      SoundPlayer soundtrack = new SoundPlayer(Resources.GameSoundtrack);
      player = new Player(CreatePosition(picPlayer), CreateCollider(picPlayer, PADDING));
      bossKoolaid = new Enemy(CreatePosition(picBossKoolAid), CreateCollider(picBossKoolAid, PADDING));
      enemyPoisonPacket = new Enemy(CreatePosition(picEnemyPoisonPacket), CreateCollider(picEnemyPoisonPacket, PADDING));
      enemyCheeto = new Enemy(CreatePosition(picEnemyCheeto), CreateCollider(picEnemyCheeto, PADDING));
      xpItem = new Item(CreatePosition(picXpItem), CreateCollider(picXpItem, PADDING));

      bossKoolaid.Img = picBossKoolAid.BackgroundImage;
      enemyPoisonPacket.Img = picEnemyPoisonPacket.BackgroundImage;
      enemyCheeto.Img = picEnemyCheeto.BackgroundImage;
      xpItem.Img = picXpItem.BackgroundImage;

      bossKoolaid.Color = Color.Red;
      enemyPoisonPacket.Color = Color.Green;
      enemyCheeto.Color = Color.FromArgb(255, 245, 161);
      xpItem.Color = Color.Orange;

      walls = new Character[NUM_WALLS];
      for (int w = 0; w < NUM_WALLS; w++) {
        PictureBox pic = Controls.Find("picWall" + w.ToString(), true)[0] as PictureBox;
        walls[w] = new Character(CreatePosition(pic), CreateCollider(pic, PADDING));
      }
      transLvl2 = false;
      Game.player = player;
      timeBegin = DateTime.Now;
      soundtrack.Play();
    
    }

    private Vector2 CreatePosition(PictureBox pic) {
      return new Vector2(pic.Location.X, pic.Location.Y);
    }

    private Collider CreateCollider(PictureBox pic, int padding) {
      Rectangle rect = new Rectangle(pic.Location, new Size(pic.Size.Width - padding, pic.Size.Height - padding));
      return new Collider(rect);
    }

    private void FrmLevel_KeyUp(object sender, KeyEventArgs e) {
      player.ResetMoveSpeed();
    }

    private void tmrUpdateInGameTime_Tick(object sender, EventArgs e) {
      TimeSpan span = DateTime.Now - timeBegin;
      string time = span.ToString(@"hh\:mm\:ss");
      lblInGameTime.Text = "Time: " + time.ToString();
    }

     private void tmrUpdateInGameLevel_Tick(object sender, EventArgs e)
     {
         int playerLevel = player.level;

         // Check if the level has changed
         if (playerLevel != player.previousLevel)
         {
             lblInGameLevel.Text = "Level: " + playerLevel.ToString("D2");
             player.previousLevel = playerLevel;
         }
     }


        private void tmrPlayerMove_Tick(object sender, EventArgs e) {

      player.Move();

      // check collision with walls
      if (HitAWall(player)) {
        player.MoveBack();
      }

      // check collision with enemies
      if (HitAChar(player, enemyPoisonPacket)) {
        Fight(enemyPoisonPacket);
        
      }
      else if (HitAChar(player, enemyCheeto)) {
        Fight(enemyCheeto);
      }
            if (HitAChar(player, bossKoolaid))
            {
                Fight(bossKoolaid);

            }
            else if (HitAChar(player, xpItem))
            {
                Pickup(xpItem);
            }
      // update player's picture box
      picPlayer.Location = new Point((int)player.Position.x, (int)player.Position.y);
            // Check if the boss is defeated
            if ((player.defeated - 1) > 3 && !transLvl2)
            {
                gameState.PlayerLevel = player.level;
                gameState.TimeBegin = DateTime.Now;

                transLvl2 = true;
                ResetLevel();

                // Show the next level form
                FrmLevel2 nextLevelForm = new FrmLevel2(gameState);
                nextLevelForm.Show();

                this.Hide();
            }
        }

    private bool HitAWall(Character c) {
      bool hitAWall = false;
      for (int w = 0; w < walls.Length; w++) {
        if (c.Collider.Intersects(walls[w].Collider)) {
          hitAWall = true;
          break;
        }
      }
      return hitAWall;
    }

    private bool HitAChar(Character you, Character other) {
      return you.Collider.Intersects(other.Collider);
    }
    private void Pickup(Item item) //Pickup function that will be called when a player picks up an item and this will give the player xp equal to one level
        {
            if (item.Color == Color.Orange) //This checks to see if the character has already picked up the item as to not give an infinite means of xp
            {
                player.ResetMoveSpeed();
                player.MoveBack();
                player.AddXP(100);
                item.Color = Color.Black;
                item.Collider.MovePosition(0, 0);
            }
            
            else
            {
                player.ResetMoveSpeed();
                player.MoveBack();
            }
        }
    private void Fight(Enemy enemy) {
      
      player.ResetMoveSpeed();
      player.MoveBack();
      frmBattle = FrmBattle.GetInstance(enemy);
      frmBattle.Show();
    }

    private void FrmLevel_KeyDown(object sender, KeyEventArgs e) {
      switch (e.KeyCode) {
        case Keys.Left:
          player.GoLeft();
          break;

        case Keys.Right:
          player.GoRight();
          break;

        case Keys.Up:
          player.GoUp();
          break;

        case Keys.Down:
          player.GoDown();
          break;

        default:
          player.ResetMoveSpeed();
          break;
      }
    }

    private void lblInGameTime_Click(object sender, EventArgs e) {

    }
    private void lblInGameLevel_Click(object sender, EventArgs e)
        {

        }

       private void ResetLevel()
       {
           // Reset player position
           player.ResetMoveSpeed();
           player.MoveBack();

           // Clear enemies
           enemyPoisonPacket.Collider.MovePosition(0, 0);
           enemyCheeto.Collider.MovePosition(0, 0);
           bossKoolaid.Collider.MovePosition(0, 0);

           // Reset item state
           xpItem.Color = Color.Orange;
           xpItem.Collider.MovePosition(0, 0);

           // Reset walls
           for (int w = 0; w < walls.Length; w++)
           {
               walls[w].Collider.MovePosition(0, 0);
           }
       }
    }

    public partial class FrmLevel2 : Form
    {
        private Player player;

        private Enemy enemyKiss;
        private Enemy evilPringle;
        private Enemy enemyCandyCorn;
        private Item xpItem;
        private Character[] walls;

        private DateTime timeBegin;
        private FrmBattle frmBattle;
        private GameState gameState;
        private bool transLvlB;
       


        public FrmLevel2(GameState gameState)
        {
           InitializeComponent_2();
            this.gameState = gameState;
            
        }

        private void FrmLevel2_Load(object sender, EventArgs e)
        {

            const int PADDING = 7;
            const int NUM_WALLS = 11;


            player = new Player(CreatePosition(picPlayer), CreateCollider(picPlayer, PADDING));
            evilPringle = new Enemy(CreatePosition(picPringle), CreateCollider(picPringle, PADDING));
            enemyCandyCorn = new Enemy(CreatePosition(picEnemyCandyCorn), CreateCollider(picEnemyCandyCorn, PADDING));
            enemyKiss = new Enemy(CreatePosition(picEnemyKiss), CreateCollider(picEnemyKiss, PADDING));
            xpItem = new Item(CreatePosition(picXpItem), CreateCollider(picXpItem, PADDING));

            evilPringle.Img = picPringle.BackgroundImage;
            enemyCandyCorn.Img = picEnemyCandyCorn.BackgroundImage;
            enemyKiss.Img = picEnemyKiss.BackgroundImage;
            xpItem.Img = picXpItem.BackgroundImage;

            player.setLevel(gameState.PlayerLevel);
            evilPringle.Color = Color.Red;
            enemyCandyCorn.Color = Color.Orange;
            enemyKiss.Color = Color.Gray;
            xpItem.Color = Color.Orange;

            walls = new Character[NUM_WALLS];
            for (int w = 0; w < NUM_WALLS; w++)
            {
                PictureBox pic = Controls.Find("picWall" + w.ToString(), true)[0] as PictureBox;
                walls[w] = new Character(CreatePosition(pic), CreateCollider(pic, PADDING));
            }

            SoundPlayer soundtrack = new SoundPlayer(Resources.GameSoundtrack);
            soundtrack.Play();

            Game.player = player;
            timeBegin = gameState.TimeBegin;
            transLvlB = false;

        }

        private Vector2 CreatePosition(PictureBox pic)
        {
            return new Vector2(pic.Location.X, pic.Location.Y);
        }

        private Collider CreateCollider(PictureBox pic, int padding)
        {
            Rectangle rect = new Rectangle(pic.Location, new Size(pic.Size.Width - padding, pic.Size.Height - padding));
            return new Collider(rect);
        }

        private void FrmLevel2_KeyUp(object sender, KeyEventArgs e)
        {
            player.ResetMoveSpeed();
        }

        private void tmrUpdateInGameTime_Tick(object sender, EventArgs e)
        {
            TimeSpan span = DateTime.Now - timeBegin;
            string time = span.ToString(@"hh\:mm\:ss");
            lblInGameTime.Text = "Time: " + time.ToString();
        }
        private void tmrUpdateInGameLevel_Tick(object sender, EventArgs e)
        {
            int playerLevel = player.level;

            // Check if the level has changed
            if (playerLevel != player.previousLevel)
            {
                lblInGameLevel.Text = "Level: " + playerLevel.ToString("D2");
                player.previousLevel = playerLevel;
            }
        }
        private void tmrPlayerMove_Tick(object sender, EventArgs e)
        {
            // move player
            player.Move();

            // check collision with walls
            if (HitAWall(player))
            {
                player.MoveBack();
            }

            // check collision with enemies
            if (HitAChar(player, enemyKiss))
            {
                Fight(enemyKiss);

            }
            else if (HitAChar(player, enemyCandyCorn))
            {
                Fight(enemyCandyCorn);
            }
            if (HitAChar(player, evilPringle))
            {
                Fight(evilPringle);
            }
            else if (HitAChar(player, xpItem))
            {
                Pickup(xpItem);
            }
            // update player's picture box
            picPlayer.Location = new Point((int)player.Position.x, (int)player.Position.y);

            if ((player.defeated -1) > 3 && !transLvlB)
            {
                gameState.PlayerLevel = player.level;
                gameState.TimeBegin = DateTime.Now;

                transLvlB = true;
                ResetLevel();

                // Show the next level form
                FrmLevel_Boss nextLevelFormB = new FrmLevel_Boss(gameState);
                nextLevelFormB.Show();

                this.Hide();
            }
        }

        private bool HitAWall(Character c)
        {
            bool hitAWall = false;
            for (int w = 0; w < walls.Length; w++)
            {
                if (c.Collider.Intersects(walls[w].Collider))
                {
                    hitAWall = true;
                    break;
                }
            }
            return hitAWall;
        }

        private bool HitAChar(Character you, Character other)
        {
            return you.Collider.Intersects(other.Collider);
        }
        private void Pickup(Item item) //Pickup function that will be called when a player picks up an item and this will give the player xp equal to one level
        {
            if (item.Color == Color.Orange) //This checks to see if the character has already picked up the item as to not give an infinite means of xp
            {
                player.ResetMoveSpeed();
                player.MoveBack();
                player.AddXP(100);
                item.Color = Color.Black;
                item.Collider.MovePosition(0, 0);
            }

            else
            {
                player.ResetMoveSpeed();
                player.MoveBack();
            }
        }
        private void Fight(Enemy enemy)
        {

            player.ResetMoveSpeed();
            player.MoveBack();
            frmBattle = FrmBattle.GetInstance(enemy);
            frmBattle.Show();
        }

        private void FrmLevel2_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    player.GoLeft();
                    break;

                case Keys.Right:
                    player.GoRight();
                    break;

                case Keys.Up:
                    player.GoUp();
                    break;

                case Keys.Down:
                    player.GoDown();
                    break;

                default:
                    player.ResetMoveSpeed();
                    break;
            }
        }

        private void lblInGameTime_Click(object sender, EventArgs e)
        {

        }
        private void lblInGameLevel_Click(object sender, EventArgs e)
        {

        }

        private void ResetLevel()
        {
            // Reset player position
            player.ResetMoveSpeed();
            player.MoveBack();

            // Clear enemies
            evilPringle.Collider.MovePosition(0, 0);
            enemyCandyCorn.Collider.MovePosition(0, 0);
            enemyKiss.Collider.MovePosition(0, 0);

            // Reset item state
            xpItem.Color = Color.Orange;
            xpItem.Collider.MovePosition(0, 0);

            // Reset walls
            for (int w = 0; w < walls.Length; w++)
            {
                walls[w].Collider.MovePosition(0, 0);
            }
        }
    }

    public partial class FrmLevel_Boss : Form
    {
        private Player player;
        private Enemy bossKoolaid;
        
        private Character[] walls;

        private DateTime timeBegin;
        private FrmBattle frmBattle;
        private GameState gameState;


        public FrmLevel_Boss(GameState gameState)
        {
            InitializeComponent_Boss();
            this.gameState = gameState;
        }

        private void FrmLevel_Boss_Load(object sender, EventArgs e)
        {

            const int PADDING = 7;
            const int NUM_WALLS = 9;


            player = new Player(CreatePosition(picPlayer), CreateCollider(picPlayer, PADDING));
            bossKoolaid = new Enemy(CreatePosition(picBossKoolAid), CreateCollider(picBossKoolAid, PADDING));
            player.setLevel(gameState.PlayerLevel);

            bossKoolaid.Img = picBossKoolAid.BackgroundImage;

            bossKoolaid.Color = Color.Red;
           

            walls = new Character[NUM_WALLS];
            for (int w = 0; w < NUM_WALLS; w++)
            {
                PictureBox pic = Controls.Find("picWall" + w.ToString(), true)[0] as PictureBox;
                walls[w] = new Character(CreatePosition(pic), CreateCollider(pic, PADDING));
            }

            SoundPlayer soundtrack = new SoundPlayer(Resources.GameSoundtrack);
            soundtrack.Play();

            Game.player = player;
            timeBegin = DateTime.Now;

        }

        private Vector2 CreatePosition(PictureBox pic)
        {
            return new Vector2(pic.Location.X, pic.Location.Y);
        }

        private Collider CreateCollider(PictureBox pic, int padding)
        {
            Rectangle rect = new Rectangle(pic.Location, new Size(pic.Size.Width - padding, pic.Size.Height - padding));
            return new Collider(rect);
        }

        private void FrmLevel_Boss_KeyUp(object sender, KeyEventArgs e)
        {
            player.ResetMoveSpeed();
        }

        private void tmrUpdateInGameTime_Tick(object sender, EventArgs e)
        {
            TimeSpan span = DateTime.Now - timeBegin;
            string time = span.ToString(@"hh\:mm\:ss");
            lblInGameTime.Text = "Time: " + time.ToString();
        }
        private void tmrUpdateInGameLevel_Tick(object sender, EventArgs e)
        {
            string level = player.level.ToString();
            lblInGameLevel.Text = "Level: " + level;
        }
        private void tmrPlayerMove_Tick(object sender, EventArgs e)
        {
            // move player
            player.Move();

            // check collision with walls
            if (HitAWall(player))
            {
                player.MoveBack();
            }
            if (HitAChar(player, bossKoolaid))
            {
                Fight(bossKoolaid);
            }
            // update player's picture box
            picPlayer.Location = new Point((int)player.Position.x, (int)player.Position.y);
        }

        private bool HitAWall(Character c)
        {
            bool hitAWall = false;
            for (int w = 0; w < walls.Length; w++)
            {
                if (c.Collider.Intersects(walls[w].Collider))
                {
                    hitAWall = true;
                    break;
                }
            }
            return hitAWall;
        }

        private bool HitAChar(Character you, Character other)
        {
            return you.Collider.Intersects(other.Collider);
        }
        private void Pickup(Item item) //Pickup function that will be called when a player picks up an item and this will give the player xp equal to one level
        {
            if (item.Color == Color.Orange) //This checks to see if the character has already picked up the item as to not give an infinite means of xp
            {
                player.ResetMoveSpeed();
                player.MoveBack();
                player.AddXP(100);
                item.Color = Color.Black;
                item.Collider.MovePosition(0, 0);
            }

            else
            {
                player.ResetMoveSpeed();
                player.MoveBack();
            }
        }
        private void Fight(Enemy enemy)
        {

            player.ResetMoveSpeed();
            player.MoveBack();
            frmBattle = FrmBattle.GetInstance(enemy);
            frmBattle.Show();

            if (enemy == bossKoolaid)
            {
                frmBattle.SetupForBossBattle();
            }
        }

        private void FrmLevel_Boss_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    player.GoLeft();
                    break;

                case Keys.Right:
                    player.GoRight();
                    break;

                case Keys.Up:
                    player.GoUp();
                    break;

                case Keys.Down:
                    player.GoDown();
                    break;

                default:
                    player.ResetMoveSpeed();
                    break;
            }
        }

        private void lblInGameTime_Click(object sender, EventArgs e)
        {

        }
        private void lblInGameLevel_Click(object sender, EventArgs e)
        {

        }
    }
}
