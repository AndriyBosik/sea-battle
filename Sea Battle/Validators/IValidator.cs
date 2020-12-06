/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11/17/2020
 * Time: 22:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Models;

namespace Validators
{
	/// <summary>
	/// Description of IValidator.
	/// </summary>
	public interface IValidator<T> where T: Base<T>
	{
		bool IsValid(T model);
	}
}
