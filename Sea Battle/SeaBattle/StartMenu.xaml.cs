﻿/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/07/2020
 * Time: 18:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
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
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class StartMenu : Window
	{
		
		private int rows;
		private int columns;
		
		public StartMenu()
		{
			InitializeComponent();
		}
		
		private void ExitApplication(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
		
		private void AskSize(object sender, RoutedEventArgs e)
		{
			FieldSizeWindow fieldSizeWindow = new FieldSizeWindow();
			if (fieldSizeWindow.ShowDialog() == true)
			{
				rows = fieldSizeWindow.Rows;
				columns = fieldSizeWindow.Columns;
				
				OpenFieldEditor();
			}
		}
		
		private void OpenFieldEditor()
		{
			FieldEditor fieldEditor = new FieldEditor(rows, columns);
			fieldEditor.Show();
			this.Close();
		}
		
	}
}