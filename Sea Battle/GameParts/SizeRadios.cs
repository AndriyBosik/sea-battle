/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/17/2020
 * Time: 00:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;

namespace GameParts
{
	/// <summary>
	/// Description of SizeRadios.
	/// </summary>
	public class SizeRadios: StackPanel
	{
		private const string GROUP_NAME = "Size";
		
		private Field field;
		
		public SizeRadios(int count, Field field)
		{
			Init();
			
			this.field = field;
			for (int i = 1; i <= count; i++)
			{
				RadioButton rb = new RadioButton()
				{
					Content = i.ToString(),
					GroupName = GROUP_NAME,
					IsChecked = i == 1
				};
				rb.Margin = new Thickness(10, 0, 0, 0);
				rb.Checked += ChangeSize;
				this.Children.Add(rb);
			}
		}
		
		private void Init()
		{
			this.Orientation = Orientation.Horizontal;
			this.Margin = new Thickness(0, 0, 0, 10);
		}
		
		private void ChangeSize(object obj, RoutedEventArgs e)
		{
			RadioButton sender = (RadioButton)obj;
			string sSize = (string)sender.Content;
			field.Size = Int32.Parse(sSize);
		}
	}
}
