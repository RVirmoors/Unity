using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    public InputField inputField;

    GameController controller;

    private void Awake()
    {
        controller = GetComponent<GameController>();
        inputField.onEndEdit.AddListener(AcceptStringInput);
        Debug.Log("init textinput");
    }
    void AcceptStringInput (string userInput)
    {
        userInput = userInput.ToLower();
        controller.LogStringWithReturn(userInput);

        Debug.Log("user said: " + userInput);

        char[] delimiterChars = { ' ' };
        string[] separatedInputWords = userInput.Split(delimiterChars);

        for (int i = 0; i < controller.inputActions.Length; i++)
        {
            InputAction inputAction = controller.inputActions[i];
            if (inputAction.keyWord == separatedInputWords[0])
            {// verb
                inputAction.RespondToInput(controller, separatedInputWords);
            }
        }

        InputComplete();
    }

    void InputComplete()
    {
        controller.DisplayLoggedText();
        inputField.ActivateInputField();
        inputField.text = null;
    }

}
