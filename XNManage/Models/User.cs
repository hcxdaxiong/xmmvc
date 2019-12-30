// ***********************************************************************
// Assembly         : XNManage
// Author           : stoneniqiu
// Created          : 06-12-2014
//
// Last Modified By : stoneniqiu
// Last Modified On : 06-12-2014
// ***********************************************************************
// <copyright file="User.cs" company="China">
//     Copyright (c) China. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace XNManage.Models
{
    /// <summary>
    /// Class User
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [Key]
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [MaxLength(30)]
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the pass word.
        /// </summary>
        /// <value>The pass word.</value>
        public string PassWord { get; set; }
        /// <summary>
        /// Gets or sets the create time.
        /// </summary>
        /// <value>The create time.</value>
        public DateTime CreateTime { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        public User()
        {
            CreateTime = DateTime.Now;
        }

    }
}