using System;
using UnityEngine;

public class Localization : MonoBehaviour
{
    private string language;
    void Awake()
    {
        SetLanguage();
    }
    private void SetLanguage()
    {
        GameObject gameDataController = GameObject.Find("GameDataController");
        if (gameDataController is not null)
        {
            SaveData saveData = gameDataController.GetComponent<GameDataController>().LoadData();
            language = saveData._language;
            Debug.Log($"Current localization: {language}");
        }
    }
    public string GetDialogueText(string ownerName)
    {
        switch (language)
        {
            case "RUS":
                {
                    switch (ownerName)
                    {
                        case "textKnightGirl":
                            {
                                return "���� ������ �������, ������� ����� ��������! ����� ������ � ������ �� ������ ���!";
                            }
                        case "textGenie":
                            {
                                return "�� ����� ����! ����� � ������ �� ���� ����� ������!!!";
                            }
                        default: return "������ ��������!";
                    }
                }
            case "ENG":
                {
                    switch (ownerName)
                    {
                        case "textKnightGirl":
                            {
                                return "The Dragon can only be defeated by stunning with a stone! Find his and throw it on his head!";
                            }
                        case "textGenie":
                            {
                                return "Don't touch me! Else I'll bring the magic of the stones down on you!!!";
                            }
                        default: return "Translation Error!";
                    }
                }
            default: return "localization is not selected";
        }
    }
    public string GetGUIText(string text)
    {
        switch (language)
        {
            case "RUS":
                {
                    switch(text)
                    {
                        case "Balance":
                            {
                                return "������";
                            }
                        case "NextLevel":
                            {
                                return "��������� �������";
                            }
                        case "QuitGame":
                            {
                                return "����� �� ����";
                            }
                        case "Replay":
                            {
                                return "���������";
                            }
                        case "Resume":
                            {
                                return "����������";
                            }
                        case "MainMenu":
                            {
                                return "������� ����";
                            }
                        case "Settings":
                            {
                                return "���������";
                            }
                        case "PlayGame":
                            {
                                return "������ ����";
                            }
                        default: return "������ ��������!";
                    }  
                }
            case "ENG":
                {
                    switch (text)
                    {
                        case "Balance":
                            {
                                return "Balance";
                            }
                        case "NextLevel":
                            {
                                return "Next Level";
                            }
                        case "QuitGame":
                            {
                                return "Quit Game";
                            }
                        case "Replay":
                            {
                                return "Replay";
                            }
                        case "Resume":
                            {
                                return "Resume";
                            }
                        case "MainMenu":
                            {
                                return "Main Menu";
                            }
                        case "Settings":
                            {
                                return "Settings";
                            }
                        case "PlayGame":
                            {
                                return "Play Game";
                            }
                        default: return "Translition Error!";
                    }
                }
            default: return "ERROR!";
        }
    }
    public string TextSettingsButtons(string btnName, string variables)
    {
        switch (language)
        {
            case "RUS":
                {
                    switch (btnName)
                    {
                        case "btnDifficulty":
                            {
                                switch (variables)
                                {
                                    case "Random": return "�����: ������";
                                    case "Easy": return "�����: �������";
                                    case "Normal": return "�����: �������";
                                    case "Hard": return "�����: �������";
                                    default: return "������ ������ ������!";
                                }
                            }
                        case "btnGameMode":
                            {
                                switch (variables)
                                {
                                    case "True": return "��������� ������� �������";
                                    case "False": return "�������� ������� �������";
                                    default: return "������ ������ ������!";
                                }
                            }
                        default: return "������������ ������!";
                    }
                }
            case "ENG":
                {
                    switch (btnName)
                    {
                        case "btnDifficulty":
                            {
                                switch (variables)
                                {
                                    case "Random": return "Difficulty: Random";
                                    case "Easy": return "Difficulty: Easy";
                                    case "Normal": return "Difficulty: Normal";
                                    case "Hard": return "Difficulty: Hard";
                                    default: return "Difficulty state error!";
                                }
                            }
                        case "btnGameMode":
                            {
                                switch (variables)
                                {
                                    case "True": return "Disable GameShop ";
                                    case "False": return "Enable GameShop";
                                    default: return "GameShop state error!";
                                }
                            }
                        default: return "Uncorrect button error!";
                    }
                }
            default: return "";
        }
    }
    public string TextLevelButtons(int initialRange)
    {
        switch (language)
        {
            case "RUS":
                {
                    return $"������� {initialRange}";
                }
            case "ENG":
                {
                    return $"Level {initialRange}";
                }
            default: return "";
        }
    }
    public int LevelNumberConstructor(string btnTextLevel)
    {
        switch (language)
        {
            case "RUS":
                {
                    btnTextLevel = btnTextLevel.Trim(new char[] { '�', '�', '�', '�', '�', '�', '�', ' ' });
                    return Int32.TryParse(btnTextLevel, out int j) ? j : 0;
                }
            case "ENG":
                {
                    btnTextLevel = btnTextLevel.Trim(new char[] { 'L', 'e', 'v', 'l', ' ' });
                    return Int32.TryParse(btnTextLevel, out int j) ? j : 0;
                }
            default: return 0;
        }
    }
    public string GetPotionName(string name)
    {
        switch(language)
        {
            case "RUS":
                {
                    switch(name)
                    {
                        case "Power":
                            {
                                return "����� ����";
                            }
                        case "Speed":
                            {
                                return "����� ��������";
                            }
                        case "JumpForce":
                            {
                                return "����� �������";
                            }
                        case "HP":
                            {
                                return "������� ��������";
                            }
                        case "Defense":
                            {
                                return "������� ��������";
                            }
                        case "Immortal":
                            {
                                return "�������� ����������";
                            }
                        default: return "������ ��������!";
                    }
                    
                }
                
            case "ENG":
                switch (name)
                {
                    case "Power":
                        {
                            return "Potion of Power";
                        }
                    case "Speed":
                        {
                            return "Potion of Speed";
                        }
                    case "JumpForce":
                        {
                            return "Mixture of Lightness";
                        }
                    case "HP":
                        {
                            return "Elixir of Health";
                        }
                    case "Defense":
                        {
                            return "Elixir of Forteresses";
                        }
                    case "Immortal":
                        {
                            return "Elixir of Immortality";
                        }
                    default: return "Translation error!";
                }
            default: return "";
        }
    }
    public string GetPotionDescription(string name)
    {
        switch (language)
        {
            case "RUS":
                {
                    switch (name)
                    {
                        case "Power":
                            {
                                return "���� ����� +50% �� 10 ������";
                            }
                        case "Speed":
                            {
                                return "�������� ���� +25% �� 10 ������";
                            }
                        case "JumpForce":
                            {
                                return "���� ������ +25% �� 10 ������";
                            }
                        case "HP":
                            {
                                return "��������������� 33.3% ��������";
                            }
                        case "Defense":
                            {
                                return "+30 ��. ������";
                            }
                        case "Immortal":
                            {
                                return "������������ �� 10 ������";
                            }
                        default: return "������ ��������!";
                    }

                }

            case "ENG":
                switch (name)
                {
                    case "Power":
                        {
                            return "Attack power +50% for 10 seconds";
                        }
                    case "Speed":
                        {
                            return "Run speed +25% for 10 seconds";
                        }
                    case "JumpForce":
                        {
                            return "Jump force +25% for 10 seconds";
                        }
                    case "HP":
                        {
                            return "+33.3% HP of the maximum amount";
                        }
                    case "Defense":
                        {
                            return "+30 armor points";
                        }
                    case "Immortal":
                        {
                            return "Invulnerability for 10 seconds";
                        }
                    default: return "Translation error!";
                }
            default: return "";
        }
    }
}
