(function () {

    var div = document.createElement("div");
    document.getElementsByTagName('body')[0].appendChild(div);
    div.outerHTML = "<div id='botDiv' style='height: 38px; position: fixed; bottom: 0; z-index: 1000; background-color: #fff'><div id='botTitleBar' style='height: 38px; width: 400px; position:fixed; cursor: pointer;'></div><iframe width='500px' height='600px' src='https://webchat.botframework.com/embed/saibabatempleassistance?s=l_11_un7HKk.j7SiuuYH6s1kEjswUyYca55tECp4qciHCtIkO3LrGqU'></iframe></div>";
    document.querySelector('body').addEventListener('click', function (e) {
        e.target.matches = e.target.matches || e.target.msMatchesSelector;
        if (e.target.matches('#botTitleBar')) {
            var botDiv = document.querySelector('#botDiv');
            botDiv.style.height = botDiv.style.height == '600px' ? '38px' : '600px';
        };
    });
}());