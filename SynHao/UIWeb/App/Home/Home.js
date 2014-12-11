/// <reference path="../App.js" />

(function () {
    "use strict";

    // 每次加载新页面时均必须运行初始化函数
    Office.initialize = function (reason) {
        $(document).ready(function () {
            app.initialize();

            $('#get-data-from-selection').click(getDataFromSelection);
        });
    };

    // 从当前选择的文档中读取数据并显示通知
    function getDataFromSelection() {
        Office.context.document.getSelectedDataAsync(Office.CoercionType.Text,
            function (result) {
                if (result.status === Office.AsyncResultStatus.Succeeded) {
                    app.showNotification('选定的文本为: ', '“' + result.value + '”');
                } else {
                    app.showNotification('错误: ', result.error.message);
                }
            }
        );
    }
})();