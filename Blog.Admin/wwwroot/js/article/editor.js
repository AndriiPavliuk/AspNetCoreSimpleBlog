window.editorUploader= (function () {
    var editorUploader = {};
    /**
    * @param {object} data
    * @param {requestCallback} cb function(isSuccess,data[if sucess]|res,res[if no sucess])
    * data:{
        @param {int} id
        @param {string} Content
        @param {string} Title
        @param {bool} IsPublish
        @param {string[]} [Tags=[]]
        @param {int} Category.Id - id of category
        @param {string} [PostImage]
        @param {string} [Summary]
        @param {Object[]} [Tags]
    *}
    */
    editorUploader.Upload = function (data, cb) {
        data.Tags = data.Tags || [];
        var postData = {};
        for (var i in data) {
            postData['CurrentArticle.' + i] = data[i];
        }
        $.ajaxWhithToken({
            url: '/Articles/Edit/' + data.Id,
            method: 'PUT',
            data: postData,
            success: function (data, status, res) {
                cb(true, data, res);
            },
            error:function(res) {
                cb(false,res);
            }
        })
    }
    return editorUploader;
})()