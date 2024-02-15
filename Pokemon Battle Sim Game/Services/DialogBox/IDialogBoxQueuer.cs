namespace Pokemon_Battle_Sim_Game.Services.DialogBox
{
    public interface IDialogBoxQueuer
    {
        void AppendBoxIntoQueue(DialogBox dialogBox);
        bool BoxActive { get; }
    }
}
