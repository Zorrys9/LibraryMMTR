/// Работа с модальными окнами сайта




// При клике на кнопку "Отмена" модального окна, оно скрывается и дальнейшие действия прекращаются
$('#CancelBut').click(function () {

    $('#ConfirmDelete').modal('hide');

});

// При клике на кнопку "Отмена" модального окна, оно скрывается и дальнейшие действия прекращаются
$('#OkBut').click(function () {

    if (document.location.pathname == "/Account/Registration") {

        document.location.href = document.location.protocol + "//" + document.location.host + "/Account/LogIn";

    }

    $('#ModalInfo').modal('hide');

});

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