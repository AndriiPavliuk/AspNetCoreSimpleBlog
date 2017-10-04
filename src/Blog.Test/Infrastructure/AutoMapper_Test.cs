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
    [AutoMap(typeof(FooForMapperModel),typeof(FooForMapperModel2))]
    public class FooForMapper
    {
        public int IntValue { get; set; }
        public string StrValue { get; set; }
        public string StrValue2 { get; set; }
        //Only Allow FooForMapper MapTo FooForMapperModel
        [IgnoreMap]//equal to OnlyMapTo
        public string StrValueForIgnoreMapAttribute { get; set; }
        [OnlyMapFrom]
        public int IntValueForOnlyMapAttribute { get; set; }
        [OnlyMapTo]
        public double DoubleValueForOnlyMapAttribute { get; set; }
        [DontMapTo(typeof(FooForMapperModel))]
        public float floatValueForDontMapTo { get; set; }
        public DateTime Date { get; set; }
    }
    [MapIgnoreNullMember]
    public class FooForMapperModel
    {
        public string StrValue { get; set; }
        public string StrValue2 { get; set; }
        public string StrValueForIgnoreMapAttribute { get; set; }
        public int IntValueForOnlyMapAttribute { get; set; }
        public double DoubleValueForOnlyMapAttribute { get; set; }
        public float floatValueForDontMapTo { get; set; }
        public DateTime Date { get; set; }
    }

    public class FooForMapperModel2
    {
        public float floatValueForDontMapTo { get; set; }
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
        }

        [Fact]
        public void IgnoreMemberMap_Test()
        {
            var obj1 = new FooForMapperModel()
            {
                StrValueForIgnoreMapAttribute = "123"
            };
            var obj2 = obj1.MapTo<FooForMapper>();
            obj2.StrValueForIgnoreMapAttribute.ShouldBeNull();
            obj2 = new FooForMapper()
            {
                StrValueForIgnoreMapAttribute = "456"
            };
            obj1 = obj2.MapTo<FooForMapperModel>();
            obj1.StrValueForIgnoreMapAttribute.ShouldBe("456");
        }

        [Fact]
        public void OnlyMapFrom_Test()
        {
            //Only FooForMapperModel.IntValuForOnlyMapAttribute MapTo FooForMapper

            //Try FooForMapperModel To FooForMapper
            var foo = new FooForMapperModel()
            {
                IntValueForOnlyMapAttribute = 666
            };
            var foo2 = foo.MapTo<FooForMapper>();
            foo2.IntValueForOnlyMapAttribute.ShouldBe(666);

            //Try FooForMapper  To FooForMapperModel  
            foo2 = new FooForMapper()
            {
                IntValueForOnlyMapAttribute = 777
            };
            foo = foo2.MapTo<FooForMapperModel>();
            foo.IntValueForOnlyMapAttribute.ShouldBe(0);
        }
        [Fact]
        public void OnlyMapTo_Test()
        {
            //Only FooForMapper.DoubleValueForOnlyMapAttribute MapTo FooForMapperModel

            //Try FooForMapperModel To FooForMapper
            var foo = new FooForMapperModel()
            {
                DoubleValueForOnlyMapAttribute = 3.33
            };
            var foo2 = foo.MapTo<FooForMapper>();
            foo2.DoubleValueForOnlyMapAttribute.ShouldBe(0,0.1);

            //Try FooForMapper  To FooForMapperModel  
            foo2 = new FooForMapper()
            {
                DoubleValueForOnlyMapAttribute = 777.777
            };
            foo = foo2.MapTo<FooForMapperModel>();
            foo.DoubleValueForOnlyMapAttribute.ShouldBe(777.777,0.1);
        }
        [Fact]
        public void DontMapTo_Test()
        {
            //FooForMapper canot mapto FooForMapperModel
            var foo = new FooForMapper()
            {
                floatValueForDontMapTo = 2.33f
            };
            var foo2 = foo.MapTo<FooForMapperModel>();
            foo2.floatValueForDontMapTo.ShouldBe(0, 0.1);
            var foo3 = foo.MapTo<FooForMapperModel2>();
            foo3.floatValueForDontMapTo.ShouldBe(2.33f, 0.1);
        }

        private object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }

    //public class Bar
    //{
    //    public string Val { get; set; }
    //    public string Val2 { get; set; }
    //}
    //public class BarViewModal
    //{
    //    public string Val { get; set; }
    //}
}
