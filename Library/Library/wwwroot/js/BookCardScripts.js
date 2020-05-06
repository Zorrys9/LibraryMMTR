/// работа с карточкой книги 


$(document).ready(function () {

    var bookId = $('#bookId').val();

    // при загрузке страницы выводится 5 записей с дейтсвиями над книгой
    $.ajax({
        type: "POST",
        url: "/Library/Logs/LogsBook",
        data: { bookId: bookId, count: 5, countRequest: 0 },
        success: function (result) {
            $('#Logs').append(result);
        }


    });

    // при загрузке страницы выводится 5 записей с активными пользователями книги
    $.ajax({
        type: "POST",
        url: "/Library/Holders/HoldersBook",
        data: { bookId: bookId, count: 5, countRequest: 0 },
        success: function (result) {
            $('#Holders').append(result);
        }


    });

    // при клике по кнопке "Показать ещё" добавляется ещё 5 записей с активными пользователями книги
    $(document.body).on("click", "#addholders", function () {

        var countRequest = $('.holderItem').length / 5;
        var a = this.closest('tr');

        $.ajax({
            type: "POST",
            url: "/Library/Holder/HoldersBook",
            data: { bookId: bookId, count: 5, countRequest: countRequest },
            success: function (result) {

                a.parentElement.removeChild(a);
                $('#Holders').append(result);
            }


        });

    });

    // при клике по кнопке "Показать ещё" добавляется ещё 5 записей с действиями над книгой
    $(document.body).on("click", "#addLogs", function () {

        var countRequest = $('.logItem').length / 5;
        var a = this.closest('tr');

        $.ajax({
            type: "POST",
            url: "/Library/Logs/LogsBook",
            data: { bookId: bookId, count: 5, countRequest: countRequest },
            success: function (result) {
                
                a.parentElement.removeChild(a);
                $('#Logs').append(result);
            }


        });

    });

})