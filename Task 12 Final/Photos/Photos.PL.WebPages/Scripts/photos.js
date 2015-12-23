(function () {
    var $content = $(".content-photos"),
        $thumbnails = $(".thumbnail", $content),
        $editPrompt = $(".edit-prompt", $content),
        $removePrompt = $(".remove-prompt", $content),
        $editPromptInput = $("input[type='text']", $editPrompt),
        photoId;

    $thumbnails.on("click", ".remove-btn", clickRemoveBtn);
    $thumbnails.on("click", ".edit-btn", clickEditBtn);
    $thumbnails.on("click", ".photo-like-notowner", clickLikeBtn);

    $(".fancybox").fancybox({
        openEffect: "none",
        closeEffect: "none",
        type: "image",
        loop: false,
        fixed: false,
        autoCenter: false
    });

    function clickEditBtn(e) {
        e.stopPropagation();
        e.preventDefault();
        var $thisThumb = $(e.target).closest(".thumbnail");

        $editPromptInput.val($(".text-right > small", $thisThumb).text());
        $(".modal-title", $editPrompt).text("Edit Photo");
        photoId = $thisThumb.data("photo-id") + "";
        $editPrompt.modal();
    }

    function clickRemoveBtn(e) {
        e.stopPropagation();
        e.preventDefault();
        var $thisThumb = $(e.target).closest(".thumbnail");

        $(".modal-body", $removePrompt).html("Do you really want to delete photo "
            + "<b>" + $(".text-right > small", $thisThumb).text() + "</b>?");

        $(".modal-title", $removePrompt).text("Remove Photo");
        photoId = $thisThumb.data("photo-id") + "";
        $removePrompt.modal();
    }

    function clickLikeBtn(e) {
        e.stopPropagation();
        e.preventDefault();
        var $thisThumb = $(e.target).closest(".thumbnail");

        photoId = $thisThumb.data("photo-id") + "";
        
        $.ajax({
            url: 'UsersAjax',
            method: 'post',
            data: {
                queryName: "clickLikeBtn",
                photoid: photoId
            }
        }).success(function (data) {
            var result = JSON.parse(data);

            if (result.Error === null) {
                window.location.reload();
            } else {
                showError(result.Error);
            }
        })
    }

    $(".edit-prompt-btn", $editPrompt).click(function () {
        var name = $editPromptInput.val();

        if (name === null || name === undefined || name.length <= 0) {
            showError("Please enter new name.");
        } else {
            $.ajax({
                url: 'UsersAjax',
                method: 'post',
                data: {
                    queryName: "clickPromptEditPhotoBtn",
                    photoid: photoId,
                    name: name
                }
            }).success(function (data) {
                var result = JSON.parse(data);

                if (result.Error === null) {
                    window.location.reload();
                } else {
                    showError(result.Error);
                }
            })
        }
    });

    $(".remove-prompt-btn", $removePrompt).click(function () {
        $.ajax({
            url: 'UsersAjax',
            method: 'post',
            data: {
                queryName: "clickPromptRemovePhotoBtn",
                photoid: photoId
            }
        }).success(function (data) {
            var result = JSON.parse(data);

            if (result.Error === null) {
                window.location.reload();
            } else {
                showError(result.Error);
            }
        })
    });

    function showError(str) {
        var $modal = $(".errorModal", $content);

        $(".modal-body", $modal).html("<p>" + str + "</p>");
        $modal.modal();
    }

}());