/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 14.11.2020
 * Time: 19:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Serializators
{
	/// <summary>
	/// Description of Serializator.
	/// </summary>
	public interface Serializator<T>
	{
		void Serialize(T obj);
		T Deserialize();
		void Remove();
	}
}
