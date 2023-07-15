using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public abstract class RequestParameters
    {
		const int maxPagesize = 50;
        public int PageNumber { get; set; }
		private int _pageSize;

		public int PageSize
		{
			get { return _pageSize; }
			set { _pageSize = Math.Min(value, maxPagesize); }
		}

        public string? OrderBy { get; set; }

        public string Fields { get; set; }
    }
}
