$(document).ready(function () {

    var rooms = [];
    var chat = $.connection.chat;
    $.connection.hub.start();

    chat.client.chatWith = chatWith;
    chat.client.showError = showError;
    chat.client.addMessage = addMessage;
    
    $('#send-message').click(function () {

        var msg = $('#message').val();

        chat.server.sendMessage(msg);
    });

    $("#join-room").click(function () {

        var room = $('#room').val();

        chat.server.joinRoom(room)
    });

    $('#send-message-to-room').click(function () {

        var msg = $('#room-message').val();

        chat.server.sendMessageToRoom(msg, rooms);
    });

    $('#submit-user').click(function (ev) {
        var previousUsername = $('#chat-panel').attr("data-username");
        if (previousUsername) {
            chat.server.disconnect(previousUsername);
        }

        var username = $('#username').val();
        chat.server.checkAndConnect(username);
    });

    $('#msg-btn').click(function (ev) {
        var message = $('#msg-input').val();
        var username = $('#chat-panel').attr('data-username');
        chat.server.sendMessage(username, message);
        var encodedMessage = $('<div>').text(message).html();

        var element = '<li class="right clearfix">' +
            '<div class="chat-body clearfix">' +
            `<div class="header"><strong class="pull-right primary-font">${username}</strong></div>` +
            `<p>${encodedMessage}</p>`
        '</div>'
        '</li>';

        $('#messages').append(element);
    })

});

function chatWith(user) {
    $('#error-msg').text('');

    const panel = $('#chat-panel');
    panel.removeClass('display-none');
    panel.attr('data-username', user);

    $("#messages").html("");
}

function showError() {
    const errorMessage = $('#error-msg');
    errorMessage.text('The user does not exist.');
}

function addMessage(username, message) {
    const element = '<li class="left clearfix">' +
        '<div class="chat-body clearfix">' +
        `<div class="header"><strong class="primary-font">${username}</strong></div>` +
        `<p>${message}</p>` +
        '</div>' +
        '</li>';

    $('#messages').append(element);
}

function joinRoom(room) {
    rooms.push(room);
    $('#currentRooms').append('<div>' + room + '</div>');
}