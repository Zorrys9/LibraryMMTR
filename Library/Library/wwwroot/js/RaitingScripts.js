$(document).ready(function () {

    GetRaitings();

    $(document.body).on("click", "#raitingUser", function () {

        var result = document.getElementsByClassName('vote-success').item(1).textContent.replace('.', ',');

        $('#ModalDialog').modal('hide');
        $('#score').text(result);
        $('#ConfirmRaiting').modal('show');


    });

    $(document.body).on("click", "#raitingModal", function () {

        var result = document.getElementsByClassName('vote-success').item(4).textContent.replace('.', ',');
        var id = $('#IdReturnBook').val();


         $.ajax({
            type: "POST",
            url: document.location.protocol + "//" + document.location.host + "/RaitingBooks/Create",
            data: { Score: result, BookId: id },
            success: function () {

                $('#ConfirmRaitingReturned').modal('hide');
                $('#h').html("Оценка книге успешно поставлена");
                $('#ModalDialog').modal('show');
                $('#ModalDialog').addClass('opened');

            },
            error: function () {

                $('#ConfirmRaitingReturned').modal('hide');
                $('#ErrorH').html("При добавлении оценки возникла ошибка");
                $('#ErrorInfo').modal('show');

            }
        });


    })
});