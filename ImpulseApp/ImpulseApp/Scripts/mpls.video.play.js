var videoLength = 0;
var video = null;
var curPlayingVideoId = null;
var currentVideoInfo = null;
var startSlideEvent = new Event("mpls-slide-start");
var endSlideEvent = new Event("mpls-slide-end");
var startAdEvent = new Event("mpls-ad-start");
var endAdEvent = new Event("mpls-ad-end");
var elementsToAppear = [];
var elementsToDisappear = [];
var videos = [];
var initCss = function (currentId) {
    $(".mpls-video").css('position', 'absolute');
    $(".mpls-video").css('visibility', 'hidden');
    $(".mpls-video").css('left', '0');
    $(".mpls-video").css('top', '0');
    $(".mpls-video-control").css('position', 'absolute');
    $(".mpls-video-control").css('z-index', '1');
    $(".mpls-video-control").css('visibility', 'hidden');
    $(".mpls-ue").css('visibility', 'hidden');
    $(".mpls-action-button").css('cursor', 'pointer');
    $(".mpls-action-button").css('background-color', 'white');
    $(".mpls-action-button").css('color', 'black');
    $(".mpls-video").removeClass('mpls-current');
    
    if (currentId === undefined) {
        $(".mpls-start-video").css('visibility', 'visible');
        $(".mpls-video-start-control").css('visibility', 'visible');
        $(".mpls-start-video").addClass('mpls-current');
    }
    else {
        $("#mpls-video-" + currentId).css('visibility', 'visible');
        $("#mpls-video-" + currentId).addClass('mpls-current');
        var videoControl = $("#mpls-container").find("[data-for='mpls-video-" + curPlayingVideoId + "']");
        videoControl.css("visibility", "visible");
    }
}
//Тут эффект для остановки видео
var initCssEffects = function (currentId) {
    if (currentId) {
        $("#mpls-video-" + currentId).css('-webkit-filter', 'blur(10px)');
    }

}
var removeCssEffects = function (currentId) {
    if (currentId) {
        $("#mpls-video-" + currentId).css('-webkit-filter', '');
    }
}

var getVideoInfo = function (initStatsListener, adId, adUrl, abTestId) {
    jQuery.support.cors = true;
    $.getJSON('/api/ad', {
        url: adUrl
    }).done(function (data) {
        videos = data.AdStates;
        appendHtmlToDiv(data);
        initStatsListener(adId, abTestId);
        initCss();
        curPlayingVideoId = $(".mpls-start-video").data('id');
        video = document.getElementById("mpls-video-" + curPlayingVideoId);
        currentVideoInfo = getCurrentVideoInfo(videos, curPlayingVideoId);
        videoLength = video.duration.toFixed(1);
        reinitListeners(video);

        $(".mpls-action-button").click(function () {
            var button = $(this);
            var action = button.data("action");
            if (action === "next-slide") {
                document.dispatchEvent(endSlideEvent);
                var nextSlide = button.data("next-id");
                var nextTime = button.data("next-time");
                goToSlide(nextTime, nextSlide)
            } else if (action === 'start') {
                document.dispatchEvent(startAdEvent);
                initCss(curPlayingVideoId);
                updateElementsAppearness();
                video.play();
                $(".mpls-video-start-control").css('visibility', 'hidden');
            }
        })
    }).error(function (data) {
        console.log(data);
    });
}

var goToSlide = function (nextTime, nextSlide) {
    removeCssEffects(curPlayingVideoId);
    curPlayingVideoId = nextSlide;
    removeListeners(video);
    video.pause();
    video = document.getElementById("mpls-video-" + curPlayingVideoId);
    currentVideoInfo = getCurrentVideoInfo(videos, curPlayingVideoId);
    updateElementsAppearness();
    initCss(curPlayingVideoId);
    reinitListeners(video);
    video.currentTime = nextTime;
    video.play();
    document.dispatchEvent(startSlideEvent);
}
var getCurrentVideoInfo = function (array, id) {
    return _.findWhere(array, { VideoUnitId: id });
}

var updateElementsAppearness = function () {
    elementsToAppear = [];
    elementsToDisappear = [];
    var controls = currentVideoInfo.UserElements;
    elementsToAppear = _.groupBy(controls, function (c) {
        return c.TimeAppear;
    });
    elementsToDisappear = _.groupBy(controls, function (c) {
        return c.TimeDisappear;
    });
}

var getElementsByAppearTime = function (time) {
    var result = [];
    result = elementsToAppear[time];
    
    if (!result || result.length === 0) {
        return result;
    }
    var res = _.pluck(result, 'Id');
    return res;
}

var getElementsByDisppearTime = function (time) {
    var result = [];
    result = elementsToDisappear[time];
    if (!result || result.length === 0) {
        return result;
    }
    var res = _.pluck(result, 'Id');
    
    return res;
}

var timeUpdateListener = function () {
    var videoTime = video.currentTime.toFixed(1);
    var endTime = videoLength * 0.9;
    
    
    if (!currentVideoInfo.IsFullPlay) {
        endTime = currentVideoInfo.EndTime;
    }
    var vt = Math.floor(videoTime);
    if (vt - videoTime === 0) {
        var elemsToAppear = getElementsByAppearTime(vt);
        var elemsToDisappear = getElementsByDisppearTime(vt);
        _.each(elemsToAppear, function (id) {
            console.log(id);
            $('.mpls-ue-' + id).css("visibility", "visible");
        })
        _.each(elemsToDisappear, function (id) {
            $('.mpls-ue-' + id).css("visibility", "hidden");
        })
    }
    if (videoTime > endTime) {
        video.pause();
        initCssEffects(curPlayingVideoId);
        var elemsToAppear = getElementsByAppearTime(-1);
        if (!elemsToAppear || elemsToAppear.length === 0) {
            goToSlide(0, currentVideoInfo.DefaultNext);
        }
        _.each(elemsToAppear, function (id) {
            $('.mpls-ue-' + id).css("visibility", "visible");
        })

    }

}

var loadedMetaDataListener = function () {
    videoLength = video.duration.toFixed(1);
}

var reinitListeners = function (video) {
    videoLength = video.duration.toFixed(1);
    //Проблема с WebKit c первым видео
    video.addEventListener('loadedmetadata', loadedMetaDataListener);

    video.addEventListener("timeupdate", timeUpdateListener);
}

var removeListeners = function (video) {
    video.removeEventListener('timeupdate', timeUpdateListener, false);
}


var appendHtmlToDiv = function (ad) {
    var videos = ad.AdStates;
    var startHtml = ad.HtmlStartSource;
    var container = $("#mpls-container");
    _.each(videos, function (video) {
        var startTag = "";
        if (video.IsStart) {
            startTag = " mpls-start-video";
            container.append("<div class='mpls-video-control mpls-video-start-control'>" + startHtml + "</div>")
        }
        
        container.append("<video height='400' id='mpls-video-" + video.VideoUnitId + "' class='mpls-video" + startTag + "' data-id='" + video.VideoUnitId + "' data-name='" + video.Name + "'>" +
            "<source src='" + video.VideoUnit.FullPath + "' type='" + video.VideoUnit.MimeType + "' />" +
            "</video>");
        var controlLayer = $("<div></div>", {
            id: "mpls-layer-" + video.VideoUnitId,
            "data-for": "mpls-video-" + video.VideoUnitId,
            "class": 'mpls-video-control'
        });
        var _video = $("#mpls-video-" + video.VideoUnitId);
        _video.attr({
            'data-name': video.Name,
            'data-video-name': video.VideoUnit.Name
        });
        controlLayer.css({
            'top': '0',
            'left': '0',
            "width": document.getElementById("mpls-video-" + video.VideoUnitId).clientWidth,
            "height": document.getElementById("mpls-video-" + video.VideoUnitId).clientHeight
        });
        container.append(controlLayer);
        var currentId = "#mpls-layer-" + video.VideoUnitId;
        _.each(video.UserElements, function (element) {
            var id = element.HtmlId;
            if (element.HtmlId === undefined || element.HtmlId === '') {
                id = 'mpls-ue-' + element.Id;
            }
            var elem = $("<" + element.HtmlType + "></" + element.HtmlType + ">", {
                "class": element.HtmlClass,
                id: id,
                text: element.Text,
                "data-action": element.Action,
                "data-appear": element.TimeAppear,
                "data-disappear": element.TimeDisappear,
                "data-current-id": element.CurrentId,
                "data-next-id": element.NextId,
                "data-next-time": element.NextTime,
                "data-form-name": element.FormName,
                "style": element.HtmlStyle
            });
            elem.addClass('mpls-ue');
            elem.addClass('mpls-ue-' + element.Id);
            $(currentId).append(elem);
            _.each(element.HtmlTags, function (tag) {
                var key = tag.Key;
                var value = tag.Value;
                $('#' + id).attr(key, value);
            });
            $('#' + id).css({
                'top': element.X + '%',
                'left': element.Y + '%',
                //'width': element.Width,
                //'height': element.Height,
                'position': 'absolute'
            });

        })
    })
}
