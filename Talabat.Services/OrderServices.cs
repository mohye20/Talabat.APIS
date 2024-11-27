using Talabat.Core.Entites;
using Talabat.Core.Entites.Order_Aggregate;
using Talabat.Core.Repositories;
using Talabat.Core.Services;

namespace Talabat.Services
{
	public class OrderServices : IOrderSevice
	{
		private readonly IBasketRepository _basketRepository;
		private readonly IGenaricRepository<Product> _productRepo;
		private readonly IGenaricRepository<DeliveryMethod> _deliverMethod;
		private readonly IGenaricRepository<Order> _orderRepo;

		public OrderServices(IBasketRepository basketRepository,
			IGenaricRepository<Product> productRepo,
			IGenaricRepository<DeliveryMethod> deliverMethod,
			IGenaricRepository<Order> orderRepo)
		{
			_basketRepository = basketRepository;
			_productRepo = productRepo;
			_deliverMethod = deliverMethod;
			_orderRepo = orderRepo;
		}

		public async Task<Order> CreateOrderAsync(
			string BuyerEmail,
			string BasketId,
			int DeliveryMethodId,
			Address ShippingAddress
			)
		{
			//1.Get Basket From Basket Repo

			var Basket = await _basketRepository.GetBasketAsync(BasketId);

			//2.Get Selected Item at Basket From Product Repo

			var OrderItems = new List<OrderItem>();
			if (Basket?.Items.Count > 0)
			{
				foreach (var item in Basket.Items)
				{
					var Product = await _productRepo.GetByIdAsync(item.Id);
					var ProductItemOrder = new ProdcutItemOrdered(Product.Id, Product.Name, Product.PictureUrl);
					var OrderItem = new OrderItem(ProductItemOrder, item.Quantity, Product.Price);
					OrderItems.Add(OrderItem);
				}
			}

			//3.Calculate Subtotal

			var SubTotal = OrderItems.Sum(item => item.Price * item.Quintity);

			//4.Get Delivery Method From DeliveryMethod Repo

			var DeliverMethod = await _deliverMethod.GetByIdAsync(DeliveryMethodId);

			//5.Create Order

			var Order = new Order(BuyerEmail, ShippingAddress, DeliverMethod, OrderItems, SubTotal);
			//6.Add Order Locally
			await _orderRepo.Add(Order);
			//7.Save Order To Database[ToDo]
			return Order;

		}

		public Task<Order> GetOrderByIdForSpecificUserAsync(string BuyerEmail, int OrderId)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(string BuyerEmail)
		{
			throw new NotImplementedException();
		}
	}
}