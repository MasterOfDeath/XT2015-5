(function () {
    var $content = $(".content"),
        $regNameInput = $(".regNameInput", $content),
        $regPasswordInput = $(".regPasswordInput", $content),
        nameExp = /[^\w]+|_{2,}|^_|_$|^(.)\1+$/;

    $regNameInput.change(changeNameInput);
    $regNameInput.keyup(changeNameInput);
    $regPasswordInput.change(changeNameInput);
    $regPasswordInput.keyup(changeNameInput);


    function changeNameInput(event) {
        var $input = $(event.target),
            str = $input.val() + "";

        toggleInputError($input, !isValidName(str));
    }

    function toggleInputError($el, show) {
        var $container = $el.parents(".has-feedback");

        if (show) {
            $container.removeClass("has-success").addClass("has-error");
            $(".badIcon", $container).removeClass("hide");
            $(".goodIcon", $container).addClass("hide");
            $(".passTipAlert", $content).removeClass("hide");
        } else {
            $container.removeClass("has-error").addClass("has-success");
            $(".badIcon", $container).addClass("hide");
            $(".goodIcon", $container).removeClass("hide");
            $(".passTipAlert", $content).addClass("hide");
        }
    }

    function isValidName(str) {
        return (str.length !== 0
            && str.length >= 6
            && (str.search(nameExp) === -1))
    }
})();