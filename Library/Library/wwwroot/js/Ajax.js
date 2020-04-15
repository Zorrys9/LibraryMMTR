/// Ajax- запросы к БД


// Запрос на взятие книги пользователем
$('.ReceivingBook').click(function () {

    var id = "#id" + this.id;
    var book = $(id).val();

    $.ajax({
        type: "POST",
        url: "ReceivingBook",
        data: { bookId: book },
        success: function () {

            $('#h').html("Книга взята для прочтения");
            $('#ModalDialog').modal('show');

        }

    });

});

// Запрос на отправку оповещения при появлении книги
$('.notificationCreate').click(function () {

    var id = "#id" + this.id;
    var book = $(id).val();

    $.ajax({
        type: "POST",
        url: "CreateNotification",
        data: { bookId: book },
        success: function () {

            $('#h').html("При появлении книги в наличии Вам будет отправлено уведомление");
            $('#ModalDialog').modal('show');

        }

    });

});

// Запрос на возврат книги пользователем
$('.ReturnBook').click(function () {

    var id = "#id" + this.id;
    var book = $(id).val();

    $.ajax({
        type: "POST",
        url: "ReturnBook",
        data: { bookId: book },
        success: function () {

            $('#h').html("Вы успешно вернули книгу");
            $('#ModalDialog').modal('show');

        }

    });

});

// Запрос на удаление книги
$('#ConfirmBut').click(function () {

    var id = "id" + $('#currentBook').val();
    var bookId = document.getElementById(id).value;

    $.ajax({
        type: "POST",
        url: "/Library/Books/DeleteBook",
        data: { bookId: bookId },
        success: function () {

            $('#h').html("Книга успешно удалена");
            $('#ModalDialog').modal('show');

        }

    });

});

// Запрос на добавление книги
$('#create').click(function () {

    checkInputs();

        if ($('.error').length == 0) {

            var categories = $('.category');
            var keywords = $('.kw');
            var arrCategories = [];
            var arrKeyWords = []

            for (var i = 0; i < categories.length; i++) {
                arrCategories[i] = categories[i].value;
            }
            for (var i = 0; i < keywords.length; i++) {
                arrKeyWords[i] = keywords[i].value;
            }

            $.ajax({
                type: "POST",
                url: "CreateBook",
                data: {
                    Id: $('#bookId').val(),
                    PrevCount: $('#PrevCount').val(),
                    YearOfPublication: $('#YearOfPublication').val(),
                    Title: $('#Title').val(),
                    Author: $('#Author').val(),
                    Language: $('#Language').val(),
                    URL: $('#URL').val(),
                    CountPages: $('#CountPages').val(),
                    Cover: $('#Cover').val(),
                    Count: $('#Count').val(),
                    Description: $('#Description').val(),
                    IdCategories: arrCategories,
                    KeyWordsName: arrKeyWords
                },
                success: function () {

                    $('#h').html("Книга успешно добавлена");
                    $('#ModalDialog').modal('show');

                }

            });

        }
        else {

            $('#hInfo').html("Проверьте введенные данные и повторите попытку");
            $('#ModalInfo').modal('show');

        }

    

});

// Запрос на изменение книги
$('#update').click(function () {

    checkInputs();

    if ($('.error').length == 0) {

        var categories = $('.category');
        var keywords = $('.kw');
        var arrCategories = [];
        var arrKeyWords = []

        for (var i = 0; i < categories.length; i++) {
            arrCategories[i] = categories[i].value;
        }
        for (var i = 0; i < keywords.length; i++) {
            arrKeyWords[i] = keywords[i].value;
        }

        $.ajax({
            type: "POST",
            url: "UpdateBook",
            data: {
                Id: $('#bookId').val(),
                PrevCount: $('#PrevCount').val(),
                YearOfPublication: $('#YearOfPublication').val(),
                Title: $('#Title').val(),
                Author: $('#Author').val(),
                Language: $('#Language').val(),
                URL: $('#URL').val(),
                CountPages: $('#CountPages').val(),
                Cover: $('#Cover').val(),
                Count: $('#Count').val(),
                Description: $('#Description').val(),
                IdCategories: arrCategories,
                KeyWordsName: arrKeyWords
            },
            success: function () {

                $('#h').html("Книга успешно изменена");
                $('#ModalDialog').modal('show');

            }

        });
    }
    else {

        $('#hInfo').html("Проверьте введенные данные и повторите попытку");
        $('#ModalInfo').modal('show');

    }
});

// Запрос на выборку 3-х подходящий ключевых слов
$(document.body).on("keyup", '.keywords', function () {

    var val = this.value;
    var id = '#select' + this.id;

    $('#drop').remove();

    if (val != '' && val != null) {

        $.ajax({
            type: 'POST',
            url: 'SelectKeyWords',
            data: { Name: val },
            success: function (result) {

                $(id).append(result);

            }

        });

    }

});