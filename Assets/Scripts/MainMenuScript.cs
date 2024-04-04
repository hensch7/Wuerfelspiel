using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private UIDocument document;

    private Button button;
    private DropdownField dropdownField;
    private IntegerField integerField;

    // Start is called before the first frame update
    // All UI Fields get selected
    void Start()
    {
        document = GetComponent<UIDocument>();

        button = document.rootVisualElement.Q("startButton") as Button;
        button?.RegisterCallback<ClickEvent>(OnPlayGameClick);

        dropdownField = document.rootVisualElement.Q<DropdownField>("diceType");
        integerField = document.rootVisualElement.Q<IntegerField>("diceAmount");

    }
    // The game starts and the settings are saved for later use
    private void OnPlayGameClick(ClickEvent evt)
    {
        Debug.Log("Button registered");
        
        // Save settings
        PlayerPrefs.SetString("diceType", dropdownField.value);
        PlayerPrefs.SetInt("diceAmount", integerField.value);
        
        SceneManager.LoadScene("GameScene");
    }
}