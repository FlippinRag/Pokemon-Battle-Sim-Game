using Pokemon_Battle_Sim_Game.Battle;

namespace Pokemon_Battle_Sim_Game.Services.Content
{
    public abstract class PlayerTestLoader //Loads a opponent
    {
        public static Player LoadPlayer(int id)
        {
            return new Player {Id = id, Name = "TEAM ROCKET GRUNT", TextureName = "Trainers/teamRocket"};
        }
    }
}
