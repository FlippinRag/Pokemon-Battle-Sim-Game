using System;

namespace Pokemon_Battle_Sim_Game.EventArg
{
    public class NewInputEvent : EventArgs
    { //EventArg for when a new input is sent
        public Enumerables.Inputs Inputs { get; }
        public NewInputEvent(Enumerables.Inputs inputs)
        {
            Inputs = inputs;
        }
    }
}
