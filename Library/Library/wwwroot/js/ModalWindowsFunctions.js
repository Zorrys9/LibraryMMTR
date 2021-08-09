/// функции работы с модальными окнами системы





function getModalDialog(text) {

    $('#h').html(text);
    $('#ModalDialog').modal('show');
    $('#ModalDialog').addClass('opened');

}

function hideModalDialog() {

    $('#h').html('');
    $('#ModalDialog').modal('hide');

}

function getModalInfo(text) {

    $('#hInfo').html(text);
    $('#ModalInfo').modal('show');

}

function hideModalInfo() {

    $('#ModalInfo').modal('hide');

}


function getConfirmReturn() {

    $('#ConfirmReturn').modal('show');

}

function hideConfirmReturn() {

    $('#ConfirmReturn').modal('hide');

}

function createConfirmRaitingModal(score) {

    if (score == 0) {

        $('#RaitingH').text('Вы успешно вернули книгу. Ваша оценка книге : ');
        $('#raitingModal').removeClass('hidden');

    }
    else {

        $('#RaitingH').text('Вы успешно вернули книгу.');

    }

}

function getConfirmRaitingReturnedModal() {

    $('#ConfirmRaitingReturned').modal('show');
    $('#ConfirmRaitingReturned').addClass('opened');

}

function hideConfirmRaitingReturnedModal() {

    $('#ConfirmRaitingReturned').modal('hide');

}

function getConfirmRaiting() {

    $('#ConfirmRaiting').modal('show');

}

function hideConfirmRaiting() {

    $('#ConfirmRaiting').modal('hide');

}

function getConfirmLogOut() {

    $('#ConfirmLogOut').modal('show');

}

function hideConfirmLogOut() {

    $('#ConfirmLogOut').modal('hide');

}

function getErrorInfo(text) {

    $('#ErrorH').html(text);
    $('#ErrorInfo').modal('show');

}

function hideErrorInfo() {

    $('#ErrorInfo').modal('hide');

}

function getConfirmDelete() {

    $('#ConfirmDelete').modal('show');

}

function hideConfirmDelete() {

    $('#ConfirmDelete').modal('hide');

}