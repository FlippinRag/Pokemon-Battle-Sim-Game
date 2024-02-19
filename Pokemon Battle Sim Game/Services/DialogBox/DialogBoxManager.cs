using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Pokemon_Battle_Sim_Game.Services.Content;

namespace Pokemon_Battle_Sim_Game.Services.DialogBox;

public class DialogBoxManager
{
    private readonly Queue<DialogBox> dialogBoxQueue = new();
    private DialogBox currentDialogBox;
    private readonly IContentLoader contentLoader;

    public DialogBoxManager(IContentLoader contentLoader)
    {
        this.contentLoader = contentLoader;
    }

    public bool BoxActive => dialogBoxQueue.Any() || currentDialogBox != null;

    public void AppendBoxIntoQueue(DialogBox dialogBox)
    {
        dialogBoxQueue.Enqueue(dialogBox);
        ShowNextBoxInQueue();
    }

    public void ShowNextBoxInQueue()
    {
        if (!dialogBoxQueue.Any() || currentDialogBox != null) return;
        currentDialogBox = dialogBoxQueue.Dequeue();
        currentDialogBox.LoadContent(contentLoader);
    }

    public void UpdateCurrentWindow(double gameTime)
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
