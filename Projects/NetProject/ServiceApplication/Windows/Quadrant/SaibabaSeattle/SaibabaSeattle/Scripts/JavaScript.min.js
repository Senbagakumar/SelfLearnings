function guid() {
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
        s4() + '-' + s4() + s4() + s4();
}

function s4() {
    return Math.floor((1 + Math.random()) * 0x10000)
        .toString(16)
        .substring(1);
}

var userId = guid().toUpperCase();
var userName = 'User-' + Math.floor((1 + Math.random()) * 10000);

var user = {
    id: id,
    name: userName
};

var bot = {
    id: 'saibabatempleassistance',
    name: 'saibabatempleassistance'
};


var botConnection = new BotChat.DirectLine({
    token: t,
    webSocket: true
});

console.log("Init bot component");

BotChat.App({
    botConnection: botConnection,
    user: user,
    bot: bot,
    resize: 'detect',
    chatTitle: 'SaiBabaTempleBot',
    conversation: { "id": cid }
}, document.getElementById("bot"));

if (f == 1) {
    //botConnection.postActivity({ type: "event", from: user, name: "firstMessage", value: "ping" }).subscribe(id => console.log("Conversation updated"));
    botConnection.postActivity({ type: "event", from: user, name: "firstMessage", value: "ping" }).subscribe();
}
$(function () {
    $("div.wc-header").prop("isopen", "true");

    $("div.wc-header").click(function () {
        var isopen = $(this).prop("isopen");
        //alert(isopen);
        if (isopen == "true") {
            $("div.wc-chatview-panel").addClass("closechat");
            $("div.wc-header").prop("isopen", "false");
        } else {
            $("div.wc-chatview-panel.closechat").removeClass("closechat");
            $("div.wc-header").prop("isopen", "true");
        }
    });
});