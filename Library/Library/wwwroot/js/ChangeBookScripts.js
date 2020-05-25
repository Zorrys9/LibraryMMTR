/// Изменение книг


$(document).ready(function () {

    window.onbeforeunload = function (evt) {

        var filled = false;

        var controls = $('.ControlCreateForm');

        for (var i = 0; i < controls.length; i++) {

            if (controls[i].value != '' && controls[i].value != "undefined") {

                filled = true;

            }

        }
        var categories = $('.itemList');
        if (categories.length != 0) {

            filled = true;

        }

        if (filled == true) {

            var message = "Document 'foo' is not saved. You will lost the changes if you leave the page.";
            if (typeof evt == "undefined") {
                evt = window.event;
            }
            if (evt) {
                evt.returnValue = message;
            }
            return message;

        }
        else {

            return null;

        }
    }

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

    $('#Count').inputmask({ "mask": "99", placeholder:"" }); 
    $('#CountPages').inputmask({ "mask": "99999", placeholder:"" }); 
    $('#YearOfPublication').inputmask({ "mask": "9999", placeholder:"" }); 

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
            $('#CategoryError').removeClass('hidden');

        }
        else {

            categories.classList.remove('error');
            $('#btnDropCategory').removeClass('error');
            $('#CategoryError').addClass('hidden');

        }

    });

    // Проверка заполнения поля "Название"
    $('#Title').focusout(function () {

        var title = $('#Title');

        if (title.val() == '') {

            title.addClass('error');
            $('#TitleError').removeClass('hidden');

        }
        else {

            title.removeClass('error');
            $('#TitleError').addClass('hidden');

        }

    });

    // Проверка заполнения поля "Автор"
    $('#Author').focusout(function () {

        var author = $('#Author');

        if (author.val() == '') {

            author.addClass('error');
            $('#AuthorError').removeClass('hidden');

        }
        else {

            author.removeClass('error');
            $('#AuthorError').addClass('hidden');

        }

    });

    // Проверка заполнения поля "Количество"
    $('#Count').focusout(function () {

        var count = $('#Count');

        if (count.val() == '' || count.val() < 1) {

            count.addClass('error');
            $('#CountError').removeClass('hidden');

        }
        else {

            $('#CountError').addClass('hidden');

            if (count.val() < 1 || count.val() > 10) {

                $('#CountBooksError').removeClass('hidden');
                count.addClass('error');

            }
            else {

                $('#CountBooksError').addClass('hidden');
                count.removeClass('error');
                
            }
            
        }

    });

    // Проверка заполнения поля "Количество страниц"
    $('#CountPages').focusout(function () {

        var countPages = $('#CountPages');

        if (countPages.val() == '' || countPages.val() < 0) {

            countPages.addClass('error');
            $('#CountPagesError').removeClass('hidden');

        }
        else {

            countPages.removeClass('error');
            $('#CountPagesError').addClass('hidden');

        }

    });

    // Проверка ссылки на валидность
    $('#URL').focusout(function () {
        //\.[A-Za-z]{2,3}
        var u = /http(s?):\/\/[-\w\.]{3,}/;
        var url = this;

        if (!u.test(url.value) && url.value != '') {

            url.classList.add('error');
            $('#errorURL').removeClass('hidden');

        }
        else {

            url.classList.remove('error');
            $('#errorURL').addClass('hidden');
        }

    })

    // Проверка заполнения поля "Год публикации"
    $('#YearOfPublication').focusout(function () {

        var year = $('#YearOfPublication');

        if (year.val() == '' || year.val() < 1) {

            year.addClass('error');
            $('#YearOfPublicationError').removeClass('hidden');

        }
        else {

            year.removeClass('error');
            $('#YearOfPublicationError').addClass('hidden');

            var date = new Date().getFullYear();

            if (date < year.val()) {

                $('#YearHightError').removeClass('hidden');

            }
            else {

                $('#YearHightError').addClass('hidden');

            }


        }

    });

    // Проверка заполнения поля "Описание"
    $('#Description').focusout(function () {

        var description = $('#Description');

        if (description.val() == '') {

            description.addClass('error');
            $('#DescriptionError').removeClass('hidden');

        }
        else {

            description.removeClass('error');
            $('#DescriptionError').addClass('hidden');

        }

    });

    // Проверка заполнения полей "Ключевые слова"
    $(document.body).on("focusout", ".keywords", function () {

        var keyword = this;
        var ErrorId = "#error" + keyword.id.substring(4, 3);

        if (keyword.value == '') {

            keyword.classList.add('error');
            $(ErrorId).removeClass('hidden');

        }
        else {

            keyword.classList.remove('error');
            $(ErrorId).addClass('hidden');
        }

    });



    // Проверка заполнения поля "Количество"
    $('.CountEdit').focusout(function () {

        var aviable = Number($('#Aviable').val());
        var count = $('#Count');
        var prevCount = $('#PrevCount').val();

        $('#CountError').addClass('hidden');

        if (count.val() < 1 || count.val() > 10) {

            $('#CountBooksError').removeClass('hidden');
            count.addClass('error');

        }
        else {

            $('#CountBooksError').addClass('hidden');
            count.removeClass('error');

            if (((count.val() - prevCount) + aviable) < 0) {

                count.addClass('error');

                $('#hInfo').html("Общее количество книг не может быть равно " + count.val() + ", т.к. " + aviable + " книг находятся в пользовании");
                $('#ModalInfo').modal('show');

            }
            else {

                $('#Count').removeClass('error');

            }

        }

    });

    $("#Cover").on('change', function () {

        var cover = $('#Cover');
        var currentCover = $('#img');

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
      
    });

    // Проверка заполнения поля "Обложка"
    $('#Cover').blur(function () {

        var cover = $('#Cover');
        var currentCover = $('#img');

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