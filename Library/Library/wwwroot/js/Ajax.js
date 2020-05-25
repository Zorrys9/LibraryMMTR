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
            $('#ModalDialog').addClass('opened');


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
            $('#ModalDialog').addClass('opened');

        }

    });

});

// Подтверждение возврата книги
$(document.body).on("click", '.ReturnBook', function () {

    var id = "#id" + this.id;
    var book = $(id).val();

    $('#IdReturnBook').val(book);

    $('#ConfirmReturn').modal('show');

});

// Запрос на возврат книги пользователем
$('#ConfirmReturnBut').click(function () {

    var book = $('#IdReturnBook').val();

    $.ajax({
        type: "POST",
        url: "ReturnBook",
        data: { bookId: book },
        success: function () {

            $('#ConfirmReturn').modal('hide');
            $('#h').html("Вы успешно вернули книгу");
            $('#ModalDialog').modal('show');
            $('#ModalDialog').addClass('opened');
        }

    });

});

// Запрос на удаление книги
$('#ConfirmDeleteBut').click(function () {

    $('#ConfirmDelete').modal('hide');
    var id = "id" + $('#currentBook').val();
    var bookId = document.getElementById(id).value;

    $.ajax({
        type: "POST",
        url: "/Library/Books/DeleteBook",
        data: { bookId: bookId },
        success: function () {

            $('#h').html("Книга успешно удалена");
            $('#ModalDialog').modal('show');
            $('#ModalDialog').addClass('opened');

        }

    });

});

// Подтверждение выхода из аккаунта
$(document.body).on("click", '#logOut', function () {

    $('#ConfirmLogOut').modal('show');

});

$('#ConfirmLogOutBut').click(function () {

    $.ajax({
        type: 'POST',
        url: "/Account/LogOut",
        success: function () {

            document.location.href = document.location.protocol + "//" + document.location.host + "/Account/LogIn";

        },
        error:function(){

            $('#ConfirmLogOut').modal('hide');
            $('#hInfo').html("При выходе из аккаунта произошла ошибка");
            $('#ModalInfo').modal('show');

        }
        
    })

})

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
                $('#ModalDialog').addClass('opened');

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
                $('#ModalDialog').addClass('opened');

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

// Запрос на авторизацию пользователя
$('#auth').click(function () {

    
    var inputs = $('.form-control');

    for (var i = 0; i < inputs.length; i++) {

        var text = inputs[i].value;

        if (text == ' ' || text == '') {

            inputs[i].classList.add('error');

        }
        else {

            inputs[i].classList.remove('error');

        }

    }

    var errors = $('.error');

    if (errors.length == 0) {

        var form = new FormData($('#authForm').get(0));

        $.ajax({
            type: "POST",
            url: "LogIn",
            data: form,
            contentType: false,
            processData: false,
            success: function () {

                location.replace(document.location.protocol + "//" + document.location.host + "/Library/Books/AllBooks");

            },
            error: function () {

                $('#hInfo').html("При авторизации возникла ошибка. Проверьте введенные данные и повторите попытку");
                $('#ModalInfo').modal('show');

            }
        })

    }
    else {

        $('#hInfo').html("Некоторые поля заполнены неверно");
        $('#ModalInfo').modal('show');

    }

})

// Проверка доступен ли email для регистрации
$('#email').blur(function () {

    var email = this.value;
    var emailInpt = this;

    CheckEmail();

    if (this.classList.contains('error')) {


    }
    else {

        $.ajax({
            type: "POST",
            url: "CheckEmail",
            data: { email: email },
            success: function () {

                emailInpt.classList.remove('error');
                $('#emailError').addClass('hidden');

            },
            error: function () {

                emailInpt.classList.add('error');
                $('#emailError').removeClass('hidden');

            }


        })

    }

})


// Проверка доступен ли логин для регистрации
$('#login').blur(function () {

    var login = this.value;
    var loginInpt = this;

    $.ajax({
        type: "POST",
        url: "CheckUserName",
        data: { userName: login },
        success: function () {

            loginInpt.classList.remove('error');
            $('#loginError').addClass('hidden');

        },
        error: function () {

            loginInpt.classList.add('error');
            $('#loginError').removeClass('hidden');

        }

    })


})


// Запрос на регистрацию пользователя
$('#register').click(function () {

    var inputs = $('.form-control');

    CheckEmail();

    for (var i = 0; i < inputs.length; i++) {

        var text = inputs[i].value;

        if (text == ' ' || text == '') {

            this.classList.add('error');

        }
        else {

            this.classList.remove('error');

        }

    }

    checkPassword();

    var password = $('#password').value;
    var confirm = $('#confirmPassword').value;

    if (password != confirm) {

        $('#confirmPassword').addClass('error');
        $('#confirmedError').removeClass('hidden');

    }
    else {

        $('#confirmPassword').removeClass('error');
        $('#confirmedError').addClass('hidden');
    }

    var errors = $('.error');

    if (errors.length == 0) {

        var form = new FormData($('#formRegistration').get(0));

        $.ajax({
            type: "POST",
            url: "Registration",
            data: form,
            processData: false,
            contentType:false,
            success: function () {

                $('#h').html("Регистрация выполнена успешно");
                $('#ModalDialog').modal('show');
                $('#ModalDialog').addClass('opened');

            },
            error: function () {

                $('#hInfo').html("При регистрации возникла ошибка");
                $('#ModalInfo').modal('show');

            }

        })

    }
    else {

        $('#hInfo').html("Некоторые поля заполнены неверно");
        $('#ModalInfo').modal('show');

    }
})