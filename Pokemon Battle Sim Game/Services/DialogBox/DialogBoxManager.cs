using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Services.Content;

namespace Pokemon_Battle_Sim_Game.Services.DialogBox;

public class DialogBoxManager // This class is responsible for managing the dialog boxes
{
    private readonly Queue<DialogBox> dialogBoxQueue = new(); //uses queues so that the dialog boxes are shown in the order they were added and dequeued whenever need be
    private DialogBox currentDialogBox;
    private readonly IContentLoader contentLoader;

    public DialogBoxManager(IContentLoader contentLoader)
    {
        this.contentLoader = contentLoader;
    }

    public bool BoxActive => dialogBoxQueue.Any() || currentDialogBox != null;

    public void AppendBoxIntoQueue(DialogBox dialogBox) //Appends a dialog box into the queue
    {
        dialogBoxQueue.Enqueue(dialogBox);
        ShowNextBoxInQueue();
    }

    private void ShowNextBoxInQueue()
    {
        if (!dialogBoxQueue.Any() || currentDialogBox != null) return;
        currentDialogBox = dialogBoxQueue.Dequeue();
        currentDialogBox.LoadContent(contentLoader);
    }

    public void UpdateCurrentWindow(double gameTime) //this is used for phases and checks if the current dialog box is done so that the next one can be shown
    {
        if (currentDialogBox == null) return;
        currentDialogBox.Update(gameTime);
        if (!currentDialogBox.IsDone) return;
        currentDialogBox = null;
        ShowNextBoxInQueue();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        currentDialogBox?.Draw(spriteBatch);
    }
}
