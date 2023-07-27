

$(document).ready(function () {
    $('.btn[data-toggle="collapse"]').on('click', function () {
        $(this).toggleClass('collapsed');
    });
});

function loadUpdateForm(id) {
    $.get('/Attandances/Update?id=' + id, function (data) {
        $('#updateAttendancePartialContainer').html(data);
        $('#updateModal').modal('show'); // Show the modal after loading the content
    });
}

function setUpReponseRecaptcha() {
    console.log("có vào")
    var response = grecaptcha.getResponse();
    console.log("mã recaptcha là ", response)
}

function recaptchaCallback(response) {
    // Lưu giá trị response vào thẻ input
    let rp = document.getElementById("ReponseCaptcha")

    rp.value = response;
    console.log("mã capcha la", rp.value)
    console.log("thẻ value", rp)

}
        $(document).ready(function () {
            $('.btn[data-toggle="collapse"]').on('click', function () {
                $(this).toggleClass('collapsed');
            });

        // Handle form submission for the update form
        $('#updateForm').submit(function (event) {
            event.preventDefault(); // Prevent the default form submission

        // Serialize the form data
        var formData = $(this).serialize();

        // Submit the form using jQuery AJAX
        $.post('/Attandances/Update', formData, function (data) {
            // If the form submission is successful, reload the page to refresh the data
            location.reload();
                }).fail(function (xhr, status, error) {
            // If the form submission fails, handle the error (if needed)
            console.error(xhr.responseText);
                });
            });
        });

