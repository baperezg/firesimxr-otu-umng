using UnityEngine;
using TMPro;

public class EmergencyDialer : MonoBehaviour
{
    [Header("Phone Stats")]
    public TMP_Text inputText; 
    public GameObject respondentsUI;
    private string dialedNumbers = "";
    public string Numbers;

    [Header("Task Ui")]
    public TextMeshProUGUI taskDone;
    public bool isCompleted = false;

    private void Start()
    {
        isCompleted = false;
    }

    public void OnNumberButtonClicked(int number)
    {
        if (dialedNumbers.Length < 3)
        {
            dialedNumbers += number.ToString();
            UpdateNumberDisplay(dialedNumbers);
        }
    }

    public void OnCallButtonPressed()
    {
        if (dialedNumbers.Length == 3)
        {
            if (dialedNumbers == Numbers)
            {
                ShowRespondentsOnTheWay();
                taskDone.fontStyle = FontStyles.Strikethrough;
                isCompleted = true;
            }
            else
            {
                ResetDialer();
            }
        }
    }

    private void ResetDialer()
    {
        dialedNumbers = "";
        UpdateNumberDisplay(dialedNumbers);
    }

    private void ShowRespondentsOnTheWay()
    {
        respondentsUI.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void UpdateNumberDisplay(string numbers)
    {
        inputText.text = numbers;
    }
}
