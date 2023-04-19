using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Metadata;

namespace RomanToInteger;

internal static class Program
{
    #region Static Fields

    static string[] InvalidCharacters = { "a", "b", "e", "f", "g", "h", "j", "k", "n", "o", "p", "q", "r", "s", "t", "u", "w", "x", "y", "z" };
    static string[] ValidCharacters = { "CM", "M", "CD", "D", "XC", "C", "XL", "L", "IX", "X", "IV", "V", "I" };
    static readonly Dictionary<char, int> Cipher = new()
    {
          {'I', 1}
        , {'V', 5}
        , {'X', 10}
        , {'L', 50}
        , {'C', 100}
        , {'D', 500}
        , {'M', 1000}
    };
    static char CurrentCharacter = ' ';
    static char PreviousCharacter = ' ';
    const string TestValue1 = "III";
    const string TestValue2 = "LVIII";
    const string TestValue3 = "MCMXCIV";
    const int MINSTRINGLENGTH = 1;
    const int MAXSTRINGLENGTH = 15;
    static int sum = 0;

    #endregion

    static void Main(string[] args)
    {
        System.Console.WriteLine(RomanToInt(TestValue1));
        System.Console.WriteLine(RomanToInt(TestValue2));
        System.Console.WriteLine(RomanToInt(TestValue3));
    }

    static int RomanToInt(string input)
    {
        ValidateInput(input);
        ParseInput(input);
        return sum;
    }

    private static void ParseInput(string input)
    {
        int currentCharacterValue   = 0;
        int previousCharacterValue  = 0;

        foreach (char token in input.ToArray())
        {
            UpdateCharacters(token);

            UpdateCharacterValues(out currentCharacterValue, out previousCharacterValue);

            EvaluateSumValue(currentCharacterValue, previousCharacterValue);

            PreviousCharacter = token;
        }
    }

    #region Helper Methods

    private static void EvaluateSumValue(int currentCharacterValue, int previousCharacterValue)
    {
        if (currentCharacterValue > previousCharacterValue)
        {
            ConfirmCharacterValue(currentCharacterValue, previousCharacterValue);
        }
        else
        {
            sum += currentCharacterValue;
        }
    }
    private static void ConfirmCharacterValue(int currentCharacterValue, int previousCharacterValue)
    {
        sum -= previousCharacterValue;
        sum = sum - previousCharacterValue + currentCharacterValue;
    }
    private static void UpdateCharacterValues(out int currentCharacterValue, out int previousCharacterValue)
    {
        currentCharacterValue = Cipher[CurrentCharacter];
        previousCharacterValue = Cipher[PreviousCharacter];
    }
    private static void UpdateCharacters(char token)
    {
        CheckIfPreviousCharacterIsNull(token);

        CurrentCharacter = token;
    }
    private static void CheckIfPreviousCharacterIsNull(char token)
    {
        if (char.IsWhiteSpace(PreviousCharacter))
        {
            PreviousCharacter = token;
        }
    }

    #endregion Helper Methods

    #region Validation

    private static void ValidateInput(string input)
    {
        bool validInput = validInput = HasValidLength(value: input) && HasValidCharacters(value: input);


        if (!validInput)
        {
            throw new ArgumentException($"Input [{input}] is not a valid roman numeral.", nameof(input));
        }
    }
    private  static bool HasValidCharacters(string value)
    {
        foreach (string token in value.Split(new string[] { "" }, StringSplitOptions.None))
        {
            if (InvalidCharacters.Contains(token))
            {
                return false;
            }
        }
        return true;
    }
    private static bool HasValidLength(string value) => value.Length <= MAXSTRINGLENGTH && value.Length >= MINSTRINGLENGTH;

    #endregion Validation

}
