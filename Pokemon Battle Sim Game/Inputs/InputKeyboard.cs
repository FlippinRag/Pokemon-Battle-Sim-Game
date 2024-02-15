using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Pokemon_Battle_Sim_Game.Inputs
{
    public class InputKeyboard : Input
    {
        private KeyboardState keyboardCondition;
        private KeyboardState previousKeyboardCondition;
        private Keys previousKey;
        private readonly Dictionary<Keys, Enumerables.Inputs> keyInputMap = new()
        {
            { Keys.S, Enumerables.Inputs.Box1 },
            { Keys.D, Enumerables.Inputs.Box2 },
            { Keys.X, Enumerables.Inputs.Box3 },
            { Keys.C, Enumerables.Inputs.Box4 },
            { Keys.Down, Enumerables.Inputs.DownArrow }
        };

        protected override void CheckInput()
        {
            keyboardCondition = Keyboard.GetState();
            if (keyboardCondition.IsKeyUp(previousKey) && previousKey != Keys.None)
            {
                SendNewInput(Enumerables.Inputs.None);
            }

            foreach (var keyInputPair in keyInputMap)
            {
                CheckKeyState(keyInputPair.Key, keyInputPair.Value);
            }

            previousKeyboardCondition = keyboardCondition;
        }

        private void CheckKeyState(Keys key, Enumerables.Inputs sendInputs)
        {
            if (!keyboardCondition.IsKeyDown(key) || !previousKeyboardCondition.IsKeyUp(key)) return;
            SendNewInput(sendInputs);
            previousKey = key;

        }
    }
}