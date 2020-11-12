/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 12.11.2020
 * Time: 12:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;

namespace SeaBattle
{
	/// <summary>
	/// Interaction logic for GameLoader.xaml
	/// </summary>
	public partial class GameLoader : Window
	{
		public GameLoader()
		{
			InitializeComponent();
		}
		
		private void LoadGame(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
			this.Close();
		}
		
		private void CancelLoading(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			this.Close();
		}
	}
}