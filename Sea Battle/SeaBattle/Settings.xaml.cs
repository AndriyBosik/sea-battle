/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11/18/2020
 * Time: 22:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using Database;
using Models;
using Validators;

namespace SeaBattle
{
	/// <summary>
	/// Interaction logic for Settings.xaml
	/// </summary>
	public partial class Settings : Window
	{
		private User user;
		
		public Settings(User user)
		{
			InitializeComponent();
			
			this.user = user;
			tbUsername.Text = user.Username;
		}
		
		private void ChangeUsername(object sender, EventArgs e)
		{
			var username = tbUsername.Text;
			if (new UserDAO().SelectFirstByQuery("Username='" + username + "'") != null)
			{
				MessageBox.Show("User with this username already exists!", "Error while Changing data");
				return;
			}
			var newUser = new User(username, user.Password);
			if (!new UserValidator().IsValid(newUser))
			{
				MessageBox.Show("Username must contain at least 5 symbols!", "Incorrect username!");
				return;
			}
			new UserDAO().UpdateById(user.ID, newUser);
			DialogResult = true;
		}
		
		private void ChangePassword(object sender, EventArgs e)
		{
			var password = pbPassword.Password;
			var confirmPassword = pbConfirmPassword.Password;
			
			if (!password.Equals(confirmPassword))
			{
				MessageBox.Show("Passwords are not equal!", "Incorrect data");
				return;
			}
			var newUser = new User(user.Username, password);
			var validator = new UserValidator();
			if (!validator.IsValid(newUser))
			{
				MessageBox.Show("Password must containg at least 5 symbols!", "Incorrect password");
				return;
			}
			new UserDAO().UpdateById(user.ID, newUser);
			DialogResult = true;
		}
		
		private void Cancel(object sender, EventArgs e)
		{
			DialogResult = false;
		}
		
	}
}