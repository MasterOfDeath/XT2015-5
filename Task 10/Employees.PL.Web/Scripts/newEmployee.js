(function () {
    var $content,
        $nameInput,
        $dateInput;

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
        $.ajax({
            url: 'AjaxQueries',
            method: 'post',
            data: {
                queryName: "clickSaveBtn"
            }
        }).success(function (data) {
            alert(data);
        })
    }

    function changeNameInput(event) {
        var nameExp = /[^\w\- \.]+|[ ]{2,}|[\-\.]{2,}|^[\-\.]+$|^ | $/,
            $input = $(event.target),
            str = $input.val() + "";

        if (str.search(nameExp) !== -1) {
            toggleInputError($input, true);
        } else {
            toggleInputError($input, false);
        }
    }

    function changeDateInput(event) {
        var $input = $(event.target),
            str = $input.val() + "";

        if (isDate(str)) {
            toggleInputError($input, false);
        } else {
            toggleInputError($input, true);
        }
    }

    function toggleInputError($el, show) {
        var $container = $el.closest(".has-feedback");

        if (show) {
            $container.addClass("has-error");
            $(".form-control-feedback", $container).removeClass("hide");
        } else {
            $container.removeClass("has-error");
            $(".form-control-feedback", $container).addClass("hide");
        }
    }

    function isDate(txtDate) {
        var currVal = txtDate;
        if (currVal == '')
            return false;

        var rxDatePattern = /^(\d{1,2})(\.)(\d{1,2})(\.)(\d{4})$/; //Declare Regex
        var dtArray = currVal.match(rxDatePattern); // is format OK?

        if (dtArray == null)
            return false;

        //Checks for mm/dd/yyyy format.
        dtDay = dtArray[1];
        dtMonth = dtArray[3];
        dtYear = dtArray[5];

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


})();