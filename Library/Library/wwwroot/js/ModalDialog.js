/// Работа с модальными окнами сайта


$(document).click(function () {

    var modal = $('#ModalDialog');
     //&& $('#ConfirmRaiting').hasClass('show')

    if (modal.hasClass('opened')) {

        var createURL = document.location.protocol + "//" + document.location.host + "/Library/Books/EditBook";
        var editURL = document.location.protocol + "//" + document.location.host + "/Library/Books/CreateBook";

        if (document.location.href.substring(0, createURL.length) != createURL && document.location.href.substring(0, editURL.length) != editURL) {

            window.location.reload();

        }
        else {

            window.location.href = document.referrer;

        }

    }

})

// При клике на кнопку "Ок" модального окна в зависимости от текущего URL происходит либо переадресация, либо обновление страницы
$(document.body).on("click",'#ModalBut', function () {

    var createURL = document.location.protocol + "//" + document.location.host + "/Library/Books/EditBook";
    var editURL = document.location.protocol + "//" + document.location.host + "/Library/Books/CreateBook";

    if (document.location.href.substring(0, createURL.length) != createURL && document.location.href.substring(0, editURL.length) != editURL) {

        window.location.reload();

    }
    else {

        window.location.href = document.referrer;

    }


});

// При клике на кнопку "Подтверждаю" модального окна с подтверждением оценивания происходит сохранение оценки
$('#ConfirmRaitingBut').click(function () {

    var result = document.getElementsByClassName('vote-success').item(1).textContent;
    var prevRait = $('#rait').val();
    var id = $('#bookId').val();

    if (prevRait == 0) {

        $.ajax({
            type: "POST",
            url: document.location.protocol + "//" + document.location.host + "/RaitingBooks/Create",
            data: { Score: result, BookId: id },
            success: function () {

                RefreshList();
                hideConfirmRaiting();
                getModalDialog("Оценка книге успешно поставлена");

            },
            error: function (errorRequest) {

                getErrorInfo(errorRequest.responseText);

            }
        });

    }
    else {


        $.ajax({
            type: "POST",
            url: document.location.protocol + "//" + document.location.host + "/RaitingBooks/Update",
            data: { Score: result, BookId: id },
            success: function () {

                RefreshList();
                hideConfirmRaiting();
                getModalDialog("Оценка книге успешно поставлена");

            },
            error: function (errorRequest) {

            getErrorInfo(errorRequest.responseText);

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

    hideConfirmRaiting();
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

            getConfirmDelete();
            $('#currentBook').val(id.substring(2,5));

        },
        error: function (errorRequest) {

            getErrorInfo(errorRequest.responseText);

        }
    })
});