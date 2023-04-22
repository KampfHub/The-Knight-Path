using System;
using UnityEngine;

public class Localization : MonoBehaviour
{
    private string language;
    void Start()
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
                            return "Mixture of Immortality";
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
                                return "����������� ���� ����� �� 25% �� 10 ������";
                            }
                        case "Speed":
                            {
                                return "����������� �������� ������������ �� 25% �� 10 ������";
                            }
                        case "JumpForce":
                            {
                                return "����������� ���� ������ �� 25% �� 10 ������";
                            }
                        case "HP":
                            {
                                return "��������������� 33.3% ��������";
                            }
                        case "Defense":
                            {
                                return "��������� 30 ��. ������";
                            }
                        case "Immortal":
                            {
                                return "���� ������������ �� 20 ������";
                            }
                        default: return "������ ��������!";
                    }

                }

            case "ENG":
                switch (name)
                {
                    case "Power":
                        {
                            return "Increases attack power by 25% for 10 seconds";
                        }
                    case "Speed":
                        {
                            return "Increases movement speed by 25% for 10 seconds";
                        }
                    case "JumpForce":
                        {
                            return "Increases jump force by 25% for 10 seconds";
                        }
                    case "HP":
                        {
                            return "Restores 33.3% of the maximum amount of health";
                        }
                    case "Defense":
                        {
                            return "Gives 30 armor points";
                        }
                    case "Immortal":
                        {
                            return "Gives invulnerability for 20 seconds";
                        }
                    default: return "Translation error!";
                }
            default: return "";
        }
    }
}
