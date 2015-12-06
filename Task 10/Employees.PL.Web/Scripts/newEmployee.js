(function () {
    var $content,
        $nameInput,
        $dateInput,
        nameExp = /[^\w\- \.]+|[ ]{2,}|[\-\.]{2,}|^[\-\.]+$|^[ \-\.]| $/;

    $content = $(".content");

    $nameInput = $(".nameInput", $content);
    $nameInput.change(changeNameInput);
    $nameInput.keyup(changeNameInput);

    $dateInput = $(".dateInput", $content);
    $dateInput.change(changeDateInput);
    $dateInput.keyup(changeDateInput);
    $dateInput.inputmask("d.m.9999", { "placeholder": "dd.mm.yyyy" });

    $(".saveBtn", $content).click(clickSaveBtn);

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
                queryName: "clickSaveBtn",
                name: name,
                date: date
            }
        }).success(function (data) {
            var result = JSON.parse(data);

            if (result.result === "") {
                window.location.replace("Employees");
            } else {
                alert(result.result);
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

    function toggleInputError($el, show) {
        var $container = $el.closest(".has-feedback");

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