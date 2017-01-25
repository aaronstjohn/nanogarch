using Entitas;
using Entitas.CodeGenerator;
using System.Collections.Generic;
using System;

[Serializable]
public class Order
{
	public readonly CommandType type;
	public readonly int unitId;
	public Order(CommandType type,int unitId)
	{
		this.type = type;
		this.unitId = unitId;
	}
	public string ToString()
	{
		return string.Format("{0}=>Unit[{1}]",type,unitId);
	}
}
[Serializable]
public class MoveOrder: Order
{
	public readonly int srcCell;
	public readonly int dstCell;
	public MoveOrder(int unitId,int src,int dst):base(CommandType.Move,unitId)
	{
		srcCell = src;
		dstCell = dst;
	}
	public string ToString()
	{
		return base.ToString()+string.Format(" cell [{0}] -> [{1}]",srcCell,dstCell);
	}
}

[Core]
public class OrdersComponent: IComponent
{
	public Order order;
}
[Core]
public class ExecutingOrdersComponent:IComponent
{

}
// [Core,SingleEntity]
// public sealed class OrdersComponent: IComponent 
// {
// 	public int turn;
// 	List<Order> _orders;
// 	public OrdersComponent()
// 	{
// 		_orders = new List<Order>();
// 	}
// 	public void AddOrder(Order o)
// 	{
// 		_orders.Add(o);
// 	}
// }