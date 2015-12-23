(function () {
    var $content = $(".content-upload"),
        $nameInput = $(".name-input", $content),
        $uploadBtn = $(".upload-btn", $content),
        $imgInput = $(".image-input", $content),
        $albumSelect = $(".album-select", $content),
        imageMaxSize = 4000000,
        albumId;

    $uploadBtn.click(clickUploadBtn);
    $imgInput.change(changeInputImage);
    $albumSelect.change(changeAlbumSelect);
    $(".image-upload-form", $content).submit(submitUploadImageForm);

    albumId = $albumSelect.val();

    $(".alert-success .close", $content).on("click", function (e) {
        $(this).parent().addClass("hide");
    });

    function clickUploadBtn() {
        var name = $nameInput.val();

        if (name.length <= 0) {
            showError("The name mustn't be empty");
            return;
        }

        if ($imgInput.get(0).files[0] === null || $imgInput.get(0).files[0] === undefined) {
            showError("You have to select a photo");
        }

        $(".image-upload-form", $content).submit();
    }

    function changeInputImage(event) {
        var input = event.target;

        if (input.files && input.files[0]) {
            if (input.files[0].size > imageMaxSize) {
                showError("Max size is: " + imageMaxSize + "Kb but your file is:" + input.files[0].size + "Kb");
                input.value = "";
                $(".image-container > img", $content).attr("src", "/images/DefaultPhoto.png");
            } else {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $(".image-container > img", $content).attr("src", e.target.result);
                }

                reader.readAsDataURL(input.files[0]);

                $nameInput.val(input.files[0].name);
            }
        }
    }

    function submitUploadImageForm(event) {
        var formData = new FormData(event.target),
            name = $nameInput.val(),
            size = $imgInput.get(0).files[0].size;

        formData.append("queryName", "uploadPhoto");
        formData.append("albumid", albumId);
        formData.append("name", name);
        formData.append("size", size);
        event.preventDefault();

        $.ajax({
            url: "UsersAjax",
            type: "post",
            data: formData,
            contentType: false,
            cache: false,
            processData: false
        }).success(function (data) {
            var refreshUrl,
                result = JSON.parse(data);

            if (result.Error === null) {
                onUploadSuccess();
            } else {
                showError(result.Error);
            }
        });
    }

    function onUploadSuccess() {
        $(".alert-success", $content).removeClass("hide");
        $(".alert-success i", $content).html("Photo <b>" + $nameInput.val() + "</b> has been added successfully");

        $imgInput.get(0).value = "";
        $nameInput.get(0).value = "";
        $(".image-container > img", $content).attr("src", "/images/DefaultPhoto.png");
    }

    function changeAlbumSelect() {
        albumId = $albumSelect.val();
    }

    function showError(str) {
        var $modal = $(".errorModal", $content);

        $(".modal-body", $modal).html("<p>" + str + "</p>");
        $modal.modal();
    }
}());