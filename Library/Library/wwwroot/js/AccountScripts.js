/// Работа со страницами авторизации и регистрации

$(document).ready(function () {

    $('#phone').mask('+7 (000) 000-00-00');

	$('#email').inputmask(
		{
			mask: "*{3,40}@*{3,15}.*{1,7}",
			greedy: false,
			clearMaskOnLostFocus: false
		});

	// Проверка всех полей на наличие каких-либо данных
	$('.form-control').blur(function () {

		var text = this.value;

		if (text == ' ' || text == '') {

			this.classList.add('error');

		}
		else {

			this.classList.remove('error');

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


