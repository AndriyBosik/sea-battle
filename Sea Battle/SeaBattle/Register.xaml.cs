/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11/17/2020
 * Time: 22:14
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
	/// Interaction logic for Register.xaml
	/// </summary>
	public partial class Register : Window
	{
		public User User
		{ get; private set; }
		
		public Register()
		{
			InitializeComponent();
		}
		
		private void OK(object sender, EventArgs e)
		{
			User = null;
			var username = tbUsername.Text;
			var password = pbPassword.Password;
			var confirmPassword = pbConfirmPassword.Password;
			if (new UserDAO().SelectFirstByQuery("Username='" + username + "'") != null)
			{
				MessageBox.Show("User with this username already exists!", "Error while registration");
				return;
			}
			if (!password.Equals(confirmPassword))
			{
				MessageBox.Show("Passwords are not equal!", "Incorrect data");
				return;
			}
			var user = new User(username, password);
			var validator = new UserValidator();
			if (!validator.IsValid(user))
			{
				MessageBox.Show("Your data is incorrect!", "Incorrect data");
				return;
			}
			User = user;
			DialogResult = true;
		}
		
		private void Cancel(object sender, EventArgs e)
		{
			DialogResult = false;
		}
	}
}