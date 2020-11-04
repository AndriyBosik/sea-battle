/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 03.11.2020
 * Time: 23:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace SeaBattle
{
	/// <summary>
	/// Interaction logic for CongratulateWindow.xaml
	/// </summary>
	public partial class CongratulateWindow : Window
	{
		public CongratulateWindow(string message)
		{
			InitializeComponent();
			lCongratulationText.Content = message;
			
			bMainMenu.PreviewMouseLeftButtonDown += (sender, e) => {
				DialogResult = true;
				Close();
			};
		}
	}
}