/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/07/2020
 * Time: 18:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Collections.Generic;
using System.Windows.Controls;
using Config;
using Database;
using GameObjects;
using Models;
using Processors;

using System;
using System.Windows;
using System.Windows.Media;

namespace SeaBattle
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class StartMenu: Window
	{
		private const string FIRST = "FIRST";
		private const string SECOND = "SEDOND";
		
		private int rows;
		private int columns;
		
		private static User firstUser;
		private static User secondUser;
		
		public StartMenu()
		{
			InitializeComponent();
			
			ImageBrush back = ImageProcessor.GetBackground("main-menu-background");
			back.Opacity = 0.3;
			
			if (firstUser == null)
				AttachLoginForm(spFirstUser, FIRST);
			else
				RefreshWindow(FIRST);
			if (secondUser == null)
				AttachLoginForm(spSecondUser, SECOND);
			else
				RefreshWindow(SECOND);
			
			mainPanel.Background = back;
		}
		
		private void AttachLoginForm(StackPanel sp, string user)
		{
			if (user.Equals(FIRST))
				firstUser = null;
			else
				secondUser = null;
			sp.Children.Clear();
			foreach (var elem in Login(user))
			{
				sp.Children.Add(elem);
			}
		}
		
		private User TryGetFromDatabase(string username, string password)
		{
			var userDAO = new UserDAO();
			return userDAO.SelectFirstByQuery("strcomp(Username,'" + username + "',0)=0 and " +
			                                  "strcomp(Password,'" + password + "',0)=0");
		}
		
		private void RefreshButtons()
		{
			if (firstUser != null && secondUser != null)
				bPlayOnline.IsEnabled = true;
			else
				bPlayOnline.IsEnabled = false;
		}
		
		private void RefreshWindow(string user)
		{
			RefreshButtons();
			var sp = user.Equals(FIRST) ? spFirstUser : spSecondUser;
			sp.Children.Clear();
			foreach (var elem in AuthorizedUserPanel(user))
			{
				sp.Children.Add(elem);
			}
		}
		
		private void ExitApplication(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
		
		private void StartOfflineGame(object sender, RoutedEventArgs r)
		{
			StartGame(GameMode.OFFLINE);
		}
		
		private void StartOnlineGame(object sender, EventArgs e)
		{
			StartGame(GameMode.ONLINE);
		}
		
		private void StartGame(GameMode gameMode)
		{
			FieldSizeWindow fieldSizeWindow = new FieldSizeWindow();
			if (fieldSizeWindow.ShowDialog() == true)
			{
				rows = fieldSizeWindow.Rows;
				columns = fieldSizeWindow.Columns;
				
				FieldEditor fieldEditor;
				if (gameMode == GameMode.OFFLINE)
					fieldEditor = new FieldEditor(rows, columns, gameMode);
				else
					fieldEditor = new FieldEditor(rows, columns, gameMode, firstUser, secondUser);
				fieldEditor.Show();
				this.Close();
			}
		}
		
		private void ShowAbout(object sender, RoutedEventArgs e)
		{
			About about = new About();
			about.Show();
		}
		
		private void ShowRegisterWindow(object sender, EventArgs e)
		{
			var register = new Register();
			if (register.ShowDialog().Value)
			{
				var userDAO = new UserDAO();
				userDAO.InsertData(register.User);
			}
		}
		
		private List<UIElement> Login(string userString)
		{
			var tblUserNicknameHint = new TextBlock();
			tblUserNicknameHint.Text = "Type your nickname:";
			tblUserNicknameHint.Margin = new Thickness(0,0,0,10);
			tblUserNicknameHint.FontSize = 17;
			
			var tbUserNickname = new TextBox();
			tbUserNickname.Margin = new Thickness(0,0,0,10);
			tbUserNickname.FontSize = 17;
			
			var tblUserPasswordHint = new TextBlock();
			tblUserPasswordHint.Text = "Type your password:";
			tblUserPasswordHint.Margin = new Thickness(0,0,0,10);
			tblUserPasswordHint.FontSize = 17;
			
			var pbUserPassword = new PasswordBox();
			pbUserPassword.Margin = new Thickness(0,0,0,10);
			pbUserPassword.FontSize = 17;
			
			var bLogin = new Button();
			bLogin.Content = "Login";
			bLogin.FontSize = 17;
			bLogin.PreviewMouseLeftButtonUp += (sender, e) => {
				var username = tbUserNickname.Text;
				var password = pbUserPassword.Password;
				var user = TryGetFromDatabase(username, password);
				if (user != null)
				{
					if (userString.Equals(FIRST))
					{
						if (!user.Equals(secondUser))
						{
							firstUser = user;
							RefreshWindow(FIRST);
						}
						else
						{
							MessageBox.Show("This user is currently logged in!", "Error");
						}
					}
					else
					{
						if (!user.Equals(firstUser))
						{
							secondUser = user;
							RefreshWindow(SECOND);
						}
						else
						{
							MessageBox.Show("This user is currently logged in!", "Error");
						}
					}
				}
				else
				{
					MessageBox.Show("User not found!", "Error");
				}
			};
			return new List<UIElement>
			{
				tblUserNicknameHint,
				tbUserNickname,
				tblUserPasswordHint,
				pbUserPassword,
				bLogin
			};
		}
		
		private List<UIElement> AuthorizedUserPanel(string userString)
		{
			var user = userString.Equals(FIRST) ? firstUser : secondUser;
			var tbInfo = new TextBlock();
			tbInfo.Text = "Hello, " + user.Username;
			tbInfo.Padding = new Thickness(20,0,20,0);
			tbInfo.Margin = new Thickness(0,0,0,10);
			tbInfo.FontSize = 17;
			
			var bShowStats = new Button();
			bShowStats.Content = "My games";
			bShowStats.FontSize = 17;
			bShowStats.Padding = new Thickness(20,0,20,0);
			bShowStats.Margin = new Thickness(0,0,0,10);
			bShowStats.PreviewMouseLeftButtonUp += (sender, e) => {
				var stats = new Statistics(user);
				stats.ShowDialog();
			};
			
			var bSettings = new Button();
			bSettings.Content = "Settings";
			bSettings.FontSize = 17;
			bSettings.Padding = new Thickness(20,0,20,0);
			bSettings.Margin = new Thickness(0,0,0,10);
			bSettings.PreviewMouseLeftButtonUp += (sender, e) => {
				var settings = new Settings(user);
				if (settings.ShowDialog().Value)
					AttachLoginFormByUser(userString);
			};
			
			var bRemoveAccount = new Button();
			bRemoveAccount.Content = "Remove My Account";
			bRemoveAccount.FontSize = 17;
			bRemoveAccount.Padding = new Thickness(20,0,20,0);
			bRemoveAccount.Margin = new Thickness(0,0,0,10);
			bRemoveAccount.PreviewMouseLeftButtonUp += (sender, e) => {
				new UserDAO().DeleteById(user.ID);
				AttachLoginFormByUser(userString);
			};
			
			var bLogout = new Button();
			bLogout.Content = "Logout";
			bLogout.FontSize = 17;
			bLogout.Padding = new Thickness(20,0,20,0);
			bLogout.Margin = new Thickness(0,0,0,10);
			bLogout.PreviewMouseLeftButtonUp += (sender, e) => AttachLoginFormByUser(userString);
			
			return new List<UIElement>
			{
				tbInfo,
				bShowStats,
				bSettings,
				bRemoveAccount,
				bLogout
			};
		}
		
		private void AttachLoginFormByUser(string userString)
		{
			if (userString.Equals(FIRST))
				AttachLoginForm(spFirstUser, userString);
			else
				AttachLoginForm(spSecondUser, userString);
			RefreshButtons();
		}
		
	}
}