
$(document).ready(function () {
    if (!("Notification" in window)) {
        alert('Ваш браузер не поддерживает HTML Notifications.');
    }
    else if (Notification.permission !== 'denied') {
        Notification.requestPermission();
    }

    var timer = $('.Time')[0];
    if (timer !== undefined) {
        var time = timer.dataset.container;
        time = parseInt(time);
        startTimer(timer, time);
    }
});

function startTimer(timer, time) {

    if (time < 0) time = 0;
    console.log(time);
    this.finalDate = new Date((new Date()).getTime() + time);
    refreshTimer(timer);
}
function refreshTimer(timer) {
    date = this.finalDate;
    var s = date.getTime() - (new Date()).getTime();
    s = parseInt(s / 1000);

    var h = parseInt(s / 3600);
    s -= h * 3600;
    var m = parseInt(s / 60);
    s -= m * 60;
    timer.innerHTML = ('0' + m).slice(-2) + ':' + ('0' + s).slice(-2);
    if (date.getTime() > Date.now()) setTimeout(refreshTimer, 1000, timer);
    else completePomidor();
}

function sendNotification(title, options) {

    if (!("Notification" in window)) {
        return;
    }

    else if (Notification.permission === "granted") {
        var notification = new Notification(title, options);
        //function clickFunc() { alert('Пользователь кликнул на уведомление'); }
        //notification.onclick = clickFunc;
    }

    else if (Notification.permission !== 'denied') {
        Notification.requestPermission(function (permission) {
            // Если права успешно получены, отправляем уведомление
            if (permission === "granted") {
                var notification = new Notification(title, options);
            }
        });
    }
}

function start() {
    $.post('/Home/Start', function (data) {
        var timer = $('.Time')[0];
        if (timer !== undefined) {
            
            startTimer(timer, data.rest);
        }
    });
}

function completePomidor() {
    $.post('/Home/End', function (data) {
        if (data.isSuccesfully) {
            $('#money').text(data.money);
            $('#experience').text(data.experience);
            notifyOnCompletion();
            updateHeroSummary();
            updatePrimaryPet();
        }
    });
}

function notifyOnCompletion() {
    sendNotification('Помидорка', {
        body: 'Таймер окончен',
        icon: 'icon.jpg',
        dir: 'auto'
    });
    $('#CompletionForm').modal('show');
}

function updateHeroSummary() {
    $.ajax({
        type: "GET",
        url: '/Home/HeroSummary'
    }).done(function (result) {
        $("#heroSummary").html(result);
    });
}

function updatePrimaryPet() {
    $.ajax({
        type: "GET",
        url: '/Home/PrimaryPet'
    }).done(function (result) {
        $("#primaryPet").html(result);
    });
}