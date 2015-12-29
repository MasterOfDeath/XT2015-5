(function () {
    var $content,
        $nameInput,
        $dateInput,
        $deletePrompt,
        $awardPrompt,
        $awardGivePromptBtn,
        $awardRevokePromptBtn,
        $avatarContainer,
        nameExp = /[^\w\- \.]+|[ ]{2,}|[\-\.]{2,}|^[\-\.]+$|^[ \-\.]| $/,
        userId,
        awardId = "",
        imageMaxSize = 4000000,
        doINeedToSaveImage = false;

    $content = $(".content");

    $nameInput = $(".nameInput", $content);
    $nameInput.change(changeNameInput);
    $nameInput.keyup(changeNameInput);

    $dateInput = $(".dateInput", $content);
    $dateInput.change(changeDateInput);
    $dateInput.keyup(changeDateInput);
    $dateInput.inputmask("d.m.9999", { "placeholder": "dd.mm.yyyy" });

    userId = $content.data("user-id") + "";
    if (userId !== "0") {
        $(".editBlock", $content).removeClass("hide");
    }

    $(".saveBtn", $content).click(clickSaveBtn);

    $(".giveAwardBtn", $content).click(clickGiveAwardBtn);
    $(".revokeAwardBtn", $content).click(clickRevokeAwardBtn);

    $awardPrompt = $(".awardPrompt", $content);
    $awardGivePromptBtn = $(".awardGivePromptBtn", $awardPrompt);
    $awardGivePromptBtn.click(clickAwardGivePromptBtn);
    $awardRevokePromptBtn = $(".awardRevokePromptBtn", $awardPrompt);
    $awardRevokePromptBtn.click(clickAwardRevokePromptBtn);

    $deletePrompt = $(".deletePrompt", $content);
    $(".modal-title", $deletePrompt).text("Delete Employee");
    $(".deletePromptBtn", $deletePrompt).click(clickDeletePrompt);

    $avatarContainer = $(".avatar-container", $content);
    $("input", $avatarContainer).change(changeInputImage);
    
    $("form", $avatarContainer).submit(submitUploadImageForm);

    function changeInputImage(event) {
        var input = event.target;

        if (input.files && input.files[0]) {
            if (input.files[0].size > imageMaxSize) {
                showError("Max size is: " + imageMaxSize + "Kb but your file is:" + input.files[0].size + "Kb");
                doINeedToSaveImage = false;
            } else {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $("img", $avatarContainer).attr("src", e.target.result);
                }

                reader.readAsDataURL(input.files[0]);
                doINeedToSaveImage = true;
            }
        } else {
            doINeedToSaveImage = false;
        }
    }

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
            
            if (result.Answer === null) {
                if (doINeedToSaveImage) {
                    userId = result.Data + "";
                    $("form", $avatarContainer).submit();
                } else {
                    window.location.replace("Employees");
                }

            } else {
                showError(result.Answer);
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

    function clickDeletePrompt(event) {
        $(event.target).prop("disabled", true);

        $.ajax({
            url: 'AjaxQueries',
            method: 'post',
            data: {
                queryName: "clickDeleteEmployee",
                userid: userId
            }
        }).success(function (data) {
            var result = JSON.parse(data);
            
            if (result.Answer === null) {
                window.location.replace("Employees");
            } else {
                showError(result.Answer);
            }
        }).always(function () {
            $(event.target).prop("disabled", false);
        });
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

                if (result.Answer === null) {
                    window.location.reload();
                } else {
                    showError(result.Answer);
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

                if (result.Answer === null) {
                    window.location.reload();
                } else {
                    showError(result.Answer);
                }
            })
        }
    }

    function submitUploadImageForm(event) {
        var formData = new FormData(event.target);
        formData.append("queryName", "uploadUserImage");
        formData.append("userid", userId);
        event.preventDefault();

        $.ajax({
            url: "AjaxQueries",
            type: "post",
            data: formData,
            contentType: false,
            cache: false,             
            processData: false
        }).success(function (data) {
            var refreshUrl,
                result = JSON.parse(data);

            if (result.Answer === null) {
                window.location.replace("Employees");
            } else {
                showError(result.Answer);
            }
        });
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

        if (result.Data !== null) {
            $table = $(result.Data);
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