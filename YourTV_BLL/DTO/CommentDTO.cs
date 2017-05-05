﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTV_BLL.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string ApplicationUserId { get; set; }
        public int VideoId { get; set; }
        public virtual VideoDTO Video { get; set; }
    }
}
