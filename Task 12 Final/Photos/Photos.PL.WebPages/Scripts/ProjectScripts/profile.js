(function () {
    var $content = $(".content-profile"),
        $nameInput = $(".name-input", $content),
        $firstNameInput = $(".tab-content .firstname-input", $content),
        $lastNameInput = $(".tab-content .lastname-input", $content),
        $oldPasswordInput = $(".tab-content .old-passowrd-input", $content),
        $newPasswordInput = $(".tab-content .new-passowrd-input", $content),
        nameExp = /[^\w\- \.]+|[ ]{2,}|[\-\.]{2,}|^[\-\.]+$|^[ \-\.]| $/,
        passwExp = /^(?=.{6,}$)([^\s]*[a-zA-Z0-9][^\s]*)+$/,
        userId = $content.data("user-id") + "";

    $(".userinfoTab input[type=text]", $content).change(changeNameInput);
    $(".userinfoTab input[type=text]", $content).keyup(changeNameInput);
    $(".passwordTab input[type=password]", $content).keyup(changePasswordInput);

    function changeNameInput(event) {
        var $input = $(event.target),
            str = $input.val() + "";
        toggleInputError($input, !isValidName(str));
    }

    function changePasswordInput(event) {
        var $input = $(event.target),
            str = $input.val() + "";
        toggleInputError($input, str.search(passwExp) === -1);
    }

    $(".saveProfileBtn", $content).click(function (e) {
        var $thisBtn = $(e.target),
            firstname = $firstNameInput.val(),
            lastname = $lastNameInput.val();

        if (isValidName(firstname) && isValidName(lastname) && +userId > 0) {
            $.ajax({
                url: "UsersAjax",
                method: "post",
                data: {
                    queryName: "clickSaveProfileBtn",
                    userid: userId,
                    firstname: firstname,
                    lastname: lastname
                }
            }).success(function (data) {
                var result = JSON.parse(data);

                if (result.Error === null) {
                    $(".alert-success", $content).removeClass("hide");
                    $(".alert-success p", $content).html("Profile info successefully updated");
                } else {
                    showError(result.Error);
                }
            });
        } else {
            showError("Invalid values of FirstName or LastName");
        }
    });

    $(".changePasswordBtn", $content).click(function (e) {
        var $thisBtn = $(e.target),
            oldPassword = $oldPasswordInput.val(),
            newPassword = $newPasswordInput.val();

        if (isValidName(firstname) && isValidName(lastname) && +userId > 0) {
            $.ajax({
                url: "UsersAjax",
                method: "post",
                data: {
                    queryName: "clickChangePasswordBtn",
                    userid: userId,
                    oldpassword: oldPassword,
                    newpassword: newPassword
                }
            }).success(function (data) {
                var result = JSON.parse(data);

                if (result.Error === null) {
                    $(".alert-success", $content).removeClass("hide");
                    $(".alert-success p", $content).html("Password was successefully changed");
                } else {
                    showError(result.Error);
                }
            });
        } else {
            showError("Invalid values of FirstName or LastName");
        }
    });

    $(".alert-success .close", $content).on("click", function (e) {
        $(this).parent().addClass("hide");
    });

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

    function isValidName(str) {
        return (str.length >= 1 && (str.search(nameExp) === -1))
    }
}());