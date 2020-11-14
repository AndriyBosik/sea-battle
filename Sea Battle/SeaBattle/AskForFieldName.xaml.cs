/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11/14/2020
 * Time: 21:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;

namespace SeaBattle
{
	/// <summary>
	/// Interaction logic for AskForFieldName.xaml
	/// </summary>
	public partial class AskForFieldName : Window
	{
		public AskForFieldName()
		{
			InitializeComponent();
		}
		
		private void OK(object sender, EventArgs e)
		{
			DialogResult = true;
			Close();
		}
		
		private void Cancel(object sender, EventArgs e)
		{
			DialogResult = false;
			Close();
		}
	}
}