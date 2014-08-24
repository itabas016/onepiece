using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace OnePiece.Framework.Core
{
    public static class MvcPagingExtensions
    {
        #region HtmlHelper extensions

        public static Pager Pager(this HtmlHelper htmlHelper, IPagedList models, AjaxOptions ajaxOptions = null)
        {
            var pageSize = models.PageSize;
            var currentPage = models.PageNumber;
            var totalItemCount = models.TotalItemCount;

            var pager = new Pager(htmlHelper, pageSize, currentPage, totalItemCount);

            if (ajaxOptions != null)
            {
                pager = pager.Options(o => o.AjaxOptions(ajaxOptions));
            }

            return pager;
        }

        #endregion

        #region IQueryable<T> extensions

        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int? pageIndex, int pageSize = 20, int? totalCount = null)
        {
            int currentPageIndex = pageIndex.HasValue ? pageIndex.Value - 1 : 0;

            return new PagedList<T>(source, currentPageIndex, pageSize, totalCount);
        }

        #endregion

        #region IEnumerable<T> extensions

        public static IPagedList<T> ToPagedList<T>(this IList<T> source, int? pageIndex, int pageSize = 20, int? totalCount = null)
        {
            int currentPageIndex = pageIndex.HasValue ? pageIndex.Value - 1 : 0;


            return new PagedList<T>(source, currentPageIndex, pageSize, totalCount);
        }

        #endregion
    }
}
