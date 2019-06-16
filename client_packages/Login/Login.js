$('#loginButton').click(() => {

    $('.alert').remove(); //Remove any alerts when we attempt to login/register
    mp.trigger('loginInformationToServer', $('#loginUsernameText').val(), $('#loginPasswordText').val());
});
