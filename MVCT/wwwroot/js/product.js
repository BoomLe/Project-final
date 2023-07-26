

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

