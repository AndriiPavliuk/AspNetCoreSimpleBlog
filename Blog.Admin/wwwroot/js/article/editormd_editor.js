$(function () {
    var editor
    var SaveArticle = function () {
        var postData = $("#modal_edit_more").serializeFormToObject();
        postData.Tags = [];
        postData.Content = editor.getMarkdown();
        var tags = $('.taglist .label').text().match(/[^ ]+/g);
        $.each(tags || [], function (i, e) {
            postData.Tags.push({
                Name: e
            });
        })
        window.editorUploader.Upload(postData, function (isSuccess, data, res) {
            if (isSuccess && res.status < 300) {
                toastr.success('', '保存成功', { positionClass: "toast-bottom-right" })
            } else {

            }
        })
        console.log(postData);
    }
    var EditModelInit = function () {
        function getTags() {
            var tags = $('.taglist .label').text().match(/[^ ]+/g)
            return tags || [];
        }
        //输入框添加标签
        $("#modal_edit_more").on('keydown', '#tag_input', function (e) {
            if (e.keyCode == 13) {//回车
                var inputVal = $(this).val();
                if (inputVal.length != 0) {
                    var taglist = $('.taglist');
                    if (inputVal.length != 0 && getTags().indexOf(inputVal) == -1) {
                        taglist.append($('<span class="label label-primary">' + inputVal + ' <i class="fa fa-times-circle"></i></span>'));
                    }
                    $(this).val('');
                }
            }
        })
        //delete tag click
        $("#modal_edit_more").on('click', 'i.fa-times-circle', function () {
            $(this).parent().remove();
        })
        $("#modal_edit_more").on('click', '.btn_save', function () {
            SaveArticle();
            $("#modal_edit_more").modal('hide');
        })
        $("#modal_edit_refresh").click(function (e) {
            $("#modal_edit_post_img").attr('src', $('#modal_edit_post_img_src').val());
        })

        //tagEditEvent();
    }
    var EditorInit = function () {
        editor = editormd("editormd", {
            path: "/js/plugins/editorMd/lib/",            toolbarIcons: function () {
                return ["save", "configmore", "undo", "redo", "|", "bold", "hr", "|", "preview", "watch", "fullscreen", "|", "list-ul", "list-ol", "hr", "|", "table", "image", "link", "|", "help"]
            },
            toolbarIconsClass: {
                save: "fa-save",
                configmore: "fa-gear alert-info"
            },
            toolbarIconTexts: {
                save: "保存按钮"
            },
            lang: {
                toolbar: {
                    save: "保存(Ctrl+S)",
                    configmore: "编辑更多"
                }
            },
            toolbarHandlers: {
                save: function (cm, icon, cursor, selection) {
                    //this--->editormd instance
                    SaveArticle();
                },
                /**
                 * @param {Object}      cm         CodeMirror对象
                 * @param {Object}      icon       图标按钮jQuery元素对象
                 * @param {Object}      cursor     CodeMirror的光标对象，可获取光标所在行和位置
                 * @param {String}      selection  编辑器选中的文本
                 */
                configmore: function (cm, icon, cursor, selection) {
                    $("#modal_edit_more").modal("show");
                    icon.removeClass('alert-info')
                }
            }
        });
    }
    $(document).keydown(function (e) {
        if (e.ctrlKey & e.keyCode == 83) {

            SaveArticle();
            e.preventDefault();
        }
    })
    EditorInit();
    EditModelInit();
})