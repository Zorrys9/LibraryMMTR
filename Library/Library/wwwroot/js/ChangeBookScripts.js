/// Изменение книг


$(document).ready(function () {

    // При клике по категории "Разработка" добавляем в блок с текущими категориями
    $('#1').click(function () {

        var div = createDiv("Разработка", "1");

        $('#listValue').append(div);
        $('#1').addClass("hidden");
        $('#listValue').append(createAddIcon());
        $('form#CreateForm').append('<input type="hidden" class="category" id="Development" asp-for="IdCategories" name="IdCategories" value="1" />');

    });

    // При клике по категории "Аналитика" добавляем в блок с текущими категориями
    $('#2').click(function () {

        var div = createDiv("Аналитика", "2");

        $('#plus').remove();
        $('#listValue').append(div);
        $('#2').addClass("hidden");
        $('#listValue').append(createAddIcon());
        $('form#CreateForm').append('<input type="hidden" class="category" id="Analytics" asp-for="IdCategories" name="IdCategories" value="2" />');

    });

    // При клике по категории "Тестирование" добавляем в блок с текущими категориями
    $('#3').click(function () {

        var div = createDiv("Тестирование", "3");

        $('#listValue').append(div);
        $('#3').addClass("hidden");
        $('#listValue').append(createAddIcon());
        $('form#CreateForm').append('<input type="hidden" class="category" id="Testing" asp-for="IdCategories" name="IdCategories" value="3" />');

    });

    // При клике по категории "Сопровождение" добавляем в блок с текущими категориями
    $('#4').click(function () {

        var div = createDiv("Сопровождение", "4");

        $('#listValue').append(div);
        $('#4').addClass("hidden");
        $('#listValue').append(createAddIcon());
        $('form#CreateForm').append('<input type="hidden" class="category" id="Maintenance" asp-for="IdCategories" name="IdCategories" value="4" />');

    });

    // При клике по категории "Управление" добавляем в блок с текущими категориями
    $('#5').click(function () {

        var div = createDiv("Управление", "5");

        $('#listValue').append(div);
        $('#5').addClass("hidden");
        $('#listValue').append(createAddIcon());
        $('form#CreateForm').append('<input type="hidden" class="category" id="Management" asp-for="IdCategories" name="IdCategories" value="5" />');

    });

    // При клике по категории в списке она в нем скрывается
    $('a.dropdown-item').click(function () {

        $('.dropdown-menu-left').removeClass('show');

    });

    // При изменении поля с ключевыми словами изменяем скрытое поле для будущей отправки в БД
    $(document.body).on("change", '.keywords', function () {

        var keywords = document.getElementsByClassName('keywords');

        for (var i = 0; i < keywords.length; i++) {
            var keyword = $('#hidden' + keywords[i].id);
            keyword.val(keywords[i].value);
        }

    });

    // При клике по странице закрывается список
    $(document).click(function () {

        $('#drop').remove();

    });

});