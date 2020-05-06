/// Ajax- запросы к БД


// Запрос на взятие книги пользователем
$(document.body).on("click", ".ReceivingBook", function () {

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
$(document.body).on("click", '.notificationCreate', function () {

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
$(document.body).on("click", '.ReturnBook', function () {

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

    $('#ConfirmDelete').modal('hide');
    var id = "id" + $('#currentBook').val();
    var bookId = document.getElementById(id).value;

    $.ajax({
        type: "POST",
        url: "CheckBook",
        data: { bookId: bookId },
        success: function () {

            $.ajax({
                type: "POST",
                url: "/Library/Books/DeleteBook",
                data: { bookId: bookId },
                success: function () {

                    $('#h').html("Книга успешно удалена");
                    $('#ModalDialog').modal('show');

                }

            });

        },
        error: function () {

            $('#ErrorH').html("Удаление невозможно, книга используется пользователями");
            $('#ErrorInfo').modal('show');

        }

    });


});

// Запрос на добавление книги
$('#create').click(function (e) {

    e.preventDefault();  

    checkInputs();



    if ($('.error').length == 0) {


        var formData = new FormData($('#CreateForm').get(0));

        $.ajax({
            url: 'CreateBook',
            data: formData,
            processData: false,
            contentType: false,
            type: 'POST',
            success: function () {

                $('#h').html("Книга успешно добавлена");
                $('#ModalDialog').modal('show');

            },
            error: function (error) {

                $('#hInfo').html("При создании книги возникла ошибка, проверьте введеные данные и повторите попытку");
                $('#ModalInfo').modal('show');

            }
        });

                //$('#createSub').click();
             
        }
        else {

            $('#hInfo').html("Проверьте введенные данные и повторите попытку");
            $('#ModalInfo').modal('show');

        }

    

});

// Запрос на изменение книги
$('#update').click(function (e) {

    e.preventDefault();

    checkInputs();


    if ($('.error').length == 0) {

        var formData = new FormData($('#UpdateForm').get(0));

        $.ajax({
            url: 'UpdateBook',
            data: formData,
            processData: false,
            contentType: false,
            type: 'POST',
            success: function () {

                $('#h').html("Книга успешно изменена");
                $('#ModalDialog').modal('show');

            },
            error: function (error) {

                $('#hInfo').html("При изменении книги возникла ошибка, проверьте введеные данные и повторите попытку");
                $('#ModalInfo').modal('show');

            }
        });

        //$('#editSub').click();

    }
    else {

        $('#hInfo').html("Проверьте введенные данные и повторите попытку");
        $('#ModalInfo').modal('show');

    }
});


// Запрос на выборку 3-х подходящий ключевых слов
$(document.body).on("keyup", '.keywords', function () {

    var val = this.value;
    var id = '#select' + this.id.substring(3,4);

    $('#drop').remove();

    if (val != '' && val != null) {

        $.ajax({
            type: 'POST',
            url: 'SelectKeyWords',
            data: { Name: val },
            success: function (result) {

                localStorage.setItem("selectedKeyId", JSON.stringify(id));

                $(id).append(result);

            }

        });

    }

});