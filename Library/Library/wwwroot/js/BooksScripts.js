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
        else {

            name = null;

        }

        // Выбираем атрибут поиска книги "Категория" (если он было указано)
        if (localStorage.getItem("SearchCategory") != 'undefined') {

            category = JSON.parse(localStorage.getItem("SearchCategory"));
            $('#category').val(category);

        }
        else {

            category = 0;

        }

    }

    // Выбираем количество книг на одной странице (если оно было указано)
    if (localStorage.getItem("CountItems") != 'undefined') {


        countItems = JSON.parse(localStorage.getItem("CountItems"));
        if (countItems == '1') {
            $('#blockView1').removeClass('hidden');
        }
        if (countItems == '4') {
            $('#blockView4').removeClass('hidden');
        }
        if (countItems == '8') {
            $('#blockView8').removeClass('hidden');
        }

    }
    else {

        $('#blockView4').removeClass('hidden');

    }

    if (page == '') {
        page = 1;
    }

    // При выборе вида страницы делаем запрос на вывод книг по указанным критериям
    $('#view1').click(function () {

        getBooks(page, category, name, 1);
        $('#blockView1').removeClass('hidden');
        $('#blockView4').addClass('hidden');
        $('#blockView8').addClass('hidden');

    });

    // При выборе вида страницы делаем запрос на вывод книг по указанным критериям
    $('#view4').click(function () {

        getBooks(page, category, name, 4);
        $('#blockView1').addClass('hidden');
        $('#blockView4').removeClass('hidden');
        $('#blockView8').addClass('hidden');

    });

    // При выборе вида страницы делаем запрос на вывод книг по указанным критериям
    $('#view8').click(function () {

        getBooks(page, category, name, 8);
        $('#blockView1').addClass('hidden');
        $('#blockView4').addClass('hidden');
        $('#blockView8').removeClass('hidden');

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