/// Функции, которые используются в работе сайта



// Функция, которая показывает указанный стиль styleProp в элементе el
function getStyle(el, styleProp) {

    var arr = [];
    var x = document.getElementsByClassName(el);
    var y;

    for (var i = 0; i < x.length; i++) {

        if (x[i].currentStyle) {

            y = x[i].currentStyle[styleProp]

        }
        else if (window.getComputedStyle) {

            y = document.defaultView.getComputedStyle(x[i], null).getPropertyValue(styleProp);
        }

        arr[i] = y.substring(0, 2);

    }

    return arr;

}

// Функция удаления категории text
function removeItem(text) {

    if (text == "Разработка") {

        $('#remove1').remove();
        $('input#Development').remove();
        $('#1').removeClass("hidden");

    }

    if (text == "Аналитика") {

        $('#remove2').remove();
        $('input#Analytics').remove();
        $('#2').removeClass("hidden");

    }

    if (text == "Тестирование") {

        $('#remove3').remove();
        $('input#Testing').remove();
        $('#3').removeClass("hidden");

    }

    if (text == "Сопровождение") {

        $('#remove4').remove();
        $('input#Maintenance').remove();
        $('#4').removeClass("hidden");

    }

    if (text == "Управление") {

        $('#remove5').remove();
        $('input#Management').remove();
        $('#5').removeClass("hidden");

    }

    if ($('.itemList').length == 3) {

        $('#listValue').height($('#listValue').height() / 3);

    }

}

// Функция для получение списка из countItems книг на page странице с фильтрацией по name и category с помощью ajax запроса
function getBooks(page, category, name, countItems) {

    var action = document.location.pathname.substring(15);

    $.ajax({
        type: 'POST',
        url: '/Library/Books/ListBooks',
        data: { Name: name, Category: category, page: page, pageItems: countItems, actionName: action },
        success: function (result) {

            localStorage.setItem("SearchName", JSON.stringify(name));
            localStorage.setItem("SearchCategory", JSON.stringify(category));
            localStorage.setItem("CountItems", JSON.stringify(countItems));
            $('#list').html(result);

        },
        error: function () {
            $('#list').html("<p> Книги не найдены </p>");
        }

    });

}

// Функция создает иконку добавления категории
function createAddIcon() {

    var div = document.createElement("div");
    var oncklickAction = "newCategory()";

    div.className = "newCategory";
    div.innerHTML = '<i class="fa fa-plus-square-o" id="newCategory" onclick="' + oncklickAction + '" aria-hidden="true"></i>';

    return div;

}

// Функция для создания новой категории (удаление иконки добавления категории и раскрытие списка категорий)
function newCategory() {

    $('#newCategory').remove();
    $('.dropdown-menu-left').addClass('show');

}

// Функция создания нового блока для категории text с id
function createDiv(text, id) {

    var oncklickAction = "removeItem('" + text + "')";
    var div = document.createElement("div");

    $('#newCategory').remove();
    text += '&ensp;<i type="button" class="fa fa-times" id="remove' + id + '" onclick="' + oncklickAction + '" aria-hidden="true"></i>';
    div.className = "btn btn-secondary disabled itemList";
    div.id = "remove" + id;
    div.innerHTML = text;

    if ($('.itemList').length == 3) {

        $('#listValue').height($('#listValue').height() * 3);

    }

    return div;
}

// Функция удаления ключевого слова с id
function removeKeyWord(id) {

    var createId = '#keyword' + id;
    var last;

    $(createId).remove();

    var value = $('.KeyWord');

    if (value.length != 0) {

        for (var i = 0; i < value.length; i++) {

            last = value[i].id.substring(7);

        }
        
        var idKey = '#keyword' + last;

        if ($('.addKeyWord').length == 0) {

            var removeId = '#remov' + last;
            var selectId = '#select' + last;

            $(removeId).remove();
            $(selectId).remove();

            $(idKey).append('<div class="addKeyWord" onclick="createKeyWord();"><i class="fa fa-plus-square-o" onclick="" aria-hidden="true"></i></div><div class="removeKeyWord" id="remov' + last + '" onclick="removeKeyWord(' + last + ')"><i type="button" class="fa fa-times" id="remove' + last + '" onclick="" aria-hidden="true"></i></div><div id="select' + last + '"></div>');

        }

    }
    else {

        createKeyWord();

    }

}

// Функция создания нового поля для ключевого слова
function createKeyWord() {

    var value = document.getElementsByClassName('keywords');
    var arr = [];

    $('.addKeyWord').remove();

    for (var i = 0; i < value.length; i++) {

        arr[i] = value[i].value;

    }

    var inputHid = '<input type="hidden" class="kw"  asp-for="KeyWordsName" name="KeyWordsName" id="hidden' + value.length + '" value="" />';
    var input = '<input type="text"  placeholder="Разработка" maxlength="50" autocomplete="off" id="key' + value.length + '" class="keywords form-control" value="" />';
    var divCreate = '<div class="addKeyWord" onclick="createKeyWord();"><i class="fa fa-plus-square-o" onclick="" aria-hidden="true"></i></div>';
    var divRemove = '<div class="removeKeyWord" id="remov' + value.length + '" onclick="removeKeyWord(' + value.length + ')"><i type="button" class="fa fa-times" id="remove' + value.length + '" onclick="" aria-hidden="true"></i></div>';
    var divSelect = '<div id="select' + value.length + '"></div>';
    var errorSpan = '<span class="infoError hidden KeyWordError" id="error'+value.length+'">Поле не заполнено</span>';

    $('.KeyWordList').append('<div class="KeyWord" id="keyword' + value.length + '">' + inputHid + input + divCreate + divRemove + divSelect + errorSpan +'</div>');

    for (var i = 0; i < value.length - 1; i++) {

        value[i].value = arr[i];

    }

}

// Функция проверяет содержит ли указанное поле какое-либо значение, если нет то подсвечивает его
function checkValue(input) {

    if (input.val() == '') {

        input.addClass('error');

    }
    else {

        input.removeClass('error');

    }
}

function GetErrorSpan() {

    var items = document.getElementsByClassName('form-control');

    for (var i = 0; i < items.length; i++) {

        var id = "#" + items[i].id + "Error";

        if (items[i].classList.contains('error')) {

            $(id).removeClass('hidden');

        }
        else {

            $(id).addClass('hidden');

        }


    }

}

function getCreate() {

    $.ajax({
        type: 'GET',
        url: 'CreateBook',
        success: function (result) {

            $('#body').append(result);

        }
    });

}

// Проверка всех обязательных полей на наличие каких-либо данных
function checkInputs() {

    var description = $('#Description');
    var year = $('#YearOfPublication');
    var countPages = $('#CountPages');
    var count = $('#Count');
    var author = $('#Author');
    var title = $('#Title');
    var cover = $('#Cover');
    var currentCover = $('#img');

    var keyword = $('.keywords');
    var category = $('.itemList');
            
    checkValue(title);
    checkValue(author);
    checkValue(count);
    checkValue(countPages);
    checkValue(year);
    checkValue(description);

    GetErrorSpan();

    if (keyword.length > 0) {

        for (var i = 0; i < keyword.length; i++) {

            if (keyword[i].value == '') {

                keyword[i].classList.add('error');
                $('#error' + i).removeClass('hidden');

            }
            else {

                keyword[i].classList.remove('error');
                $('#error' + i).addClass('hidden');

            }

        }

    }

    if (currentCover.length == 0) {

        if (cover.val() == '') {

            cover.addClass('error');
            $('#CoverError').removeClass('hidden');

        }
        else {

            cover.removeClass('error');
            $('#CoverError').addClass('hidden');

        }

    }

    if (category.length == 0) {

        $('#listValue').addClass('error');
        $('#btnDropCategory').addClass('error');
        $('#CategoryError').removeClass('hidden');
    }

}

// Проверка почты на валидность
function CheckEmail() {

    var re = /^[\w-\.]+@[\w-]+\.[a-z]{2,4}$/i;
    var myMail = document.getElementById('email');
    var valid = re.test(myMail.value);

    if (!valid) {

        myMail.classList.add('error');
        $('#emailValidationError').removeClass('hidden');
        $('#emailError').addClass('hidden');
    }
    else {

        myMail.classList.remove('error');
        $('#emailValidationError').addClass('hidden');
        $('#emailError').addClass('hidden');

    }

 }

// Проверка пароля по всем необходимым критериям
function checkPassword() {

    var pass = $('#password');
    var spanValidation = $('#validationPasswordError');
    var errors = 0;

    // Проверка длины
    if (pass.val().length < 8) {

        var errorLength = "<span id='length' class='errorPassword' style='color: red;'>Длина пароля должна быть равна или больше 8 символов</span>";

        if ($('#length').length == 0) {

            spanValidation.append(errorLength);

        }

        errors++;

    }
    else {

        pass.removeClass('error');
        $('#length').remove();

    }

    // Проверка наличия строчных символов
    if (!(/[a-z]/.test(pass.val())) && !(/[а-я]/.test(pass.val()))) {

        var errorLower = "<span id='Lower' class='errorPassword' style='color: red;'>Пароль должен содержать строчные символы</span>"


        if ($('#Lower').length == 0) {

            spanValidation.append(errorLower);

        }

        errors++;
    }
    else {

        pass.removeClass('error');
        $('#Lower').remove();

    }

    // Проверка наличия прописных символов
    if (!(/[A-Z]/.test(pass.val())) && !(/[А-Я]/.test(pass.val()))) {

        var errorUpper = "<span id='Upper' class='errorPassword' style='color: red;'>Пароль должен содержать прописные символы</span>";

        if ($('#Upper').length == 0) {

            spanValidation.append(errorUpper);

        }


        errors++;

    }
    else {

        pass.removeClass('error');
        $('#Upper').remove();

    }

    // Проверка наличия цифр
    if (!(/[0-9]/.test(pass.val()))) {

        var errorNum = "<span id='Num' class='errorPassword' style='color: red;'>Пароль должен содержать минимум 1 цифру</span>";

        if (($('#Num').length == 0)) {

            spanValidation.append(errorNum);

        }

        errors++;

    }
    else {

        pass.removeClass('error');
        $('#Num').remove();

    }

    // Проверка наличия спец. символов
    if (!pass.val().match(/[\!\@#\$\%\^\&\*\(\)\+\=\-\[\]\\\'\;\,\/\{\}\|\"\:\<\>\?]/)) {

        var errorSpec = "<span id='Spec' class='errorPassword' style='color: red;'>Пароль должен содержать минимум 1 спец. символ</span>";

        if ($('#Spec').length == 0) {

            spanValidation.append(errorSpec);

        }

        errors++;

    }
    else {

        pass.removeClass('error');
        $('#Spec').remove();
    }


    if (errors > 0) {

        pass.addClass('error');

    }
    else {

        pass.removeClass('error');

    }

}