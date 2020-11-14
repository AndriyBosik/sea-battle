/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 25.09.2020
 * Time: 16:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Config
{
	/// <summary>
	/// Description of SolutionConfig.
	/// </summary>
	public static class SolutionConfig
	{
		public static string PROJECT_DIRECTORY = Environment.CurrentDirectory + "/../../";
		public static string IMAGES_DIRECTORY = PROJECT_DIRECTORY + "Icons/";
		public static string FIELDS_DIRECTORY = PROJECT_DIRECTORY + "Fields/";
		public static string XML_FIELDS_LIST_FILENAME = FIELDS_DIRECTORY + "fields_list.xml";
	}
}
