/// Работа с модальными окнами сайта




// При клике на кнопку "Отмена" модального окна, оно скрывается и дальнейшие действия прекращаются
$('#CancelButDelete').click(function () {

    $('#ConfirmDelete').modal('hide');

});

// При клике на кнопку "Отмена" модального окна, оно скрывается и дальнейшие действия прекращаются
$('#CancelButReturn').click(function () {

    $('#ConfirmReturn').modal('hide');

});

// При клике на кнопку "Отмена" модального окна, оно скрывается и дальнейшие действия прекращаются
$('#CancelButLogOut').click(function () {

    $('#ConfirmLogOut').modal('hide');

});

// При клике на кнопку "Отмена" модального окна, оно скрывается и дальнейшие действия прекращаются
$('#OkBut').click(function () {


    $('#ModalInfo').modal('hide');

});

$(document).click(function () {

    var modal = $('#ModalDialog');

    if (modal.hasClass('opened') && $('#ConfirmRaiting').hasClass('show')) {

        var currentURL = document.location.protocol + "//" + document.location.host + "/Library/Books/CurrentReadList";
        var previousURL = document.location.protocol + "//" + document.location.host + "/Library/Books/PreviousReadList";
        var allbooksURL = document.location.protocol + "//" + document.location.host + "/Library/Books/AllBooks";

        //alert(document.location.href + "     " + currentURL + "/Library/Books/PreviousReadList");

        if (document.location.href.substring(0, currentURL.length) == currentURL || document.location.href.substring(0, previousURL.length) == previousURL || document.location.href.substring(0, allbooksURL.length) == allbooksURL) {

            window.location.reload();

        }
        else {

            window.location.href = document.referrer;

        }

    }

})

// При клике на кнопку "Ок" модального окна в зависимости от текущего URL происходит либо переадресация, либо обновление страницы
$('#ModalBut').click(function () {

    var currentURL = document.location.protocol + "//" + document.location.host + "/Library/Books/CurrentReadList";
    var previousURL = document.location.protocol + "//" + document.location.host + "/Library/Books/PreviousReadList";
    var allbooksURL = document.location.protocol + "//" + document.location.host + "/Library/Books/AllBooks";

    //alert(document.location.href + "     " + currentURL + "/Library/Books/PreviousReadList");

    if (document.location.href.substring(0, currentURL.length) == currentURL || document.location.href.substring(0, previousURL.length) == previousURL || document.location.href.substring(0, allbooksURL.length) == allbooksURL) {

        window.location.reload();

    }
    else {

        window.location.href = document.referrer;

    }


});

// При клике на кнопку "Ок" модального окна с ошибкой, оно просто закрывается
$('#ErrorBut').click(function () {

    $('#ErrorInfo').modal('hide');

});

// При клике на кнопку "Подтверждаю" модального окна с подтверждением оценивания происходит сохранение оценки
$('#ConfirmRaitingBut').click(function () {

    var result = document.getElementsByClassName('vote-success').item(1).textContent.replace('.', ',');
    var prevRait = $('#rait').val();
    var id = $('#bookId').val();

    if (prevRait == 0) {

        $.ajax({
            type: "POST",
            url: document.location.protocol + "//" + document.location.host + "/RaitingBooks/Create",
            data: { Score: result, BookId: id },
            success: function () {

                $('#ConfirmRaiting').modal('hide');
                $('#h').html("Оценка книге успешно поставлена");
                $('#ModalDialog').modal('show');
                $('#ModalDialog').addClass('opened');

            },
            error: function () {

                $('#ErrorH').html("При добавлении оценки возникла ошибка");
                $('#ErrorInfo').modal('show');

            }
        });

    }
    else {


        $.ajax({
            type: "POST",
            url: document.location.protocol + "//" + document.location.host + "/RaitingBooks/Update",
            data: { Score: result, BookId: id },
            success: function () {

                $('#ConfirmRaiting').modal('hide');
                $('#h').html("Оценка книге успешно поставлена");
                $('#ModalDialog').modal('show');
                $('#ModalDialog').addClass('opened');

            },
            error: function () {

                $('#ErrorH').html("При изменении оценки возникла ошибка");
                $('#ErrorInfo').modal('show');

            }
        });

    }

})

// При клике на кнопку "Продолжить" окно просто закрывается
$('#NextBut').click(function () {

    document.location.reload();

});

// При клике на кнопку "Отмена" модального окна с подтверждением оценивания, оно закрывается и дальнейшие действия не производятся
$('#CancelButRaiting').click(function () {

    $('#ConfirmRaiting').modal('hide');
    GetRaitings();

});
// При клике по кнопке удаления книги вызывается модальное окно для подтверждения удаления
$(document.body).on("click", ".del", function () {

    var id = "id" + this.id.substring(3);
    var bookId = document.getElementById(id).value;

    $.ajax({
        type: "POST",
        url: "CheckBook",
        data: { bookId: bookId },
        success: function () {

            $('#ConfirmDelete').modal('show');
            $('#currentBook').val(id.substring(2,5));

        },
        error: function () {

            $('#ErrorH').html("Удаление невозможно, книга используется пользователями");
            $('#ErrorInfo').modal('show');

        }
    })
});