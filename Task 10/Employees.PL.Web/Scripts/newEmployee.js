(function () {
    var $content,
        $nameInput,
        $dateInput,
        $deletePrompt,
        $awardPrompt,
        $awardGivePromptBtn,
        $awardRevokePromptBtn,
        nameExp = /[^\w\- \.]+|[ ]{2,}|[\-\.]{2,}|^[\-\.]+$|^[ \-\.]| $/,
        editMode,
        userId,
        awardId = "";

    $content = $(".content");

    $nameInput = $(".nameInput", $content);
    $nameInput.change(changeNameInput);
    $nameInput.keyup(changeNameInput);

    $dateInput = $(".dateInput", $content);
    $dateInput.change(changeDateInput);
    $dateInput.keyup(changeDateInput);
    $dateInput.inputmask("d.m.9999", { "placeholder": "dd.mm.yyyy" });

    editMode = (($content.data("edit-mode") + "") === "1");
    if (editMode) {
        $(".editBlock", $content).removeClass("hide");
    }

    userId = $content.data("user-id") + "";

    $(".saveBtn", $content).click(clickSaveBtn);

    $(".giveAwardBtn", $content).click(clickGiveAwardBtn);
    $(".revokeAwardBtn", $content).click(clickRevokeAwardBtn);

    $awardPrompt = $(".awardPrompt", $content);
    //$awardPrompt.on("show.bs.modal", giveMeAllAwardsTable);
    $awardGivePromptBtn = $(".awardGivePromptBtn", $awardPrompt);
    $awardGivePromptBtn.click(clickAwardGivePromptBtn);
    $awardRevokePromptBtn = $(".awardRevokePromptBtn", $awardPrompt);
    $awardRevokePromptBtn.click(clickAwardRevokePromptBtn);

    $deletePrompt = $(".deletePrompt", $content);
    $(".modal-title", $deletePrompt).text("Delete Employee");
    $(".deletePromptBtn", $deletePrompt).click(clickDeletePrompt);

    function clickSaveBtn() {
        var name = $nameInput.val(),
            date = $dateInput.val();

        if (!isValidName(name))
        {
            toggleInputError($nameInput, true);
            return;
        }

        if (!isDate(date)) {
            toggleInputError($dateInput, true);
            return;
        }

        $.ajax({
            url: 'AjaxQueries',
            method: 'post',
            data: {
                queryName: "clickSaveEmployee",
                userid: userId,
                name: name,
                date: date
            }
        }).success(function (data) {
            var result = JSON.parse(data);

            if (result.answer === "") {
                window.location.replace("Employees");
            } else {
                showError(result.answer);
            }
        })
    }

    function changeNameInput(event) {
        var $input = $(event.target),
            str = $input.val() + "";

        toggleInputError($input, !isValidName(str));
    }

    function changeDateInput(event) {
        var $input = $(event.target),
            str = $input.val() + "";

        toggleInputError($input, !isDate(str));
    }

    function clickDeletePrompt() {
        $.ajax({
            url: 'AjaxQueries',
            method: 'post',
            data: {
                queryName: "clickDeleteEmployee",
                userid: userId
            }
        }).success(function (data) {
            var result = JSON.parse(data);

            if (result.answer === "") {
                window.location.replace("Employees");
            } else {
                showError(result.answer);
            }
        })
    }

    function clickGiveAwardBtn() {
        $awardGivePromptBtn.removeClass("hide");
        $awardRevokePromptBtn.addClass("hide");

        awardId = "";

        giveMeAllAwardsTable();

        $awardPrompt.modal();
    }

    function clickRevokeAwardBtn() {
        $awardRevokePromptBtn.removeClass("hide");
        $awardGivePromptBtn.addClass("hide");

        awardId = "";

        giveMeEmployeesAwardsTable();

        $awardPrompt.modal();
    }

    function clickAwardGivePromptBtn() {
        if (awardId !== "") {
            $.ajax({
                url: 'AjaxQueries',
                method: 'post',
                data: {
                    queryName: "clickGiveAward",
                    userid: userId,
                    awardid: awardId
                }
            }).success(function (data) {
                var result = JSON.parse(data);

                if (result.answer === "") {
                    window.location.reload();
                } else {
                    showError(result.answer);
                }
            })
        }
    }

    function clickAwardRevokePromptBtn() {
        if (awardId !== "") {
            $.ajax({
                url: 'AjaxQueries',
                method: 'post',
                data: {
                    queryName: "clickRevokeAward",
                    userid: userId,
                    awardid: awardId
                }
            }).success(function (data) {
                var result = JSON.parse(data);

                if (result.answer === "") {
                    window.location.reload();
                } else {
                    showError(result.answer);
                }
            })
        }
    }

    function giveMeAllAwardsTable() {
        $.ajax({
            url: 'AjaxQueries',
            method: 'post',
            data: {
                queryName: "giveMeAllAwardsTable"
            }
        }).success(insertTableInAwardPrompt)
    }

    function giveMeEmployeesAwardsTable() {
        $.ajax({
            url: 'AjaxQueries',
            method: 'post',
            data: {
                queryName: "giveMeEmployeesAwardsTable",
                userid: userId
            }
        }).success(insertTableInAwardPrompt)
    }

    function insertTableInAwardPrompt (data) {
        var result = JSON.parse(data),
            $table,
            $td;

        if (result.answer !== "") {
            $table = $(result.answer);
            $table.addClass("table table-hover");
            $(".modal-body table", $awardPrompt).replaceWith($table);
            $td = $("td", $table);
            $td.click(function (event) {
                $td.closest("tr").removeClass("selectedRow");
                $(event.target).closest("tr").addClass("selectedRow");
                awardId = $(event.target).closest("tr").data("award-id");
            })
        }
    }

    function toggleInputError($el, show) {
        var $container = $el.parents(".has-feedback");

        if (show) {
            $container.removeClass("has-success").addClass("has-error");
            $(".badIcon", $container).removeClass("hide");
            $(".goodIcon", $container).addClass("hide");
        } else {
            $container.removeClass("has-error").addClass("has-success");
            $(".badIcon", $container).addClass("hide");
            $(".goodIcon", $container).removeClass("hide");
        }
    }

    function showError(str) {
        var $modal = $(".errorModal", $content);

        $(".modal-body", $modal).html("<p>" + str + "</p>");
        $modal.modal();
    }

    function showDeletePrompt() {
        $(".deletePrompt", $content).modal();
    }

    function isDate(txtDate) {
        var currVal = txtDate;
        if (currVal == '')
            return false;

        var rxDatePattern = /^(\d{1,2})\.(\d{1,2})\.(\d{4})$/; //Declare Regex
        var dtArray = currVal.match(rxDatePattern); // is format OK?

        if (dtArray == null)
            return false;

        //Checks for dd.mm.yyyy format.
        dtDay = dtArray[1];
        dtMonth = dtArray[2];
        dtYear = dtArray[3];

        if (dtMonth < 1 || dtMonth > 12)
            return false;
        else if (dtDay < 1 || dtDay > 31)
            return false;
        else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
            return false;
        else if (dtMonth == 2) {
            var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
            if (dtDay > 29 || (dtDay == 29 && !isleap))
                return false;
        }
        return true;
    }

    function isValidName(str) {
        return (str.length !== 0 && (str.search(nameExp) === -1))
    }

})();