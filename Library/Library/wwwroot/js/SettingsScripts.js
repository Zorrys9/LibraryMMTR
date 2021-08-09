
$(document).ready(function () {

    $('#NewEmail').inputmask(
        {
            mask: "*{3,40}@-{3,15}.-{1,5}",
            greedy: !1,
            casing: "lower",
            onBeforePaste: function onBeforePaste(pastedValue, opts) {

                return pastedValue = pastedValue.toLowerCase(), pastedValue.replace("mailto:", "");

            },
            definitions: {
                "*": {

                    validator: "[0-9\uff11-\uff19A-Za-z\u0410-\u044f\u0401\u0451\xc0-\xff\xb5!#$%&'*+/=?^_.`{|}~-]"

                },
                "-": {

                    validator: "[0-9A-Za-z-]"

                }
            }
        });

    $('.btn-dark').click(function () {

        var id = "#" + this.id + "Box";

        $(this).parent().children(id).slideToggle(300);

        return false;

    });

    $("#UseSSL").click(function () {

        var check = $("#UseSSL");

        if (check.val() == "true") {

            check.val(false);

        }
        else {

            check.val(true);

        }

    })

    $('#cancelChangeSettings').click(function () {

        document.location.href = document.referrer;

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
