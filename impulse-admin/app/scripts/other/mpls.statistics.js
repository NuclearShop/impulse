var Click = function(){ }
Click.prototype = {
    ClickZone:'',
    ClickTime: new Date(),
    ClickType: 0,
    ClickCurrentStage: 0,
    ClickNextStage: 0,
    ClickNextTime: 0
}

var Activity = function () { }
Activity.prototype = {
    StartTime: new Date(),
    EndTime: new Date(),
    CurrentStateName: '',
    Clicks: new Array()
}
var AdSession = function () { }
AdSession.prototype = {
    CurrentActivity: new Activity(),
    Activities: new Array(),
    AdId:0,
    DateTimeStart: new Date(),
    DateTimeEnd: new Date(),
    UserBrowser: '',
    AbTestId: null
}

var getBrowserName = function () {
    if (bowser.msie && bowser.version <= 6) {
        return 'IE';
    } else if (bowser.firefox) {
        return 'Firefox';
    } else if (bowser.chrome) {
        return 'Chrome';
    } else if (bowser.safari) {
        return 'Safari';
    } else if (bowser.opera) {
        return 'Opera';
    } else if (bowser.iphone || bowser.android) {
        return 'Mobile';
    }
}

var captureStatistics = function (adId, AbTestId) {
    var jsModel = new AdSession();
    jsModel.Activities = new Array();
    jsModel.CurrentActivity = new Activity();
    /* act.CurrentStateName = $('.mpls-current').data('name');
        act.StartTime = new Date();*/
    jsModel.AdId = adId;
    jsModel.DateTimeStart = new Date();
    jsModel.DateTimeEnd = new Date();
    jsModel.UserBrowser = getBrowserName();
    $(".mpls-action-button").addClass('mpls-tracked');

    $(".mpls-tracked").click(function () {
        var button = $(this);
        if(jsModel.CurrentActivity!=null) {
            var click = new Click();
            click.ClickTime = new Date();
            click.ClickZone = $(this).parent().class;
            click.ClickCurrentStage = button.data('current-id');
            click.ClickNextStage = button.data('next-id');
            click.ClickNextTime = button.data('next-time');
            click.ClickType = button.data('action');
            click.ClickZone = button.attr('class');
            jsModel.CurrentActivity.Clicks[jsModel.CurrentActivity.Clicks.length] = click;
        }
            
    });

    document.addEventListener("mpls-slide-end", function () {
        if (jsModel.CurrentActivity != null) {
            jsModel.CurrentActivity.EndTime = new Date();
            jsModel.Activities[jsModel.Activities.length] = jsModel.CurrentActivity;
            jsModel.CurrentActivity = null;
        }
    });

    document.addEventListener("mpls-slide-start", function () {
        var act = new Activity();
        act.CurrentStateName = $('.mpls-current').data('name');
        act.StartTime = new Date();
        act.Clicks = new Array();
        jsModel.CurrentActivity = act;
    });

    document.addEventListener("mpls-ad-start", function () {
        var act = new Activity();
        act.CurrentStateName = $('.mpls-current').data('name');
        act.StartTime = new Date();
        act.Clicks = new Array();
        jsModel.CurrentActivity = act;
    });

    window.onbeforeunload = function () {
        jsModel.DateTimeEnd = new Date();
        if (jsModel.CurrentActivity != null) {
            jsModel.CurrentActivity.EndTime = new Date();
            jsModel.Activities[jsModel.Activities.length] = jsModel.CurrentActivity;
            jsModel.CurrentActivity = null;
        }
        if (AbTestId && AbTestId > 0) {
            jsModel.AbTestId = AbTestId;
        }
        var jsonModel = JSON.stringify(jsModel);
        $.ajax({
            url: '/api/stat/session',
            method: 'POST',
            data: jsonModel,
            contentType: "application/json",
            success: function (data, status) {
            }
        })
    }
}