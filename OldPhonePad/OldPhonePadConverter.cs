using System;
using System.Text;

namespace OldPhonePad
{
    public static class OldPhonePadConverter
    {
        // Mapping of numbers to their corresponding letters
        private static readonly string[] keypadMapping = new string[]
        {
            "",     // 0
            "",     // 1
            "ABC",  // 2
            "DEF",  // 3
            "GHI",  // 4
            "JKL",  // 5
            "MNO",  // 6
            "PQRS", // 7
            "TUV",  // 8
            "WXYZ"  // 9
        };

        public static string OldPhonePad(string input)
        {
            if (string.IsNullOrEmpty(input) || !input.EndsWith("#"))
                throw new ArgumentException("Input must end with '#'");

            StringBuilder result = new StringBuilder();
            char currentDigit = '\0';
            int pressCount = 0;
            int lastPressTime = 0;

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                // Handle backspace
                if (c == '*')
                {
                    if (result.Length > 0)
                    {
                        result.Length--;
                    }
                    currentDigit = '\0';
                    pressCount = 0;
                    continue;
                }

                // Handle space (pause)
                if (c == ' ')
                {
                    if (currentDigit != '\0')
                    {
                        result.Append(GetCharacter(currentDigit, pressCount));
                        currentDigit = '\0';
                        pressCount = 0;
                    }
                    continue;
                }

                // Handle end of input
                if (c == '#')
                {
                    if (currentDigit != '\0')
                    {
                        result.Append(GetCharacter(currentDigit, pressCount));
                    }
                    break;
                }

                // Handle digit press
                if (char.IsDigit(c))
                {
                    int digit = c - '0';
                    if (digit < 2 || digit > 9)
                        continue;

                    if (c == currentDigit)
                    {
                        pressCount++;
                    }
                    else
                    {
                        if (currentDigit != '\0')
                        {
                            result.Append(GetCharacter(currentDigit, pressCount));
                        }
                        currentDigit = c;
                        pressCount = 1;
                    }
                }
            }

            return result.ToString();
        }

        private static char GetCharacter(char digit, int pressCount)
        {
            int digitIndex = digit - '0';
            if (digitIndex < 2 || digitIndex > 9)
                return '\0';

            string letters = keypadMapping[digitIndex];
            int letterIndex = (pressCount - 1) % letters.Length;
            return letters[letterIndex];
        }
    }
} 