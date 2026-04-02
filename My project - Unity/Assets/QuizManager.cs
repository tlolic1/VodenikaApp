using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI[] answerTexts;
    public Button[] answerButtons;
    public GameObject mainMenuButton; // Dugme za povratak
    
    [Header("Quiz Data")]
    private string[] questions = new string[5];
    private string[][] answers = new string[5][];
    private int[] correctAnswers = new int[5];
    
    private int currentQuestionIndex = 0;
    private int score = 0;
    private bool answerSelected = false;
    
    void Start()
    {
        LoadQuizData();
        DisplayQuestion();
        
        // Sakrij Main Menu button na početku
        if (mainMenuButton != null)
            mainMenuButton.SetActive(false);
    }
    
    void LoadQuizData()
    {
        // PITANJE 1
        questions[0] = "What is the common name for Impatiens?";
        answers[0] = new string[] { "Busy Lizzie", "Rose", "Tulip" };
        correctAnswers[0] = 0;
        
        // PITANJE 2
        questions[1] = "What type of light does Impatiens prefer?";
        answers[1] = new string[] { "Full sun", "Partial shade", "Complete darkness" };
        correctAnswers[1] = 1;
        
        // PITANJE 3
        questions[2] = "How often should Impatiens be watered?";
        answers[2] = new string[] { "Once a month", "When soil is dry", "Never" };
        correctAnswers[2] = 1;
        
        // PITANJE 4
        questions[3] = "What is the native region of Impatiens?";
        answers[3] = new string[] { "Europe", "Africa and Asia", "America" };
        correctAnswers[3] = 2;
        
        // PITANJE 5
        questions[4] = "What happens to Impatiens seed pods when touched?";
        answers[4] = new string[] { "Nothing", "They explode", "They shrink" };
        correctAnswers[4] = 1;
    }
    
    void DisplayQuestion()
    {
        if (currentQuestionIndex < questions.Length)
        {
            questionText.text = questions[currentQuestionIndex];
            
            for (int i = 0; i < 3; i++)
            {
                answerTexts[i].text = answers[currentQuestionIndex][i];
                
                if (answerButtons != null && answerButtons.Length > i)
                {
                    answerButtons[i].interactable = true;
                    ColorBlock colors = answerButtons[i].colors;
                    colors.normalColor = Color.white;
                    colors.disabledColor = Color.white;
                    answerButtons[i].colors = colors;
                }
            }
            
            answerSelected = false;
        }
    }
    
    public void OnAnswerSelected(int answerIndex)
    {
        if (answerSelected) return;
        answerSelected = true;
        
        foreach (Button btn in answerButtons)
        {
            btn.interactable = false;
        }
        
        if (answerIndex == correctAnswers[currentQuestionIndex])
        {
            score++;
            SetButtonColor(answerButtons[answerIndex], new Color(0.3f, 0.8f, 0.3f));
            Debug.Log("Correct! Score: " + score);
        }
        else
        {
            SetButtonColor(answerButtons[answerIndex], new Color(0.9f, 0.3f, 0.3f));
            SetButtonColor(answerButtons[correctAnswers[currentQuestionIndex]], new Color(0.3f, 0.8f, 0.3f));
            Debug.Log("Wrong! Score: " + score);
        }
        
        StartCoroutine(WaitAndContinue());
    }
    
    IEnumerator WaitAndContinue()
    {
        yield return new WaitForSeconds(1.5f);
        
        currentQuestionIndex++;
        
        if (currentQuestionIndex < questions.Length)
        {
            DisplayQuestion();
        }
        else
        {
            ShowResults();
        }
    }
    
    void SetButtonColor(Button button, Color color)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = color;
        colors.disabledColor = color;
        button.colors = colors;
    }
    
    void ShowResults()
    {
        questionText.text = "Quiz Complete!\n\nYour Score: " + score + " / 5";
        
        if (score == 5)
            questionText.text += "\n\nPerfect! You're an Impatiens expert! 🌸";
        else if (score >= 3)
            questionText.text += "\n\nGreat job! You know your plants! 🌿";
        else
            questionText.text += "\n\nKeep learning about Impatiens! 🌱";
        
        // Sakrij answer buttone
        for (int i = 0; i < answerTexts.Length; i++)
        {
            answerTexts[i].transform.parent.gameObject.SetActive(false);
        }
        
        // Prikaži Main Menu button
        if (mainMenuButton != null)
            mainMenuButton.SetActive(true);
    }
    
    // Funkcija za povratak na Main Menu
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}