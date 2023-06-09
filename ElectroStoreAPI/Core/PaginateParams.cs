﻿namespace ElectroStoreAPI.Core
{
	public class PaginateParameters
	{
		const int maxPageSize = 50;
		public int pageNumber { get; set; } = 1;

		private int _pageSize = 15;
		public int pageSize
		{
			get
			{
				return _pageSize;
			}
			set
			{
				_pageSize = (value > maxPageSize) ? maxPageSize : value;
			}
		}
	}
}
