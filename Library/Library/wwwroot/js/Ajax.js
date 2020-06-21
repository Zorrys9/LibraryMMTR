/// Ajax- запросы к БД


// Запрос на взятие книги пользователем
$(document.body).on("click", ".ReceivingBook", function () {

    var id = "#id" + this.id;
    var book = $(id).val();

    $.ajax({
        type: "POST",
        url: "ReceivingBook",
        data: { bookId: book },
        success: function (result) {

            RefreshList();
            getModalInfo(result);
            
        },
        error: function (errorRequest) {

            getModalInfo(errorRequest.responseText);

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
        success: function (result) {

            RefreshList();
            getModalInfo(result);

        },
        error: function (errorRequest) {

            getErrorInfo(errorRequest.responseText);

        }

    });

});

// Подтверждение возврата книги
$(document.body).on("click", '.ReturnBook', function () {

    var id = "#id" + this.id;
    var book = $(id).val();
    var raitingUser = $("#raitingUser" + this.id).val();

    $('#IdReturnBook').val(book);

    createConfirmRaitingModal(raitingUser);

    getConfirmReturn();

});

// Запрос на возврат книги пользователем
$('#ConfirmReturnBut').click(function () {

    var book = $('#IdReturnBook').val();

    $.ajax({
        type: "POST",
        url: "ReturnBook",
        data: { bookId: book },
        success: function () {

            RefreshList();
            hideConfirmReturn();
            getConfirmRaitingReturnedModal();

        },
        error: function (errorRequest) {

            getModalInfo(errorRequest.responseText);
        }


    });

});

// Запрос на удаление книги
$('#ConfirmDeleteBut').click(function () {

    hideConfirmDelete();

    var id = "id" + $('#currentBook').val();
    var bookId = document.getElementById(id).value;

    $.ajax({
        type: "POST",
        url: "/Library/Books/DeleteBook",
        data: { bookId: bookId },
        success: function (result) {

            RefreshList();
            getModalDialog(result);

        },
        error: function (errorRequest) {

            getErrorInfo(errorRequest.responseText);

        }

    });

});

// Подтверждение выхода из аккаунта
$(document.body).on("click", '#logOut', function () {

    getConfirmLogOut();

});

$('#ConfirmLogOutBut').click(function () {

    $.ajax({
        type: 'POST',
        url: "/Account/LogOut",
        success: function () {

            document.location.href = document.location.protocol + "//" + document.location.host + "/Account/LogIn";

        },
        error:function(errorRequest){

            hideConfirmLogOut();

            getModalInfo(errorRequest.responseText);

        }
        
    })

})

// Запрос на добавление книги
$('#create').click(function () {

    checkInputs();

    if ($('.error').length == 0) {

       

        var formData = new FormData($('#CreateForm').get(0));

        $.ajax({
            url: 'CreateBook',
            data: formData,
            processData: false,
            contentType: false,
            type: 'POST',
            success: function (result) {

<<<<<<< HEAD
                RefreshList();
=======
                $('#CreatedBook').val('ok');
>>>>>>> 2ff8004efcf18bdc379214a1f5f6733f2b4adc0b
                getModalDialog(result);

            },
            error: function (errorRequest) {

                getModalInfo(errorRequest.responseText);

            }
        })

    }
    else {

        getModalInfo("При создании книги возникла ошибка, проверьте введеные данные и повторите попытку");

    }

    

});

// Запрос на изменение книги
$(document.body).on("click", "#update", function () {

    checkInputs();

    var errors = document.getElementsByClassName('error');

    if (errors.length == 0) {

        var formData = new FormData($('#UpdateForm').get(0));

        $.ajax({
            url: 'UpdateBook',
            data: formData,
            processData: false,
            contentType: false,
            type: 'POST',
            success: function (result) {

                RefreshList();
                getModalDialog(result);

            },
            error: function (errorRequest) {

                getModalInfo(errorRequest.responseText);

            }
        });

    }
    else {

        getModalInfo("Проверьте введенные данные и повторите попытку");

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

            },
            error: function (errorRequest) {

                getModalInfo(errorRequest.responseText);

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
            error: function (errorRequest) {

                getModalInfo("При авторизации возникла ошибка, повторите попытку позже");

            }
        })

    }
    else {

        getModalInfo("Некоторые поля заполнены неверно");

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

                getModalDialog("Регистрация выполнена успешно");

            },
            error: function (errorRequest) {

                getModalInfo(errorRequest.responseText);

            }

        })

    }
    else {

        getModalInfo("Некоторые поля заполнены неверно");

    }
})

$('#ChangeMailingSettingsBtn').click(function () {

    var id = this.id.replace("Change", "");

    $.ajax({
        type: "POST",
        url: "/Settings/ChangeSettings",
        data: { Email: $('#Email').val(), Password: $('#emailPassword').val(), RabbitMQ: $('#RabbitMQ').val(), SMPTport: $('#SMPT_port').val(), SMPThost: $('#SMPT_host').val(), SSL: $("#UseSSL").val() },
        success: function (result) {

            getModalDialog(result);

        },
        error: function (errorRequest) {

            getErrorInfo(errorRequest.responseText);

        }
        
    })

})