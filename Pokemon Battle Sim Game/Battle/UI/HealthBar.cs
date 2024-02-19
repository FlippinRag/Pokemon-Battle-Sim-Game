using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Services.Content;

namespace Pokemon_Battle_Sim_Game.Battle.UI
{
    public class HealthBar
    {
        private int maxHp;
        private enum HealthBarState
        {
            GreenState,
            YellowState,
            RedState
        };

        private int CurrentHp { get; set; }

        private HealthBarState currentHealthBarState;
        private Texture2D hpBarTexture;
        private int targetHp;
        private int barWidth; 

        public HealthBar(int currentHp, int maxHp)
        {
            CurrentHp = currentHp;
            this.maxHp = maxHp;
            targetHp = currentHp;
            UpdateHpBarState();
        }

        public void LoadContent(IContentLoader contentLoader)
        {
            hpBarTexture = contentLoader.LoadTexture("Battle/Gui/HealthBar");
        }

        public void UpdateHp(int currentHealth, int maxHealth)
        {
            CurrentHp = currentHealth;
            targetHp = currentHealth; // Update targetHp
            maxHp = maxHealth;
            UpdateHpBarState();
        }

        private void UpdateHpBarState()
        {
            var percent = (float)CurrentHp / maxHp;

            currentHealthBarState = percent switch
            {
                _ when percent < 0.2 => HealthBarState.RedState,
                _ when percent < 0.5 => HealthBarState.YellowState,
                _ => HealthBarState.GreenState
            };

            barWidth = (int)(percent * 46);
        }

        public void Update()
        {
            if (targetHp < CurrentHp) // Only decrease CurrentHp
            {
                CurrentHp -= 1;
                UpdateHpBarState();
            }
            
            UpdateHpBarState();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            // if ()
            spriteBatch.Draw(hpBarTexture, new Rectangle((int)position.X + 44, (int)position.Y + 25, barWidth, 7), new Rectangle(0, 3 * (int)currentHealthBarState, 1, 3), Color.White);
        }

    }
}
