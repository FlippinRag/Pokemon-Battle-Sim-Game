namespace Pokemon_Battle_Sim_Game.Services.DialogBox
{
    public interface IDialogBoxQueuer // This interface is responsible for queuing the dialog boxes
    {
        void AppendBoxIntoQueue(DialogBox dialogBox);
        bool BoxActive { get; }
    }
}
