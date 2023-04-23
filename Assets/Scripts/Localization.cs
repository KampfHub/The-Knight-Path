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
                                return "Лишь камнем оглушив, Дракона можно победить! Найди камень и сбрось на голову ему!";
                            }
                        case "textGenie":
                            {
                                return "Не тронь меня! Иначе я обрушу на тебя магию камней!!!";
                            }
                        default: return "Ошибка перевода!";
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
                                return "Баланс";
                            }
                        case "NextLevel":
                            {
                                return "Следующий уровень";
                            }
                        case "QuitGame":
                            {
                                return "Выйти из игры";
                            }
                        case "Replay":
                            {
                                return "Повторить";
                            }
                        case "Resume":
                            {
                                return "Продолжить";
                            }
                        case "MainMenu":
                            {
                                return "Главное меню";
                            }
                        case "Settings":
                            {
                                return "Настройки";
                            }
                        case "PlayGame":
                            {
                                return "Начать игру";
                            }
                        default: return "Ошибка перевода!";
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
                                    case "Random": return "Режим: Рандом";
                                    case "Easy": return "Режим: Простой";
                                    case "Normal": return "Режим: Средний";
                                    case "Hard": return "Режим: Сложный";
                                    default: return "Ошибка выбора режима!";
                                }
                            }
                        case "btnGameMode":
                            {
                                switch (variables)
                                {
                                    case "True": return "Отключить Игровой магазин";
                                    case "False": return "Включить Игровой магазин";
                                    default: return "Ошибка выбора режима!";
                                }
                            }
                        default: return "Неккоректная кнопка!";
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
                    return $"Уровень {initialRange}";
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
                    btnTextLevel = btnTextLevel.Trim(new char[] { 'У', 'р', 'о', 'в', 'е', 'н', 'ь', ' ' });
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
                                return "Зелье силы";
                            }
                        case "Speed":
                            {
                                return "Зелье скорости";
                            }
                        case "JumpForce":
                            {
                                return "Зелье лёгкости";
                            }
                        case "HP":
                            {
                                return "Эликсир здоровья";
                            }
                        case "Defense":
                            {
                                return "Эликсир крепости";
                            }
                        case "Immortal":
                            {
                                return "Микстура бессмертия";
                            }
                        default: return "Ошибка перевода!";
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
                                return "Сила атаки +50% на 10 секунд";
                            }
                        case "Speed":
                            {
                                return "Скорость бега +25% на 10 секунд";
                            }
                        case "JumpForce":
                            {
                                return "Сила прыжка +25% на 10 секунд";
                            }
                        case "HP":
                            {
                                return "Восстанавливает 33.3% здоровья";
                            }
                        case "Defense":
                            {
                                return "+30 ед. защиты";
                            }
                        case "Immortal":
                            {
                                return "Неуязвимость на 10 секунд";
                            }
                        default: return "Ошибка перевода!";
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
