/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11/17/2020
 * Time: 22:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Models;

namespace Validators
{
	/// <summary>
	/// Description of UserValidator.
	/// </summary>
	public class UserValidator: IValidator<User>
	{
		public bool IsValid(User user)
		{
			return user.Password.Length > 5 && user.Username.Length > 5;
		}
	}
}
