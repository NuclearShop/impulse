var impulseUtils = function () {}
impulseUtils.enrichAbTestList = function (abList) {
    _.forEach(abList, function (ab) {
        ab.StartDateString = new Date(ab.DateStart).toLocaleDateString();
        ab.EndDateString = new Date(ab.DateEnd).toLocaleDateString();
        var full = Math.abs(new Date(ab.DateEnd) - new Date(ab.DateStart));
        var nowDate = new Date();
        var now = Math.abs(nowDate - new Date(ab.DateStart));
        if (nowDate < new Date(ab.DateStart))
            ab.progressValue = 0;
        else if (nowDate > new Date(ab.DateEnd))
            ab.progressValue = 100;
        else
            ab.progressValue = ((now / full) * 100).toFixed(0);
    })
}