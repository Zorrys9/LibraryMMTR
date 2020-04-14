/// Работа со страницей списка книг


$(document).ready(function () {

    var page = document.location.search.substr(6);
    var name = null;
    var category = 0;
    var countItems = 4;

    // Если текущая страница равная предыдущей то...
    if (document.location.href == document.referrer) {

        // Выбираем атрибут поиска книги "Название" (если он было указано)
        if (localStorage.getItem("SearchName") != 'undefined') {

            name = JSON.parse(localStorage.getItem("SearchName"));
            $('#name').val(name);

        }

        // Выбираем атрибут поиска книги "Категория" (если он было указано)
        if (localStorage.getItem("SearchCategory") != 'undefined') {

            category = JSON.parse(localStorage.getItem("SearchCategory"));
            $('#category').val(category);

        }

    }

    // Выбираем количество книг на одной странице (если оно было указано)
    if (localStorage.getItem("CountItems") != 'undefined') {

        countItems = JSON.parse(localStorage.getItem("CountItems"));
        $('#ViewSelector').val(countItems);

    }

    if (page == '') {
        page = 1;
    }

    // При выборе вида страницы делаем запрос на вывод книг по указанным критериям
    $('#ViewSelector').change(function () {

        countItems = $('#ViewSelector').val();
        getBooks(page, category, name, countItems);

    });

    // При нажатии на кнопку поиска делаем запрос на вывод книг по указанным критериям
    $('#search').click(function () {

        name = $('#name').val();
        category = $('#category').val();

        getBooks(page, category, name, countItems);

    })

    // При загрузке книги идет запрос на вывод книг
    getBooks(page, category, name, countItems);

});