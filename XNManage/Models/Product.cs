// ***********************************************************************
// Assembly         : XNManage
// Author           : stoneniqiu
// Created          : 06-12-2014
//
// Last Modified By : Administrator
// Last Modified On : 06-12-2014
// ***********************************************************************
// <copyright file="Product.cs" company="China">
//     Copyright (c) China. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;
using XNManage.Common;

namespace XNManage.Models
{
    /// <summary>
    /// Class Product
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// 直接做型号处理
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the brand.
        /// </summary>
        /// <value>The brand.</value>
        public string Brand { get; set; }

        public string Size { get; set; }

        /// <summary>
        /// Gets or sets the 规格.
        /// </summary>
        /// <value>The standard.</value>
        public string Standard { get; set; }

        /// <summary>
        /// 专柜
        /// </summary>
        public decimal ZhuanGui { get; set; }

        /// <summary>
        /// Gets or sets the 莎莎 price.
        /// </summary>
        /// <value>The sa sa price.</value>
        public decimal SaSaPrice { get; set; }

        /// <summary>
        /// Gets or sets the 卓越 price.
        /// </summary>
        /// <value>The zhuoyue price.</value>
        public decimal ZhuoyuePrice { get; set; }
        /// <summary>
        /// Gets or sets the 卡莱美 price.
        /// </summary>
        /// <value>The ka lai price.</value>
        public decimal KaLaiPrice { get; set; }

        /// <summary>
        /// 雅施
        /// </summary>
        public decimal YaShi { get; set; }

        /// <summary>
        /// Gets or sets the 万宁 price.
        /// </summary>
        /// <value>The wanning price.</value>
        public decimal WanningPrice { get; set; }
        /// <summary>
        /// Gets or sets the 屈臣氏 price.
        /// </summary>
        /// <value>The quchengshi price.</value>
        public decimal QuchengshiPrice { get; set; }
        /// <summary>
        /// Gets or sets the 药店 price.
        /// </summary>
        /// <value>The drugstore price.</value>
        public decimal DrugstorePrice { get; set; }

        /// <summary>
        /// Gets or sets the others.
        /// </summary>
        /// <value>The others.</value>
        public string Others { get; set; }

        /// <summary>
        /// Gets or sets the create time.
        /// </summary>
        /// <value>The create time.</value>
        public DateTime CreateTime { get; set; }

        public string ImgUrl { get; set; }

        public Product()
        {
            CreateTime = DateTime.Now;
        }
    }

    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }

    public class ChartData
    {
        public string label { get; set; }
        public double data { get; set; }
    }

    public enum WhereOperation
    {
        [StringValue("eq")]
        Equal,
        [StringValue("ne")]
        NotEqual,
        [StringValue("cn")]
        Contains
    }
}