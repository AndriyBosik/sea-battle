/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/17/2020
 * Time: 01:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System.Windows.Input;
using GameObjects;

using Config;

using System;
using System.Windows;
using System.Windows.Controls;

namespace FieldEditorParts
{
	/// <summary>
	/// Description of OrientationGroup.
	/// </summary>
	public class OrientationGroup: StackPanel
	{
		private const string ORIENTATION_GROUP = "Orientation";
		
		public OrientationGroup(RoutedEventHandler handler)
		{
			Init();
			
			RadioButton rbHorizontal = new RadioButton()
			{
				Content = Gameplay.HORIZONTAL_ORIENTATION,
				GroupName = ORIENTATION_GROUP,
				IsChecked = true
			};
			rbHorizontal.Checked += handler;
			
			RadioButton rbVertical = new RadioButton()
			{
				Content = Gameplay.VERTICAL_ORIENTATION,
				GroupName = ORIENTATION_GROUP,
			};
			rbVertical.Checked += handler;
			
			this.Children.Add(rbHorizontal);
			this.Children.Add(rbVertical);
		}
		
		private void Init()
		{
			this.Orientation = Orientation.Vertical;
			//this.Margin = new Thickness(0, 10, 10, 0);
		}
	}
}
