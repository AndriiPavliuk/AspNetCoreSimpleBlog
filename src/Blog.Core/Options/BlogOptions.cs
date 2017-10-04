using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Options
{
    public class BlogOption
    {
        public string AboutMe { get; set; } = "A Coder";
        public string SiteName { get; set; } = "SimpleBlog";
        public string Name { get; set; }
        public List<FriendLinkOption> FriendLinks { get; set; }
    }
}
