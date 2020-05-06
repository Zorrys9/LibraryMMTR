/// Работа с модальными окнами сайта




// При клике на кнопку "Отмена" модального окна, оно скрывается и дальнейшие действия прекращаются
$('#CancelBut').click(function () {

    $('#ConfirmDelete').modal('hide');

});

// При клике на кнопку "Отмена" модального окна, оно скрывается и дальнейшие действия прекращаются
$('#OkBut').click(function () {

    $('#ModalInfo').modal('hide');

});

// При клике на кнопку "Ок" модального окна в зависимости от текущего URL происходит либо переадресация, либо обновление страницы
$('#ModalBut').click(function () {
    var currentURL = document.location.protocol + "//" + document.location.host + "/Library/Books/CurrentReadList";
    var previousURL = document.location.protocol + "//" + document.location.host + "/Library/Books/PreviousReadList";

    //alert(document.location.href + "     " + currentURL + "/Library/Books/PreviousReadList");

    if (document.location.href.substring(0, currentURL.length) == currentURL || document.location.href.substring(0, previousURL.length) == previousURL) {

        window.location.reload();

    }
    else {

        window.location.replace("AllBooks");

    }

});

// При клике на кнопку "Ок" модального окна с ошибкой, оно просто закрывается
$('#ErrorBut').click(function () {

    $('#ErrorInfo').modal('hide');

});

// При клике по кнопке удаления книги вызывается модальное окно для подтверждения удаления
$(document.body).on("click", ".del", function () {

    $('#ConfirmDelete').modal('show');
    $('#currentBook').val(this.id.substring(3));

});