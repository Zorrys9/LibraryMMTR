/// Работа со страницами авторизации и регистрации

$(document).ready(function () {

    $('#phone').mask('+7 (000) 000-00-00');

	$('#email').inputmask(
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

	// Проверка всех полей на наличие каких-либо данных
	$('.form-control').blur(function () {

		var text = this.value;
		var idError = "#"+this.id + "Error";

		if (text == ' ' || text == '') {

			this.classList.add('error');
			$(idError).removeClass('hidden');
		
		}
		else {

			this.classList.remove('error');
			$(idError).addClass('hidden');

        }

	});

	// Проверка пароля по всем критериям после заполнения этого поля
	$('#password').blur(function () {

		checkPassword();

		var password = $('#password').val();
		var confirm = this.value;

		if (password != confirm) {

			this.classList.add('error');
			$('#confirmedError').removeClass('hidden');

		}
		else {

			this.classList.remove('error');
			$('#confirmedError').addClass('hidden');
		}


	});

	// Проверка повторного введения пароля
	$('#confirmPassword').blur(function () {

		var password = $('#password').val();
		var confirm = this.value;

		if (password != confirm) {

			this.classList.add('error');
			$('#confirmedError').removeClass('hidden');

		}
		else {

			this.classList.remove('error');
			$('#confirmedError').addClass('hidden');
		}

    })

})


