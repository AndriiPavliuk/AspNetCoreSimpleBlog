using Blog.AutoMapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Blog.Test.Infrastructure
{
    [AutoMap(typeof(FooForMapperModel))]
    public class FooForMapper
    {
        public int IntValue { get; set; }
        public string StrValue { get; set; }
    }
    public class FooForMapperModel
    {
        public string StrValue { get; set; }
    }
    public class AutoMapper_Test
    {
        
        [Fact]
        public void MapInit_Test()
        {
            BlogAutoMapper.Initialization();
            var foo = new FooForMapper() { StrValue = "333" };
            var fooModel = foo.MapTo<FooForMapperModel>();
            fooModel.StrValue.ShouldBe("333");
        }
    }
}
