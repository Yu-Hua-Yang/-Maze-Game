using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

public class InputManager
{
    private static InputManager instance;
    private Dictionary<Keys, List<Action>> keyHandlers;
    private KeyboardState previousKeyboardState;

    private InputManager()
    {
        keyHandlers = new Dictionary<Keys, List<Action>>();
    }

    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new InputManager();
            }
            return instance;
        }
    }

    public void AddKeyHandler(Keys key, Action action)
    {
        if (!keyHandlers.ContainsKey(key))
        {
            keyHandlers[key] = new List<Action>();
        }
        keyHandlers[key].Add(action);
    }

    public void Update()
    {
        KeyboardState keyboardState = Keyboard.GetState();

        foreach (var key in keyHandlers.Keys)
        {
            if (keyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key))
            {
                foreach (var action in keyHandlers[key])
                {
                    action.Invoke();
                }
            }
        }
        previousKeyboardState = keyboardState;
    }
}