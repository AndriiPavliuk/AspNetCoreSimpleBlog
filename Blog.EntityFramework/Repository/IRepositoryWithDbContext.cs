using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.EntityFramework.Repository
{
    public interface IRepositoryWithDbContext
    {
        DbContext GetDbContext();
    }
}
