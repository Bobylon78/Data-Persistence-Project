using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public InputField nameInput;

    public void OnStartClicked()
    {
        string name = nameInput.text;
        GameManager.Instance.currentPlayerName = name;
        GameManager.Instance.StartGame(name);
    }
}
