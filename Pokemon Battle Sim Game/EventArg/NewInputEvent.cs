using System;

namespace Pokemon_Battle_Sim_Game.EventArg
{
    public class NewInputEvent : EventArgs
    {
        public Enumerables.Inputs Inputs { get; }
        public NewInputEvent(Enumerables.Inputs inputs)
        {
            Inputs = inputs;
        }
    }
}
