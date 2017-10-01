(function ($) {
    $.fn.serializeAnything = function () {

        var toReturn = [];
        var els = $(this).find(':input').get();

        $.each(els, function () {
            if (this.name && !this.disabled && (this.checked || /select|textarea/i.test(this.nodeName) || /text|hidden|password/i.test(this.type))) {
                var val = $(this).val();
                toReturn.push(encodeURIComponent(this.name) + "=" + encodeURIComponent(val));
            }
        });

        return toReturn.join("&").replace(/%20/g, "+");
    }

    $.ajaxWhithToken = function (ajaxObj) {
        ajaxObj.headers = ajaxObj.headers || {};
        ajaxObj.headers["X-CSRF-TOKEN"] = $("meta[name='X-CSRF-TOKEN']").attr('content');
        $.ajax(ajaxObj);
    }
    $.postWithToken = function (url, data, successcb) {
        console.log(url);
        console.log($("meta[name='X-CSRF-TOKEN']").attr('content'))
        $.ajax({
            url: url,
            data: data,
            method: 'POST',
            success: successcb,
            headers: {
                "X-CSRF-TOKEN": $("meta[name='X-CSRF-TOKEN']").attr('content')
            }
        })
    }

})(jQuery);

