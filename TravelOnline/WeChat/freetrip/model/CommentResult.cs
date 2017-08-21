using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOnline.WeChat.freetrip.model
{
    public class CommentResult
    {
        public List<Comment> commentList { get; set; }
        public int pageCount { set; get; }
        public int currpage { set; get; }
    }
}