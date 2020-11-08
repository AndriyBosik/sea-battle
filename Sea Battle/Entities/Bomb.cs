/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 02.10.2020
 * Time: 22:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;

using Config;

using GameObjects;

namespace Entities
{
	/// <summary>
	/// Description of Bomb.
	/// </summary>
	public class Bomb: ShopBomb, IFieldComponent
	{
		private Point point;
		private Field field;
		
		public bool IsExposed
		{ get; private set; }
		
		public Bomb(Field field, Point point, int radius, int cost, int damage):
			base(radius, cost, damage)
		{
			this.point = point;
			this.field = field;
			this.IsExposed = false;
		}
		
		public void Explose(ref int money, ref int opponentMoney)
		{
			if (IsExposed)
				return;
			IsExposed = true;
			var core = new Core(Radius, CostByOne, Damage);
			core.Shot(
				field, point,
				new Bonus{Radius = 0, Damage = 0}, Direction.NO_DIRECTION,
				ref money, ref opponentMoney);
		}
		
		public void GetDamage(int damage, ref int money, ref int opponentMoney)
		{
			if (IsExposed)
				return;
			Explose(ref opponentMoney, ref money);
		}
		
		public override bool Equals(object obj)
		{
			Bomb other = obj as Bomb;
			if (other == null)
				return false;
			return  this.CostByOne == other.CostByOne &&
					this.Damage == other.Damage &&
					this.DamageKind == other.DamageKind &&
					this.Radius == other.Radius;
		}
	}
}
