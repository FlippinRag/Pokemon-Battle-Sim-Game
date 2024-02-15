using System;

namespace Pokemon_Battle_Sim_Game.Display.DisplayTransition
{
    public class DisplayTransitionFadeOut : DisplayTransitionFade
    {
        public DisplayTransitionFadeOut(int displayWidth, int displayHeight, byte fadeStepCount) : base(displayWidth, displayHeight, fadeStepCount)
        {
        }

        public override void Start()
        {
            Alpha = 0;
            IsDone = false; 
        }

        public override void Update()
        {
            Alpha += FadeStepCount;
            if (Math.Abs(Alpha - 255) < 1)
                IsDone = true; 
        }
    }
}
