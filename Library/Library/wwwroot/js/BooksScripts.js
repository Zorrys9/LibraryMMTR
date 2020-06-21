/// Работа со страницей списка книг


$(document).ready(function () {

    var page = document.location.search.substr(6);
    var name = null;
    var category = 0;
    var countItems = 4;
    var currentPage = document.location.href;
    var prevPage = document.referrer;
    // Если текущая страница равная предыдущей то...

    if (currentPage.substr(0, currentPage.length) == prevPage.substr(0,currentPage.length)) {

        
        name = getSearchName();
        category = getSearchCategory();
        

    }

    $('#blockView4').removeClass('hidden');

    countItems = getCountItems();

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



        hubConnection.on("RefreshList", function () {

        var category = getSearchCategory();
        var name = getSearchName();
        var countItems = getCountItems();
        var page = document.location.search.substr(6);

        if (page == '') {

            page = 1;

        }

        if (countItems == 0) {

            countItems == 4

        }

        getBooks(page, category, name, countItems);

    });




});