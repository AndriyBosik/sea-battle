/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 03.10.2020
 * Time: 0:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Windows.Media;

using System.Windows.Shapes;
using Config;

using GameObjects;

namespace Entities
{
	/// <summary>
	/// Description of Deck.
	/// </summary>
	public class Deck: Cell
	{
		private Guid shipId;
		private Guid bombId;
		
		#region Properties
		public Ship Ship
		{
			get
			{
				return Database.ships.Where(ship => ship.ID == shipId).FirstOrDefault();
			}
			set
			{
				shipId = value.ID;
			}
		}
		
		public Bomb Bomb
		{
			get { return Database.bombs.Where(bomb => bomb.ID == bombId).FirstOrDefault(); }
			set { bombId = value.ID; }
		}
		
		public int CurrentHealth
		{
			get;
			private set;
		}
		
		public int TotalHealth
		{
			get;
			private set;
		}
		
		public int PrizeForDestroy
		{
			get;
			private set;
		}
		
		private DeckKind Kind
		{
			get;
			set;
		}
		
		public string Orientation
		{
			get;
			private set;
		}
		#endregion
		
		public Deck(Ship ship, DeckKind kind, string orientation):
			this(ship, kind, orientation, 100)
		{
			Database.decks.Add(this);
		}
		
		public Deck(
			Ship ship,
			DeckKind deckKind,
			string orientation,
			int health)
		{
			Ship = ship;
			
			CurrentHealth = TotalHealth = health;
			
			Database.decks.Add(this);
			Init(deckKind, orientation);
		}
		
		private void Init(DeckKind kind, string orientation)
		{
			Kind = kind;
			Orientation = orientation;
			this.icon = DeckKindProcessor.GetIcon(kind);
			
			base.Init(this.icon);
			
			status = CellStatus.DECK;
			RotateDeck(orientation);
			DrawHealthStatusBar();
		}
		
		private void RotateDeck(string orientation)
		{
			if (orientation == Gameplay.VERTICAL_ORIENTATION)
			{
				image.LayoutTransform = new RotateTransform(90);
			}
		}
		
		private void DrawHealthStatusBar()
		{
			Line line = new Line();
			line.Stroke = GetBrushColor();
			line.X1 = 0;
			line.Y1 = 0;
			line.X2 = Gameplay.CELL_SIZE*CurrentHealth*1.0/TotalHealth;
			line.Y2 = 0;
			line.StrokeThickness = 2;
			image.Children.Add(line);
		}
		
		private Brush GetBrushColor()
		{
			if (CurrentHealth <= TotalHealth/3.0)
				return Brushes.Red;
			if (CurrentHealth <= TotalHealth*2.0/3.0)
				return Brushes.Yellow;
			return Brushes.Green;
		}
		
		public void Refresh()
		{
			DrawHealthStatusBar();
		}
		
		public void Hurt(int damage)
		{
			CurrentHealth -= damage;
		}
		
		public void Heal(int health)
		{
			this.CurrentHealth += health;
		}
	}
}
