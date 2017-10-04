$(function () {
    var table = $('#article_table').bootstrapTable({
        url: '/Articles/Index?handler=LoadArticle',         //请求后台的URL（*）
        method: 'get',                      //请求方式（*）
        toolbar: '#toolbar',                //工具按钮用哪个容器
        striped: true,                      //是否显示行间隔色
        cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
        pagination: true,                   //是否显示分页（*）
        sortable: false,                     //是否启用排序
        sortOrder: "asc",                   //排序方式
        //sortName: "order",                   //排序字段
        queryParamsType: 'limit',                 //默认值为limit,这里只可设置为其他值或limit
        sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）,注意不同选项要的数据格式不一样
        pageNumber: 1,                       //初始化加载第一页，默认第一页
        pageSize: 10,                       //每页的记录行数（*）
        pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
        search: true,                       //是否显示表格搜索
        strictSearch: true,
        //showColumns: true,                  //是否让用户自定义显示出来的列
        showRefresh: true,                  //是否显示刷新按钮
        minimumCountColumns: 2,             //最少允许的列数
        clickToSelect: true,                //是否启用点击选中行
        //height: 500,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
        uniqueId: "id",                     //每一行的唯一标识，一般为主键列

        cardView: false,                    //是否显示详细视图
        detailView: false,                   //是否显示父子表            
        columns: [{
            field: 'title',
            title: '文章名'
        }, {
            field: 'postDate',
            title: '创建时间',
            formatter: function (v, r, i) {
                var d = new Date(v);
                return d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate() + " " +
                    d.getHours() + ":" + d.getMinutes();
            }
        }, {
            field: 'articleType',
            title: '文章类型',
            formatter: function (v, r, i) {
                switch (v) {
                    case 1:
                        return 'Markdown';
                    case 2:
                        return 'HTML';
                    default:
                        return '未知';
                }
            },
        }, {
            field: 'category',
            title: '文章分类',
            class: 'category_col',
            formatter: function (v, r, i) {
                if (v == null) {
                    return '';
                }
                return v.name;
            }
        },
        {
            field: 'isPublish',
            title: '是否发布',
            formatter: function (value, row, index) {
                //this 是col对象,就是当前json
                return value ? '是' : '否';
            }
        }, {
            title: '操作',
            formatter: function (value, row, index) {
                var detail = '<a onclick="return false;" class="btn btn-primary btn-sm btn_publish" data-id=' + row.id + ' href="#">' + (row.isPublish?'取消发布':'发布')+'</a>';
                var edit = '<a class = "btn btn-white btn-sm J_menuItem" data-menuname="' + row.title + '" href="/Articles/Edit/' + row.id + '">编辑</a>';
                var del = '<a class= "btn btn-white btn-sm btn_delete" data-id=' + row.id + ' href="#">删除</a>';
                return '<div class="btn-group" style="min-width:140px">' + detail + ' ' + edit + ' ' + del + '</div>';
            },
        }]
    });

    $('body').on('click', '.J_menuItem', function () {
        var JMenu = window.top.JMenu;
        if (!JMenu) {
            console.log('JMenu no define');
            return false;
        }
        JMenu.createNewTab(this);
        return false;
    })

    //删除按钮
    $("body").on('click', '.btn_delete', function () {
        var id = $(this).data('id');
        $.ajaxWhithToken({
            url: '/Articles/Index',
            method: 'DELETE',
            data: { id: id },
            success: function () {
                table.bootstrapTable('refresh');
            }
        })
        return false;
    })
    //发布按钮
    $("body").on('click', '.btn_publish', function () {
        var id = $(this).data('id');
        $.ajaxWhithToken({
            url: '/Articles/Index',
            method: 'GET',
            data: {
                id: id,
                handler: 'TogglePublish'
            },
            success: function (date, status, res) {
                if (res.status < 300) {
                    table.bootstrapTable('refresh');
                }
            }
        })
    })

})