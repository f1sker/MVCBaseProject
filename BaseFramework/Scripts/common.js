/*
    common.js
 */

var AjaxUtil = {
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

                AjAjaxUtilax.Result = data;
            },
            error: function (data) {
                if (objFail != null && objFail != undefined) {
                    objFail.Run(data);
                }

                AjaxUtil.Result = data;
            }
        });

        return AjaxUtil.Result;
    },
    FnReturnAjax: function(url, param) {
        var returnData = '';
        $.ajax({
            url: url,
            data: data,
            type: 'POST',
            async: false,
            success: function (data) {
                returnData = data;
            },
            error: function (e) {
                console.log(e);
            }
        });
        return returnData;
    }
};
