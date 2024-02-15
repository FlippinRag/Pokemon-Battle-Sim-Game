using System;

namespace Pokemon_Battle_Sim_Game.Display.DisplayTransition
{
    public class DisplayTransitionFadeIn : DisplayTransitionFade
    {
        public DisplayTransitionFadeIn(int displayWidth, int displayHeight, byte fadeStepCount) : base(displayWidth, displayHeight, fadeStepCount)
        {
        }

        public override void Start()
        {
            Alpha = 255;
            IsDone = false;
        }

        public override void Update()
        {
            Alpha -= FadeStepCount;
            if (Math.Abs(Alpha) < 1)
                IsDone = true; 
        }
    }
}
