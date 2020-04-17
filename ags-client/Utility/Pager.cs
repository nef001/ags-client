using System;
using System.Collections.Generic;
using System.Linq;

namespace ags_client.Utility
{
    public class Pager<T>
    {
        private readonly IEnumerable<T> _data;
        private readonly int _pageSize;
        private int _page = 0;
        private readonly int _count = 0;

        public Pager(IEnumerable<T> data, int pageSize)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize", "Non zero positive integer required.");

            _data = data;
            _pageSize = pageSize;
            _count = data.Count();
        }

        public IEnumerable<T> NextPage()
        {
            if ((_page * _pageSize) > _count - 1)
                return null;

            var result = _data.Skip(_page * _pageSize).Take(_pageSize);


            _page++;


            return result;
        }

        public void Reset()
        {
            _page = 0;
        }


    }
}
