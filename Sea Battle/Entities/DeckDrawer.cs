/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11/05/2020
 * Time: 19:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Config;
using GameObjects;

namespace Entities
{
	/// <summary>
	/// Description of DeckDrawer.
	/// </summary>
	public class DeckDrawer: Drawer
	{
		private const int DEATH_LINE_PADDING = 3;
		private const int HEALTH_BAR_WIDTH = 3;
		private const int DEATH_LINE_WIDTH = 3;
		
		private DeckKind kind;
		private string orientation;
		
		public DeckDrawer(Deck fieldComponent, DeckKind kind, string orientation): base(fieldComponent)
		{
			this.kind = kind;
			this.orientation = orientation;
		}
		
		private UIElement DeathSign()
		{
			var line = new Line();
			line.Stroke = Brushes.Red;
			line.X1 = line.Y1 = DEATH_LINE_PADDING;
			line.X2 = line.Y2 = Gameplay.CELL_SIZE - DEATH_LINE_PADDING;
			line.StrokeThickness = DEATH_LINE_WIDTH;
			return line;
		}
		
		private UIElement HealthBar()
		{
			Line healthBar = new Line();
			healthBar.Stroke = GetBrushColor();
			healthBar.X1 = 0;
			healthBar.Y1 = 2;
			//healthBar.X2 = Gameplay.CELL_SIZE;
			var deck = fieldComponent as Deck;
			healthBar.X2 = Gameplay.CELL_SIZE*deck.PercentageHealthValue;
			healthBar.Y2 = 2;
			healthBar.StrokeThickness = HEALTH_BAR_WIDTH;
			return healthBar;
		}
		
		private Brush GetBrushColor()
		{
			var deck = (Deck)fieldComponent;
			var currentHealth = deck.CurrentHealth;
			var totalHealth = deck.TotalHealth;
			if (currentHealth <= totalHealth/3.0)
				return Brushes.Red;
			if (currentHealth <= totalHealth*2.0/3.0)
				return Brushes.Yellow;
			return Brushes.Green;
		}
		
		protected override string GetIcon()
		{
			return DeckKindProcessor.GetIcon(kind);
		}
		
		protected override CellStatus GetStatus()
		{
			return CellStatus.DECK;
		}
		
		protected override List<UIElement> GetChildren()
		{
			var children = new List<UIElement>();
			if ((fieldComponent as Deck).CurrentHealth == 0)
				children.Add(DeathSign());
			else
				children.Add(HealthBar());
			return children;
		}
		
		protected override void TransformImage()
		{
			if (orientation == Gameplay.VERTICAL_ORIENTATION)
				Image.LayoutTransform = new RotateTransform(90);
		}
	}
}
