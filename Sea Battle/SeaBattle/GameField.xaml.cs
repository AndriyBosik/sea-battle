/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/20/2020
 * Time: 15:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using GameParts;

using GameObjects;

using Config;

using System;
using System.Windows;
using System.Windows.Controls;


namespace SeaBattle
{
	/// <summary>
	/// Interaction logic for Game.xaml
	/// </summary>
	public partial class GameField: Window
	{
		private Field firstPlayerField;
		private Field secondPlayerField;
		
		public GameField(Field firstPlayerField, Field secondPlayerField)
		{
			InitializeComponent();
			
			this.SizeToContent = SizeToContent.WidthAndHeight;
			this.ResizeMode = ResizeMode.NoResize;

			spContent.Margin = new Thickness(10, 10, 10, 10);
			
			this.firstPlayerField = firstPlayerField;
			this.secondPlayerField = secondPlayerField;
			
			firstPlayerField.Name = Gameplay.FIRST_PLAYER_FIELD;
			spContent.Children.Add(firstPlayerField);
			
			spContent.Children.Add(GetSeparator());
			
			secondPlayerField.Name = Gameplay.SECOND_PLAYER_FIELD;
			spContent.Children.Add(secondPlayerField);
			
			spContent.MouseLeftButtonUp += ProcessMove;
		}
		
		private Label GetSeparator()
		{
			Label l = new Label();
			l.FontSize = 30;
			l.Content = "VS";
			l.VerticalAlignment = VerticalAlignment.Center;
			l.HorizontalAlignment = HorizontalAlignment.Center;
			return l;
		}
		
		public void ProcessMove(object sender, RoutedEventArgs e)
		{
			Field field = GetField(e);
			
			Title = field.Name;
		}
		
		private Field GetField(RoutedEventArgs e)
		{
			if (!(e.Source is Label))
			{
				return null;
			}
			Label cell = (Label)e.Source;
			if (!(cell.Parent is Field))
			{
				return null;
			}
			var field = (Field)cell.Parent;
			return field;
		}
	}
}