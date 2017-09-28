$(function () {
    var options = {
        toolbar: document.getElementById('custom-toolbar'),
        editor: document.querySelector('[data-toggle="pen"]'),
        debug: true,
        list: [
            'insertimage', 'blockquote', 'h2', 'h3', 'p', 'code', 'insertorderedlist', 'insertunorderedlist', 'inserthorizontalrule',
            'indent', 'outdent', 'bold', 'italic', 'underline', 'createlink'
        ]
    };
    var pen = window.pen = new Pen(options);
    pen.focus();

    var toolbar = $("#custom-toolbar")[0];
    toolbar.style.left = 'auto'
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            toolbar.style.position = 'fixed';
        } else {
            toolbar.style.position = 'static';
        }
    })

    $(document).keydown(function (e) {
        if (e.ctrlKey & e.keyCode == 83) {
            toastr.success('', '保存成功', { positionClass: "toast-bottom-right" })
            e.preventDefault();
        }
    })
})