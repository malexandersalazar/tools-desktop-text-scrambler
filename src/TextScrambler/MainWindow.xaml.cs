using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Windows;

namespace TextScrambler
{
    public partial class MainWindow : Window
    {
        private const string _lowercase_letters = "abcdefghijklmnopqrstuvwxyz";
        private const string _capital_letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string _numbers = "0123456789";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ScramblerButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = SourceTextBox.Text;

            var outputTextLines = new List<string>();

            var inputTextLines = inputText.Split(Environment.NewLine);

            foreach (var textLine in inputTextLines)
            {
#if DEBUG
                Debug.WriteLine(textLine);
#endif
                string newTextLine = ConvertTextLine(textLine);
                outputTextLines.Add(newTextLine);

                Debug.WriteLine(newTextLine);
            }

            TargetTextBox.Text = string.Join(Environment.NewLine, outputTextLines);
        }

        private string ConvertTextLine(string textLine)
        {
            var newString = new List<char>();

            for (int i = 0; i < textLine.Length; i++)
            {
                var c = textLine[i];

                if (i < SkipLeftUpDown.Value)
                {
                    newString.Add(c);
                    continue;
                }

#if DEBUG
                Debug.WriteLine(c);
#endif
                if (_numbers.Contains(c))
                    newString.Add(_numbers[RandomNumberGenerator.GetInt32(_numbers.Length)]);
                else if (_lowercase_letters.Contains(c))
                    newString.Add(_lowercase_letters[RandomNumberGenerator.GetInt32(_lowercase_letters.Length)]);
                else if (_capital_letters.Contains(c))
                    newString.Add(_capital_letters[RandomNumberGenerator.GetInt32(_capital_letters.Length)]);
                else
                    newString.Add(c);
            }

            return new string(newString.ToArray());
        }
    }
}