/*
    common.js
 */

var Ajax = {
    Result: null,
    FnCallAjax: function (url, param, objSuccess, objFail) {
        $.ajax({
            type: "POST",
            url: url,
            data: param,
            success: function (data) {
                if (objSuccess != null && objSuccess != undefined) {
                    objSuccess.Run(data);
                }

                Ajax.Result = data;
            },
            error: function (data) {
                if (objFail != null && objFail != undefined) {
                    objFail.Run(data);
                }

                Ajax.Result = data;
            }
        });

        return Ajax.Result;
    }
};
