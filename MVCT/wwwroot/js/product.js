

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


$(document).ready(function () {
    $("#btnSave").click(function () {
        var empForm = $("#saveForm").serialize();
        $.ajax({
            type: "POST",
            url: "/Reports/create",
            data: empForm,
            success: function () {
                window.location.href = "/Reports/Index";
            }
        })
    })

    $(".btnCancel").click(function () {
        window.location.href = "/Reports/Index";
    })

    $("#btnDelete").click(function () {
        var idD = $("#EmpId").val();
        $.ajax({
            type: "POST",
            url: "/Reports/DeleteEmployee",
            data: { id: idD },
            success: function (result) {
                if (result) {
                    $("EmpId").val(null);
                    window.location.href = "/Reports/Index";
                }
                else {
                    alert("Opps Something Wrong");
                }
            }
        });
    });

});

var Confirm = function (id) {
    $("#EmpId").val(id);
    $("#DeleteModal").modal('show');
}

var EditForm = function (id) {
    $.ajax({
        type: "POST",
        url: "/Reports/GetEmployee",
        data: { id: id },
        success: function (employee) {
            $("#EditModal").modal('show');
            $("#empName").val(employee.name);
            $(".updateId").val(employee.id);
        }
    })
}