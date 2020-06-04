
$(document).ready(function () {

    $('.btn-dark').click(function () {

        var id = "#" + this.id + "Box";

        $(this).parent().children(id).slideToggle(300);

        return false;

    });

    $('.btn-primary').click(function () {

        var id = "#" + this.id.replace("Show", "");

        $(id).removeClass('hidden');
        this.classList.add('hidden');

    })

    $('.btn-link').click(function () {

        var id = "#" + this.id.replace("Hide", "");

        $(id).addClass('hidden');
        $(id + "Show").removeClass('hidden');

    })

    $(document.body).on("blur", ".newValue", function () {

        var input = $("#"+this.id);

        checkValue(input);

        if (input.hasClass('error')) {

            $("#" + this.id + "EmptyError").removeClass('hidden');
            $("#Change" + this.id.replace("New", "")).addClass('hidden');
        }
        else {

            $("#" + this.id + "EmptyError").addClass('hidden');
            $("#Change" + this.id.replace("New", "")).removeClass('hidden');

        }

    })
    
})
