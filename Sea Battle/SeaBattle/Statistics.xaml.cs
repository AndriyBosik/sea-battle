/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11/18/2020
 * Time: 22:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows;
using Database;
using Models;

namespace SeaBattle
{
	/// <summary>
	/// Interaction logic for Statistics.xaml
	/// </summary>
	public partial class Statistics : Window
	{
		private User user;
		
		public Statistics(User user)
		{
			InitializeComponent();
			
			this.user = user;
			var onlineGameDAO = new OnlineGameDAO();
			string query = "userId='" + user.ID + "' or opponentId='" + user.ID + "'";
			var games = onlineGameDAO.SelectAllByQuery(
				"strcomp(userId,'" + user.ID + "',0)=0 or strcomp(opponentId,'" + user.ID + "',0)=0");
			tblUserInformation.Text = "Hello, " + user.Username + ". Your games:";
			lbGames.ItemsSource = games;
		}
		
		public void OK(object sender, EventArgs e)
		{
			Close();
		}
	}
}