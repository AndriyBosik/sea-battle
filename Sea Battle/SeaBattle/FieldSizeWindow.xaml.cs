/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/07/2020
 * Time: 19:13
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
	/// Interaction logic for FieldSize.xaml
	/// </summary>
	public partial class FieldSizeWindow : Window
	{
		
		public int Rows
		{
			get { return Int32.Parse(tbRows.Text); }
		}
		
		public int Columns
		{
			get { return Int32.Parse(tbColumns.Text); }
		}
		
		public FieldSizeWindow()
		{
			InitializeComponent();
		}
		
		private void Accept(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
		}
		
	}
}