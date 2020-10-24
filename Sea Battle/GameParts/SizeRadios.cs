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
using System.Windows.Input;

namespace FieldEditorParts
{
	/// <summary>
	/// Description of SizeRadios.
	/// </summary>
	public class SizeRadios: StackPanel
	{
		private const string GROUP_NAME = "Size";
		
		private RadioButton[] rbs;
		private bool allPasted;
		
		public bool AllPasted
		{
			get
			{
				return allPasted;
			}
		}
		
		public SizeRadios(int count, RoutedEventHandler ChangeSize)
		{
			rbs = new RadioButton[count];
			Init();
			
			for (int i = 1; i <= count; i++)
			{
				int index = i - 1;
				rbs[index] = new RadioButton()
				{
					Content = i.ToString(),
					GroupName = GROUP_NAME,
					IsChecked = i == 1
				};
				rbs[index].Margin = new Thickness(10, 0, 0, 0);
				rbs[index].Checked += ChangeSize;
				this.Children.Add(rbs[index]);
			}
		}
		
		private void Init()
		{
			this.allPasted = false;
			this.Orientation = Orientation.Horizontal;
		}
		
		public void MakeDisabled(int number)
		{
			int index = number - 1;
			rbs[index].IsEnabled = false;
			for (int i = index + 1; i < rbs.Length; i++)
			{
				if (rbs[i].IsEnabled)
				{
					rbs[i].IsChecked = true;
					return;
				}
			}
			for (int i = 0; i < index; i++)
			{
				if (rbs[i].IsEnabled)
				{
					rbs[i].IsChecked = true;
					return;
				}
			}
			allPasted = true;
		}
	}
}
