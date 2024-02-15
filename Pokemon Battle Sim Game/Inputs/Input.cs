using System;
using Pokemon_Battle_Sim_Game.EventArg;

namespace Pokemon_Battle_Sim_Game.Inputs
{
    public abstract class Input
    {
        private static bool IsInputLocked => false;

        private event EventHandler<NewInputEvent> newInput; 
        private double elapsedTime;
        private double inputCooldown; 

        public event EventHandler<NewInputEvent> NewInput
        {
            add => newInput += value;
            remove => newInput -= value;
        }

        public void Update(double gameTime)
        {
            if (IsInputLocked || inputCooldown > 0)
            {
                elapsedTime += gameTime;
                if (!(elapsedTime > gameTime)) return;
                elapsedTime = 0;
                inputCooldown = 0;
                return;
            }
            CheckInput();
        }

        protected abstract void CheckInput();

        protected void SendNewInput(Enumerables.Inputs inputs)
        {
            newInput?.Invoke(this, new NewInputEvent(inputs));
        }
    }
}
