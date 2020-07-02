/// Работа со списком книг



$(document).ready(function () {


    var stringHeights = getStyle('text-Desc', 'font-size');
    var elm = document.getElementsByClassName('text-Desc');
    var lines = 0;
    var page = document.location.search.substr(6);

    // Для каждого описания книги применяется ограничение в 3 строки, если описание > 3 строк то оно обрезается и появляется ссылка "Подробнее"
    for (var i = 0; i < elm.length; i++) {

        var heightBlock = elm[i].clientHeight;
        lines = heightBlock / stringHeights[i];

        if (lines > 3) {

            var url = document.getElementById("more" + i);

            elm[i].classList.add('hiddenText');
            url.classList.remove('hidden');
            url.classList.add('visible');

        }

    }

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

