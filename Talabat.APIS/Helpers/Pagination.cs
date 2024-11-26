using Talabat.APIS.DTOs;

namespace Talabat.APIS.Helpers
{
	public class Pagination<T>
	{

		public Pagination(int pageIndex, int pageSize, IReadOnlyList<T> data)
		{
			PageIndex = pageIndex;
			PageSize = pageSize;
			Data = data;
		}

		public int PageSize { get; set; }
		public int PageIndex { get; set; }
		public int Count { get; set; }
		public IReadOnlyList<T> Data { get; set; }
	}
}