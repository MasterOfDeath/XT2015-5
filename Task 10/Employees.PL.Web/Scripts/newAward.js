(function () {
    var $content,
        $titleInput,
        $deletePrompt,
        titleExp = /[^\w\- \.]+|[ ]{2,}|[\-\.]{2,}|^[\-\.]+$|^[ \-\.]| $/,
        awardId;

    $content = $(".content");

    $titleInput = $(".titleInput", $content);
    $titleInput.change(changeTitleInput);
    $titleInput.keyup(changeTitleInput);

    $deletePrompt = $(".deletePrompt", $content);
    $(".modal-title", $deletePrompt).text("Delete Award");
    $(".deletePromptBtn", $deletePrompt).click(clickDeletePrompt);

    $(".saveBtn", $content).click(clickSaveBtn);

    $(".deleteBtn", $content).click(clickDeleteBtn);

    awardId = $content.data("award-id") + "";

    if (awardId !== "0") {
        $(".editBlock", $content).removeClass("hide");
    }

    function clickSaveBtn() {
        var title = $titleInput.val();

        if (!isValidTitle(title))
        {
            toggleInputError($titleInput, true);
            return;
        }

        $.ajax({
            url: 'AjaxQueries',
            method: 'post',
            data: {
                queryName: "clickSaveAward",
                awardid: awardId,
                title: title
            }
        }).success(function (data) {
            var result = JSON.parse(data);

            if (result.answer === "") {
                window.location.replace("Awards");
            } else {
                showError(result.answer);
            }
        })
    }

    function clickDeleteBtn() {
        hasAwardOwners();
    }

    function changeTitleInput(event) {
        var $input = $(event.target),
            str = $input.val() + "";

        toggleInputError($input, !isValidTitle(str));
    }

    function clickDeletePrompt() {
        $.ajax({
            url: 'AjaxQueries',
            method: 'post',
            data: {
                queryName: "clickDeleteAward",
                awardid: awardId
            }
        }).success(function (data) {
            var result = JSON.parse(data);

            if (result.answer === "") {
                window.location.replace("Awards");
            } else {
                showError(result.answer);
            }
        })
    }

    function hasAwardOwners() {
        if (awardId !== "0") {
            $.ajax({
                url: 'AjaxQueries',
                method: 'post',
                data: {
                    queryName: "hasAwardOwners",
                    awardid: awardId
                }
            }).success(function (data) {
                var result = JSON.parse(data);

                if (result.answer.toLowerCase() === "true") {
                    $(".modal-body", $deletePrompt).html("<p>This award has owners.<br/>Do you really want to continue?</p>");
                }
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

    function isValidTitle(str) {
        return (str.length !== 0 && (str.search(titleExp) === -1))
    }

})();