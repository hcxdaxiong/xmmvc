using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XNManage.Repository
{
     public interface IPager
    {
        /// <summary>
        /// 每页项目数
        /// </summary>
        /// <value>The page item count.</value>
      int  PageItemCount { get; set; }
      /// <summary>
      /// 总页数
      /// </summary>
      /// <value>The total page.</value>
       int TotalPage { get; }
       /// <summary>
       /// 显示的页数
       /// </summary>
       /// <value>The display page.</value>
       int DisplayPage { get; set; }
       /// <summary>
       /// 满足条件的总数目
       /// </summary>
        int TotalItem { get; set; }
    }
}