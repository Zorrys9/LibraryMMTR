/// Изменение книг


$(document).ready(function () {

    // При клике по категории "Разработка" добавляем в блок с текущими категориями
    $('#1').click(function () {

        var div = createDiv("Разработка", "1");

        $('#listValue').append(div);
        $('#1').addClass("hidden");
        $('#listValue').append(createAddIcon());
        $('form#CreateForm').append('<input type="hidden" class="category" id="Development" asp-for="IdCategories" name="IdCategories" value="1" />');
        $('form#UpdateForm').append('<input type="hidden" class="category" id="Development" asp-for="IdCategories" name="IdCategories" value="1" />');

    });

    // При клике по категории "Аналитика" добавляем в блок с текущими категориями
    $('#2').click(function () {

        var div = createDiv("Аналитика", "2");

        $('#plus').remove();
        $('#listValue').append(div);
        $('#2').addClass("hidden");
        $('#listValue').append(createAddIcon());
        $('form#CreateForm').append('<input type="hidden" class="category" id="Analytics" asp-for="IdCategories" name="IdCategories" value="2" />');
        $('form#UpdateForm').append('<input type="hidden" class="category" id="Analytics" asp-for="IdCategories" name="IdCategories" value="2" />');

    });

    // При клике по категории "Тестирование" добавляем в блок с текущими категориями
    $('#3').click(function () {

        var div = createDiv("Тестирование", "3");

        $('#listValue').append(div);
        $('#3').addClass("hidden");
        $('#listValue').append(createAddIcon());
        $('form#CreateForm').append('<input type="hidden" class="category" id="Testing" asp-for="IdCategories" name="IdCategories" value="3" />');
        $('form#UpdateForm').append('<input type="hidden" class="category" id="Testing" asp-for="IdCategories" name="IdCategories" value="3" />');

    });

    // При клике по категории "Сопровождение" добавляем в блок с текущими категориями
    $('#4').click(function () {

        var div = createDiv("Сопровождение", "4");

        $('#listValue').append(div);
        $('#4').addClass("hidden");
        $('#listValue').append(createAddIcon());
        $('form#CreateForm').append('<input type="hidden" class="category" id="Maintenance" asp-for="IdCategories" name="IdCategories" value="4" />');
        $('form#UpdateForm').append('<input type="hidden" class="category" id="Maintenance" asp-for="IdCategories" name="IdCategories" value="4" />');

    });

    // При клике по категории "Управление" добавляем в блок с текущими категориями
    $('#5').click(function () {

        var div = createDiv("Управление", "5");

        $('#listValue').append(div);
        $('#5').addClass("hidden");
        $('#listValue').append(createAddIcon());
        $('form#CreateForm').append('<input type="hidden" class="category" id="Management" asp-for="IdCategories" name="IdCategories" value="5" />');
        $('form#UpdateForm').append('<input type="hidden" class="category" id="Management" asp-for="IdCategories" name="IdCategories" value="5" />');

    });

    // При клике по категории в списке она в нем скрывается
    $('a.dropdown-item').click(function () {

        $('.dropdown-menu-left').removeClass('show');

    });

    // При изменении поля с ключевыми словами изменяем скрытое поле для будущей отправки в БД
    $(document.body).on("change", '.keywords', function () {

        var keywords = document.getElementsByClassName('keywords');

        for (var i = 0; i < keywords.length; i++) {
            var keyword = $('#hidden' + keywords[i].id.substring(3,4));
            keyword.val(keywords[i].value);
        }

    });


    
    // При изменении списка категорий проверяется количество этих категорий, если оно равно нулю то поле с категориями подсвечивается
    $('#listValue').bind("DOMSubtreeModified", function () {

        var categories = document.getElementById('listValue');

        if ($('.itemList').length == 0) {

            categories.classList.add('error');
            $('#btnDropCategory').addClass('error');
        }
        else {

            categories.classList.remove('error');
            $('#btnDropCategory').removeClass('error');

        }

    });

    // Проверка заполнения поля "Название"
    $('#Title').focusout(function () {

        var title = $('#Title');

        if (title.val() == '') {

            title.addClass('error');
        }
        else {

            title.removeClass('error');
        }

    });

    // Проверка заполнения поля "Автор"
    $('#Author').focusout(function () {

        var author = $('#Author');

        if (author.val() == '') {

            author.addClass('error');
        }
        else {

            author.removeClass('error');
        }

    });

    // Проверка заполнения поля "Количество"
    $('#Count').focusout(function () {

        var count = $('#Count');

        if (count.val() == '' || count.val() < 1) {

            count.addClass('error');
        }
        else {

            if (count.val() < 1 || count.val() > 10) {

                alert("Количество книг должно быть от 1 до 10");
                count.addClass('error');

            }
            else {

                count.removeClass('error');

            }
            
        }

    });

    // Проверка заполнения поля "Количество страниц"
    $('#CountPages').focusout(function () {

        var countPages = $('#CountPages');

        if (countPages.val() == '' || countPages.val() < 0) {

            countPages.addClass('error');

        }
        else {

            countPages.removeClass('error');

        }

    });

    // Проверка заполнения поля "Год публикации"
    $('#YearOfPublication').focusout(function () {

        var year = $('#YearOfPublication');

        if (year.val() == '' || year.val() < 1) {

            year.addClass('error');
        }
        else {

            year.removeClass('error');
        }

    });

    // Проверка заполнения поля "Описание"
    $('#Description').focusout(function () {

        var description = $('#Description');

        if (description.val() == '') {

            description.addClass('error');
        }
        else {

            description.removeClass('error');
        }

    });

    // Проверка заполнения полей "Ключевые слова"
    $(document.body).on("focusout", ".keywords", function () {

        var keyword = this;

        if (keyword.value == '') {

            keyword.classList.add('error');
        }
        else {

            keyword.classList.remove('error');
        }

    });



    // Проверка заполнения поля "Количество"
    $('.CountEdit').focusout(function () {

        var aviable = Number($('#Aviable').val());
        var count = $('#Count').val();
        var prevCount = $('#PrevCount').val();

        if (((count - prevCount) + aviable) < 0) {

            $('#Count').addClass('error');

            $('#hInfo').html("Общее количество книг не может быть равно " + count + ", т.к. " + aviable + " книг находятся в пользовании");
            $('#ModalInfo').modal('show');

        }
        else {

            $('#Count').removeClass('error');

        }

    });

    // Проверка заполнения поля "Обложка"
    $('#Cover').focusout(function () {

        var cover = $('#Cover');
        var currentCover = $('#img');

        if (currentCover.length == 0) {

            if (cover.val() == '') {

                cover.addClass('error');
            }
            else {

                cover.removeClass('error');
            }

        }

    });

    // заполнения поля ключевого слова, выбранным из списка значением
    $(document.body).on("click", ".keywordItemDrop", function () {

        var keyw = document.activeElement;
        var idhid = null;
        var idinp = null;
        var hid = null;
        var inp = null;

        idhid = "hidden" + localStorage.getItem("selectedKeyId").substring(8, 9);
        idinp = "key" + localStorage.getItem("selectedKeyId").substring(8, 9);

        hid = document.getElementById(idhid);
        inp = document.getElementById(idinp);

        inp.value = keyw.text;
        hid.value = keyw.text;

        localStorage.removeItem("selectedKeyId");
        $('.dropdown-menu').removeClass('show');

    });

    // При клике по странице закрывается список
    $(document).click(function () {

        $('#drop').remove();

    });

});