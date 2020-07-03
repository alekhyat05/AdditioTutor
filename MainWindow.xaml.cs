using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Addition_Tutor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Random OPERAND = new Random();

		int OPERAND_MIN = 100;
		int OPERAND_MAX = 501;

		int MULTIPLICAND_MIN = 1;
		int MULTIPLICAND_MAX = 10;

		int DIVIDOR_MIN = 1;
		int REMAINDER = 0;

		string ADDITION = "+";
		string SUBSTRACTION = "−";
		string MULTIPLICATION = "×";
		string DIVISION = "÷";
		string EQUAL = "=";
		string NOT_EQUAL = "≠";

		int CORRECT_ANSWER;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void OperatorClick(object sender, RoutedEventArgs e)
		{
			Button clickedBtn = sender as Button;

			AnswerText.Text = "";

			int operandFirst = OPERAND.Next(OPERAND_MIN, OPERAND_MAX);
			OperandFirstText.Text = operandFirst.ToString();

			TryAgain.IsEnabled = true;
			CheckAnswer.IsEnabled = true;

			if (clickedBtn == Addition)
			{
				OperandSymbolText.Text = ADDITION;
				OperandSecondText.Text = OPERAND.Next(OPERAND_MIN, OPERAND_MAX).ToString();
			}

			else if (clickedBtn == Subtraction)
			{
				OperandSymbolText.Text = SUBSTRACTION;
				OperandSecondText.Text = OPERAND.Next(OPERAND_MIN, operandFirst).ToString();
			}

			else if (clickedBtn == Multiplication)
			{
				OperandSymbolText.Text = MULTIPLICATION;
				OperandSecondText.Text = OPERAND.Next(MULTIPLICAND_MIN, MULTIPLICAND_MAX).ToString();
			}

			else
			{
				int operandSecond = OPERAND.Next(DIVIDOR_MIN, operandFirst);

				while (operandFirst % operandSecond != REMAINDER)
				{
					operandSecond = OPERAND.Next(DIVIDOR_MIN, operandFirst);
				}

				OperandSymbolText.Text = DIVISION;
				OperandSecondText.Text = operandSecond.ToString();
			}

			EqualSymbolText.Text = EQUAL;
		}

		private void CheckAnswerClick(object sender, RoutedEventArgs e)
		{
			int firstNumber = int.Parse(OperandFirstText.Text);
			int secondNumber = int.Parse(OperandSecondText.Text);

			try
			{
				int answerInput = int.Parse(AnswerText.Text);

				if (OperandSymbolText.Text == ADDITION)
				{
					CORRECT_ANSWER = firstNumber + secondNumber;
				}

				else if (OperandSymbolText.Text == SUBSTRACTION)
				{
					CORRECT_ANSWER = firstNumber - secondNumber;
				}

				else if (OperandSymbolText.Text == MULTIPLICATION)
				{
					CORRECT_ANSWER = firstNumber * secondNumber;
				}

				else if (OperandSymbolText.Text == DIVISION)
				{
					CORRECT_ANSWER = firstNumber / secondNumber;
				}

				EqualSymbolText.Text = (CORRECT_ANSWER == answerInput ? EQUAL : NOT_EQUAL);
				MessageBox.Show($"Your answer is {(CORRECT_ANSWER == answerInput ? "correct" : "not correct")}");
			}

			catch
			{
				MessageBox.Show("Please enter answer");
			}

			string fileContent = $"Question: {OperandFirstText.Text + " " + OperandSymbolText.Text + " " + OperandSecondText.Text}"
				+ Environment.NewLine + $"Your anwser: { AnswerText.Text}" +
				Environment.NewLine + $"Correct answer: {CORRECT_ANSWER.ToString()}" + Environment.NewLine;
			fileContent += "\n";

			File.AppendAllText("MathTutor.txt", fileContent);
		}

		private void TryAgainClick(object sender, RoutedEventArgs e)
		{
			AnswerText.Text = "";
			EqualSymbolText.Text = EQUAL;
		}

		private void ResetClick(object sender, RoutedEventArgs e)
		{
			OperandFirstText.Text = "";
			OperandSecondText.Text = "";
			OperandSymbolText.Text = "";
			EqualSymbolText.Text = "";
			AnswerText.Text = "";

			TryAgain.IsEnabled = false;
			CheckAnswer.IsEnabled = false;
		}

		private void AnswerTextPreviewKeyDown(object sender, KeyEventArgs e)
		{
			if ((e.Key < Key.D0 || e.Key > Key.D9) &&
			(e.Key < Key.NumPad0 || e.Key > Key.NumPad9) &&
			e.Key != Key.Back && e.Key != Key.Delete &&
			e.Key != Key.Left && e.Key != Key.Right)
			{
				e.Handled = true;
			}
		}
	}
}