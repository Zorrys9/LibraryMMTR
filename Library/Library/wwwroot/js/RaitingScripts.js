$(document).ready(function () {

    GetRaitings();

    $(document.body).on("click", "#raitingUser", function () {

        var result = document.getElementsByClassName('vote-success').item(1).textContent;

        hideModalDialog();

        $('#score').text(result);

        getConfirmRaiting();

    });

    $(document.body).on("click", "#raitingModal", function () {

        var score = document.getElementsByClassName('vote-success');

        var result = score.item(score.length-1).textContent;
        var id = $('#IdReturnBook').val();

        hideConfirmRaitingReturnedModal();

         $.ajax({
            type: "POST",
             url: document.location.protocol + "//" + document.location.host + "/RaitingBooks/Create",
             data: { Score: result.replace('.',','), BookId: id },
            success: function () {

                RefreshList();
                getModalDialog("Оценка книге успешно поставлена");

            },
             error: function (errorRequest) {

                 getErrorInfo(errorRequest.responseText);

            }
        });


    })
});