(function () {
    var $content = $(".content"),
        $saveBtn = $(".saveBtn", $content),
        $table = $(".securityTable", $content);
    
    $saveBtn.click(clickSaveBtn)

    function clickSaveBtn(event) {
        var $saveBtn = $(event.target),
            array = [];

        if ($("tbody > tr > td:first-child > input:checked", $table).length < 1) {
            showError("At least one user has to have Admin role");
            return;
        }

        $saveBtn.prop("disabled", true);

        $("tbody > tr", $table).each(function () {
            var $tr = $(this),
                username = $tr.data("username"),
                isChecked = $("td:first-child > input", $tr).prop("checked");

            array.push({ isAdmin: isChecked, userName: username });
        });

        $.ajax({
            url: "AjaxQueries",
            method: "post",
            data: {
                queryName: "saveRoles",
                array: JSON.stringify(array)
            }
        }).success(function (data) {
            var result = JSON.parse(data);

            if (result.Answer === "") {
                window.location.reload();
            } else {
                showError(result.Answer);
            }
        }).always(function () {
            $saveBtn.prop("disabled", false);
        }); 
    }

    function showError(str) {
        var $modal = $(".errorModal", $content);

        $(".modal-body", $modal).html("<p>" + str + "</p>");
        $modal.modal();
    }
})();