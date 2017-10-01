using AutoMapper;
using Blog.AutoMapper;
using Blog.AutoMapper.Attributes;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Blog.Test.Infrastructure
{
    [MapIgnoreNullMember]
    [AutoMap(targetTypes: typeof(FooForMapperModel))]
    public class FooForMapper
    {
        public int IntValue { get; set; }
        public string StrValue { get; set; }
        public string StrValue2 { get; set; }
        public DateTime Date { get; set; }
    }
    [MapIgnoreNullMember]
    public class FooForMapperModel
    {
        public string StrValue { get; set; }
        public string StrValue2 { get; set; }
        public DateTime Date { get; set; }
    }
    public class AutoMapper_Test
    {
        public AutoMapper_Test()
        {
            BlogAutoMapper.Initialization();
        }

        [Fact]
        public void Map_Test()
        {

            var foo = new FooForMapper() { StrValue = "333" };
            var fooModel = foo.MapTo<FooForMapperModel>();
            fooModel.StrValue.ShouldBe("333");
        }
        [Fact]
        public void MapList_Test()
        {
            var list = new List<FooForMapper>();
            for (var i = 0; i < 10; i++)
            {
                list.Add(new FooForMapper()
                {
                    StrValue = i.ToString()
                });
            }
            var mappedList = list.MapTo<List<FooForMapperModel>>();
            for (var i = 0; i < 10; i++)
            {
                mappedList.ElementAt(i).StrValue.ShouldBe(i.ToString());
            }
        }

        [Fact]
        public void IgnoreNullMember_Test()
        {
            var foo = new FooForMapper() { StrValue = "333", IntValue = 3, Date = DateTime.Now };
            var foo2 = new FooForMapperModel() { StrValue2 = "444" };
            var foo3 = foo2.MapTo(foo);
            foo3.IntValue.ShouldBe(3);
            foo3.StrValue.ShouldBe("333");
            foo3.StrValue2.ShouldBe("444");
            foo3.Date.ShouldNotBe(GetDefaultValue(typeof(DateTime)));
        }
        private object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
